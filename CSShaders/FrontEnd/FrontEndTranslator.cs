using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSShaders
{
  public class FrontEndContext
  {
    public FrontEndTranslator mFrontEnd;
    public ShaderType mCurrentType;
    public ShaderFunction mCurrentFunction;
    public ShaderBlock mCurrentBlock;

    public Stack<IShaderIR> mOpStack = new Stack<IShaderIR>();
    public Dictionary<ISymbol, IShaderIR> mSymbolToIRMap = new Dictionary<ISymbol, IShaderIR>();
    public IShaderIR mThisOp = null;

    public void Push(IShaderIR op)
    {
      mOpStack.Push(op);
    }
    public IShaderIR Pop()
    {
      return mOpStack.Pop();
    }
  }

  public class FrontEndTranslator
  {
    public ShaderLibrary mCurrentLibrary;
    public SemanticModel mSemanticModel;
    public CSharpCompilation Compilation;

    public List<FrontEndPass> mPasses = new List<FrontEndPass>()
    {
      // Collect all types
      new FrontEndPrimitiveTypeCollector(),
      new FrontEndTypeCollector(),
      // Visit Intrinsics separately (e.g. Vector2)
      
      new FrontEndPrimitiveDeclarationCollector(),
      // Collect all functions/vars definitions
      new FrontEndDeclarationCollector(),
      new FrontEndPreconstructorPass(),
      // Collect all function contents, var definitions
      new FrontEndDefinitionCollector()
    };

    public void Translate(CSharpCompilation compilation, List<SyntaxTree> trees)
    {
      Compilation = compilation;

      FrontEndContext context = new FrontEndContext();
      foreach (var pass in mPasses)
      {
        pass.Visit(this, Compilation, trees, context);
      }

      // Validation passes
    }

    public ShaderType CreatePrimitive(Type type, OpType opType)
    {
      var shaderType = CreateType(new TypeKey(type), type.Name, opType, null);
      // Clear out the debug names for primitives. The disassembler names are best for primitives.
      shaderType.DebugInfo.Name = "";
      shaderType.FindPointerType(StorageClass.Function).DebugInfo.Name = "";
      return shaderType;
    }

    public ShaderType CreateType(ISymbol symbol, OpType opType)
    {
      return CreateType(new TypeKey(symbol), symbol.Name, opType, symbol);
    }

    public ShaderType CreateType(TypeKey key, string typeName, OpType opType, ISymbol symbol)
    {
      var shaderType = mCurrentLibrary.CreateType(key);
      shaderType.DebugInfo.Name = typeName;
      shaderType.mMeta.mName = typeName;
      shaderType.mBaseType = opType;
      shaderType.StorageClassCollection = new ShaderTypeStorageClassCollection(shaderType);

      var pointerType = CreatePointerType(key, typeName);
      pointerType.mMeta.mName = typeName + "_ptr";

      return shaderType;
    }

    public ShaderType CreateType(ShaderType baseType, StorageClass storageClass, bool ispointerType)
    {
      var typeStorage = baseType.StorageClassCollection;
      var pointerType = typeStorage.FindPointerType(storageClass);
      if (pointerType != null)
        return pointerType;
      var shaderType = new ShaderType();
      shaderType.DebugInfo.Name = "";
      shaderType.mMeta.mName = baseType.mMeta.mName;
      shaderType.mBaseType = OpType.Pointer;
      shaderType.mStorageClass = storageClass;
      shaderType.StorageClassCollection = typeStorage;
      shaderType.mParameters.AddRange(baseType.mParameters);
      typeStorage.AddPointerType(storageClass, shaderType);

      return shaderType;
    }

    public ShaderType CreatePointerType(TypeKey key, string typeName)
    {
      string ptrTypeName = typeName + "_ptr";
      var baseType = mCurrentLibrary.FindType(key);
      var pointerType = new ShaderType();
      pointerType.mBaseType = OpType.Pointer;
      pointerType.DebugInfo.Name = "";
      baseType.StorageClassCollection.AddPointerType(StorageClass.Function, pointerType);
      return pointerType;
    }

    public string GenerateDebugFunctionName(ShaderType owningType, string fnName)
    {
      return owningType.mMeta.mName + "_" + fnName;
    }

    public ShaderFunction CreateFunctionAndType(ShaderType owningType, ShaderType returnType, string name, ShaderType thisType, List<ShaderType> args)
    {
      var shaderFunction = new ShaderFunction();
      shaderFunction.mMeta.mName = name;
      shaderFunction.DebugInfo.Name = GenerateDebugFunctionName(owningType, name);
      shaderFunction.mResultType = FindOrCreateFunctionType(returnType, thisType, args);
      shaderFunction.mParameters.Add(returnType);
      if (thisType != null)
        shaderFunction.mParameters.Add(thisType);
      if (args != null)
        shaderFunction.mParameters.AddRange(args);
      shaderFunction.mMeta.IsStatic = shaderFunction.IsStatic = (thisType == null);
      owningType.mFunctions.Add(shaderFunction);
      return shaderFunction;
    }

    public ShaderFunction CreateFunction(ShaderType owningType, ShaderType returnType, string name, ShaderType thisType, List<ShaderType> args)
    {
      var shaderFunction = new ShaderFunction();
      shaderFunction.mMeta = new ShaderFunctionMeta();
      shaderFunction.mMeta.mName = name;
      shaderFunction.DebugInfo.Name = name;
      shaderFunction.mParameters.Add(returnType);
      if(thisType != null)
        shaderFunction.mParameters.Add(thisType);
      if (args != null)
        shaderFunction.mParameters.AddRange(args);
      owningType.mFunctions.Add(shaderFunction);
      return shaderFunction;
    }

    public ShaderType CreateFunctionType(ShaderType returnType, ShaderType thisType, List<ShaderType> argTypes)
    {
      return FindOrCreateFunctionType(returnType, thisType, argTypes);
    }

    public ShaderType FindOrCreateFunctionType(ShaderType returnType, ShaderType thisType, List<ShaderType> argTypes)
    {
      var builder = new StringBuilder();
      builder.Append(returnType.mMeta.mName);
      builder.Append("(");
      if (thisType != null)
      {
        builder.Append(thisType.mMeta.mName);
        builder.Append(", ");
      }
      if(argTypes != null)
      {
        foreach (var argType in argTypes)
        {
          builder.Append(argType.mMeta.mName);
          builder.Append(", ");
        }
      }
      
      builder.Append(")");

      var shaderFunctionType = mCurrentLibrary.FindType(new TypeKey(builder.ToString()));
      if(shaderFunctionType == null)
      {
        shaderFunctionType = CreateType(new TypeKey(builder.ToString()), builder.ToString(), OpType.Function, null);
        shaderFunctionType.mParameters.Add(returnType);
        if (thisType != null)
        {
          shaderFunctionType.mParameters.Add(thisType);
        }
        if (argTypes != null)
        {
          foreach (var paramType in argTypes)
          {
            shaderFunctionType.mParameters.Add(paramType);
          }
        }
        shaderFunctionType.DebugInfo.Name = "";
      }
      
      return shaderFunctionType;
    }

    public ShaderField CreateField(ShaderType owningType, ShaderType fieldType, string name, IShaderIR initializerExpression)
    {
      var shaderField = new ShaderField();
      shaderField.mMeta = new ShaderFieldMeta();
      shaderField.mType = fieldType;
      shaderField.mMeta.mName = name;
      shaderField.DebugInfo.Name = name;
      owningType.mFields.Add(shaderField);

      return shaderField;
    }

    public ShaderConstantLiteral CreateConstantLiteral(ShaderType constantType, string value)
    {
      var constantOp = new ShaderConstantLiteral();
      constantOp.mType = constantType;
      switch (constantType.mBaseType)
      {
        case OpType.Bool:
          {
            constantOp.mValue = bool.Parse(value);
            break;
          }
        case OpType.Int:
          {
            if (ShaderType.IsSignedInt(constantType))
              constantOp.mValue = int.Parse(value);
            else
              constantOp.mValue = uint.Parse(value);
            break;
          }

        case OpType.Float:
          {
            constantOp.mValue = float.Parse(value);
            break;
          }

      }

      mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }

    public ShaderConstantLiteral CreateConstantLiteral(uint value)
    {
      var constantOp = new ShaderConstantLiteral();
      constantOp.mType = mCurrentLibrary.FindType(new TypeKey(typeof(uint)));
      constantOp.mValue = value;
      constantOp = mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }
    public ShaderConstantLiteral CreateConstantLiteral(int value)
    {
      var constantOp = new ShaderConstantLiteral();
      constantOp.mType = mCurrentLibrary.FindType(new TypeKey(typeof(int)));
      constantOp.mValue = value;
      constantOp = mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }

    public ShaderOp CreateConstantOp(ShaderType constantType, ShaderConstantLiteral constantLiteral)
    {
      var constantOpKey = new ConstantOpKey(constantLiteral);
      var constantOp = mCurrentLibrary.FindConstant(constantOpKey);
      if (constantOp == null)
      {
        constantOp = mCurrentLibrary.FindOrCreateConstant(constantOpKey);
        // Handle bools specially (they're special ops in spirv)
        if(constantType.mBaseType == OpType.Bool)
        {
          var boolVal = (bool)constantLiteral.mValue;
          if (boolVal == true)
            InitializeOp(constantOp, OpInstructionType.OpConstantTrue, constantType, null);
          else
            InitializeOp(constantOp, OpInstructionType.OpConstantFalse, constantType, null);
        }
        else
          InitializeOp(constantOp, OpInstructionType.OpConstant, constantType, new List<IShaderIR> { constantLiteral });
      }
      return constantOp;
    }

    public ShaderOp CreateConstantOp(ShaderType constantType, bool constantLiteral)
    {
      var constantOpKey = new ConstantOpKey(mCurrentLibrary.FindType(new TypeKey(typeof(bool))), constantLiteral);
      var constantOp = mCurrentLibrary.FindConstant(constantOpKey);
      if (constantOp == null)
      {
        constantOp = mCurrentLibrary.FindOrCreateConstant(constantOpKey);
        if (constantLiteral == true)
          InitializeOp(constantOp, OpInstructionType.OpConstantTrue, constantType, null);
        else
          InitializeOp(constantOp, OpInstructionType.OpConstantFalse, constantType, null);
      }
      return constantOp;
    }

    public ShaderOp CreateOpVariable(ShaderType resultType, FrontEndContext context)
    {
      var variableOp = CreateOp(OpInstructionType.OpVariable, resultType.FindPointerType(resultType.mStorageClass), null);
      context.mCurrentFunction.mBlocks[0].mLocalVariables.Add(variableOp);
      return variableOp;
    }

    public void InitializeOp(ShaderOp op, OpInstructionType opType, ShaderType resultType, List<IShaderIR> arguments)
    {
      op.mOpType = opType;
      op.mResultType = resultType;
      if (arguments != null)
        op.mParameters.AddRange(arguments);
    }

    public ShaderOp CreateOp(OpInstructionType opType, ShaderType resultType, List<IShaderIR> arguments)
    {
      var resultOp = new ShaderOp();
      InitializeOp(resultOp, opType, resultType, arguments);
      return resultOp;
    }

    public ShaderOp CreateOp(ShaderBlock block, OpInstructionType opType, ShaderType resultType, List<IShaderIR> arguments)
    {
      var resultOp = CreateOp(opType, resultType, arguments);
      block.mOps.Add(resultOp);
      return resultOp;
    }

    public ShaderOp ConstructAndInitializeOpVariable(ShaderType shaderType, FrontEndContext context)
    {
      // Create the variable
      var voidType = mCurrentLibrary.FindType(new TypeKey(typeof(void)));
      var varOp = CreateOpVariable(shaderType, context);

      // Either call the constructor or implicitly construct the type if needed.
      if (shaderType.mImplicitConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mImplicitConstructor);
        fnParamOps.Add(varOp);
        CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }
      else if (shaderType.mPreConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mPreConstructor);
        fnParamOps.Add(varOp);
        CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }
      return varOp;
    }

    public ShaderOp CreateStoreOp(ShaderBlock block, IShaderIR variable, IShaderIR result)
    {
      var valueResult = GetOrGenerateValueTypeFromIR(block, result);
      return CreateOp(block, OpInstructionType.OpStore, null, new List<IShaderIR> { variable, valueResult });
    }

    public ShaderOp GetOrGenerateValueTypeFromIR(ShaderBlock block, IShaderIR ir)
    {
      var op = ir as ShaderOp;
      if (op == null)
        return null;

      var opResultType = GetOpResultType(op);
      if (opResultType.mBaseType != OpType.Pointer)
        return op;

      var opValueType = GetValueType(opResultType);
      var valueOp = CreateOp(block, OpInstructionType.OpLoad, opValueType, new List<IShaderIR> { op });
      return valueOp;
    }

    public ShaderType GetOpResultType(ShaderOp op)
    {
      return op.mResultType;
    }

    static public ShaderType GetValueType(ShaderType shaderType)
    {
      return shaderType.GetDereferenceType();
    }

    static public ShaderType GetPointerType(ShaderType shaderType)
    {
      return shaderType.FindPointerType(StorageClass.Function);
    }

    public void FixupBlockTerminators(ShaderFunction shaderFunction)
    {
      if(shaderFunction.mBlocks.Count == 0)
      {
        shaderFunction.mBlocks.Add(new ShaderBlock());
      }

      var lastBlock = shaderFunction.mBlocks[shaderFunction.mBlocks.Count - 1];
      if (lastBlock.mTerminatorOp == null)
        lastBlock.mTerminatorOp = CreateOp(lastBlock, OpInstructionType.OpReturn, null, null);

      foreach (var block in shaderFunction.mBlocks)
      {
        if (block.mTerminatorOp == null)
          continue;

        for (var i = 0; i < block.mOps.Count; ++i)
        {
          var op = block.mOps[i] as ShaderOp;
          if (op != null && (op.mOpType == OpInstructionType.OpReturn || op.mOpType == OpInstructionType.OpReturnValue))
          {
            var nextOpIndex = i + 1;
            block.mOps.RemoveRange(nextOpIndex, block.mOps.Count - nextOpIndex);
            break;
          }
        }
      }
    }

    public ShaderAttributes ParseAttributes(ISymbol symbol)
    {
      var shaderAttributes = new ShaderAttributes();
      foreach(var attribute in symbol.GetAttributes())
      {
        var shaderAttribute = new ShaderAttribute();
        shaderAttributes.Add(shaderAttribute);
        shaderAttribute.Name = attribute.AttributeClass.Name;
        foreach(var argument in attribute.NamedArguments)
        {
          var shaderAttributeParam = new ShaderAttributeParameter();
          shaderAttribute.Parameters.Add(shaderAttributeParam);
          shaderAttributeParam.Name = argument.Key;
          shaderAttributeParam.Value = argument.Value;
        }
      }
      return shaderAttributes;
    }

    void FindField<FieldType>(IEnumerable<FieldType> fieldList, string fieldName, out FieldType shaderField, out int fieldIndex) where FieldType : ShaderField
    {
      fieldIndex = 0;
      foreach(var field in fieldList)
      {
        if (field.mMeta.mName == fieldName)
        {
          shaderField = field;
          return;
        }
        ++fieldIndex;
      }

      shaderField = null;
      fieldIndex = -1;
    }

    public void FindInstanceField(ShaderType owningType, string fieldName, out ShaderField shaderField, out int fieldIndex)
    {
      FindField(owningType.mFields, fieldName, out shaderField, out fieldIndex);
    }

    public void FindStaticField(ShaderType owningType, string fieldName, out GlobalShaderField shaderField, out int fieldIndex)
    {
      FindField(owningType.mStaticFields, fieldName, out shaderField, out fieldIndex);
    }

    public void FindField(ShaderType owningType, string fieldName, out ShaderField shaderField, out int fieldIndex)
    {
      FindInstanceField(owningType, fieldName, out shaderField, out fieldIndex);
      if (shaderField == null)
      {
        GlobalShaderField globalShaderField;
        FindStaticField(owningType, fieldName, out globalShaderField, out fieldIndex);
        shaderField = globalShaderField;
      }
    }

    public GlobalShaderField CreateStaticField(ShaderType owningType, ShaderType fieldType, string name, IShaderIR initializerExpression)
    {
      // Static fields are a special storage class. Create the pointer type if needed.
      var staticFieldType = fieldType.FindPointerType(StorageClass.Private);
      if (staticFieldType == null)
        staticFieldType = CreateType(fieldType, StorageClass.Private, true);

      var shaderField = new GlobalShaderField();
      shaderField.mMeta = new ShaderFieldMeta();
      shaderField.mType = staticFieldType;
      shaderField.mMeta.mName = name;
      shaderField.DebugInfo.Name = name;
      owningType.mStaticFields.Add(shaderField);
      
      shaderField.InstanceOp = new ShaderOp();
      shaderField.InstanceOp.mOpType = OpInstructionType.OpVariable;
      shaderField.InstanceOp.mResultType = staticFieldType;
      shaderField.InstanceOp.mParameters.Add(staticFieldType);
      mCurrentLibrary.mStaticGlobals.Add(shaderField, shaderField);
      return shaderField;
    }
    public ShaderType FindType(TypeKey typeKey)
    {
      return mCurrentLibrary.FindType(typeKey);
    }

    public ShaderType FindType(Type type)
    {
      return mCurrentLibrary.FindType(new TypeKey(type));
    }

    public ShaderOp GenerateAccessChain(IShaderIR selfIR, string fieldName, FrontEndContext context)
    {
      var selfOp = selfIR as ShaderOp;
      var selfType = selfOp.mResultType.GetDereferenceType();
      ShaderField shaderField;
      int fieldIndex;
      FindField(selfType, fieldName, out shaderField, out fieldIndex);
      return GenerateAccessChain(selfIR, shaderField.mType, (uint)fieldIndex, context);
    }

    public ShaderOp GenerateAccessChain(IShaderIR selfIR, ShaderType resultType, uint fieldIndex, FrontEndContext context)
    {
      var selfOp = selfIR as ShaderOp;
      var constantLiteral = CreateConstantLiteral(fieldIndex);
      var memberIndexConstant = CreateConstantOp(FindType(typeof(uint)), constantLiteral);
      resultType = resultType.FindPointerType(selfOp.mResultType.mStorageClass);
      var memberVariableOp = CreateOp(context.mCurrentBlock, OpInstructionType.OpAccessChain, resultType, new List<IShaderIR> { selfOp, memberIndexConstant });
      return memberVariableOp;
    }
  }
}
