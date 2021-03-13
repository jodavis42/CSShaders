using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class FrontEndTranslator
  {
    public ShaderLibrary mCurrentLibrary;
    public ShaderProject CurrentProject;
    public SemanticModel mSemanticModel;
    public CSharpCompilation Compilation;

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

    public ShaderType FindOrCreatePointerType(TypeKey key, string typeName, StorageClass storageClass)
    {
      string ptrTypeName = typeName + "_ptr";
      var baseType = mCurrentLibrary.FindType(key);
      return FindOrCreatePointerType(baseType, storageClass);
    }

    public ShaderType FindOrCreatePointerType(ShaderType baseType, StorageClass storageClass)
    {
      var pointerType = baseType.FindPointerType(storageClass);
      if (pointerType != null)
        return pointerType;

      pointerType = new ShaderType();
      pointerType.mBaseType = OpType.Pointer;
      pointerType.DebugInfo.Name = "";
      pointerType.mStorageClass = storageClass;
      baseType.StorageClassCollection.AddPointerType(storageClass, pointerType);
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

    public ShaderConstantLiteral CreateConstantLiteralZero(ShaderType constantType)
    {
      switch (constantType.mBaseType)
      {
        case OpType.Bool:
            return CreateConstantLiteral(false);
        case OpType.Int:
          {
            if (ShaderType.IsSignedInt(constantType))
              return CreateConstantLiteral(0);
            else
              return CreateConstantLiteral(0u);
          }

        case OpType.Float:
          return CreateConstantLiteral(0.0f);
        default:
          throw new Exception();
      }
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

    public ShaderConstantLiteral CreateConstantLiteral<T>(ShaderType literalType, T value)
    {
      var constantOp = new ShaderConstantLiteral();
      constantOp.mType = literalType;
      constantOp.mValue = value;
      constantOp = mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }

    public ShaderConstantLiteral CreateConstantLiteral<T>(T value)
    {
      var literalType = mCurrentLibrary.FindType(new TypeKey(typeof(T)));
      return CreateConstantLiteral<T>(literalType, value);
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

    public ShaderOp CreateConstantOp<T>(T value)
    {
      var constantLiteral = CreateConstantLiteral<T>(value);
      return CreateConstantOp(constantLiteral.mType, constantLiteral);
    }

    public ShaderOp CreateConstantOp<T>(ShaderType constantType, T value)
    {
      var constantLiteral = CreateConstantLiteral<T>(constantType, value);
      return CreateConstantOp(constantLiteral.mType, constantLiteral);
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
      {
        if(shaderFunction.GetReturnType() != FindType(typeof(void)))
          lastBlock.mTerminatorOp = CreateOp(lastBlock, OpInstructionType.OpUnreachable, null, null);
        else
          lastBlock.mTerminatorOp = CreateOp(lastBlock, OpInstructionType.OpReturn, null, null);
      }

      foreach (var block in shaderFunction.mBlocks)
      {
        if (block.mTerminatorOp == null)
          continue;

        for (var i = 0; i < block.mOps.Count; ++i)
        {
          var op = block.mOps[i] as ShaderOp;
          if (op != null &&
            (op.mOpType == OpInstructionType.OpReturn || op.mOpType == OpInstructionType.OpReturnValue ||
             op.mOpType == OpInstructionType.OpBranch || op.mOpType == OpInstructionType.OpBranchConditional ||
             op.mOpType == OpInstructionType.OpKill || op.mOpType == OpInstructionType.OpSwitch))
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
        shaderAttribute.Attribute = attribute;
        
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
      // Static fields are a special storage class. Get the target storage class from the field type. If the field type is a special storage class then use that instead of private.
      var storageClass = StorageClass.Private;
      if (fieldType.mStorageClass == StorageClass.Uniform || fieldType.mStorageClass == StorageClass.UniformConstant)
        storageClass = fieldType.mStorageClass;

      // Also create the pointer type if needed.
      var staticFieldType = fieldType.FindPointerType(storageClass);
      if (staticFieldType == null)
        staticFieldType = CreateType(fieldType, storageClass, true);

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
      resultType = FindOrCreatePointerType(resultType.GetDereferenceType(), selfOp.mResultType.mStorageClass);
      var memberVariableOp = CreateOp(context.mCurrentBlock, OpInstructionType.OpAccessChain, resultType, new List<IShaderIR> { selfOp, memberIndexConstant });
      return memberVariableOp;
    }

    public IShaderIR GetFunctionParameter(ShaderFunction shaderFunction, int paramIndex, IShaderIR argumentOp, FrontEndContext context)
    {
      // Handle if the parameter types don't match (e.g. handle out params and whatnot)
      var paramType = shaderFunction.GetExplicitParameterType(paramIndex);
      // If the function expects a pointer
      if (paramType.mBaseType == OpType.Pointer)
      {
        // If this isn't already a pointer then throw an exception. This might be valid, but has to be thought more about.
        // With C# I think it'll always be a pointer when you pass something in to an out/ref param.
        var op = argumentOp as ShaderOp;
        if (op.mResultType != paramType)
          throw new Exception();
      }
      // If this is a pointer but the function expects a value type then deref if necessary
      else
      {
        argumentOp = GetOrGenerateValueTypeFromIR(context.mCurrentBlock, argumentOp);
      }
      return argumentOp;
    }

    public ShaderOp GenerateFunctionCall(ShaderFunction shaderFunction, IShaderIR selfOp, List<IShaderIR> arguments, FrontEndContext context)
    {
      var argumentOps = new List<IShaderIR>();
      argumentOps.Add(shaderFunction);
      if (!shaderFunction.IsStatic || selfOp != null)
      {
        argumentOps.Add(selfOp);
      }

      if(arguments != null)
      {
        for (var i = 0; i < arguments.Count; ++i)
        {
          var argument = arguments[i];
          var paramIR = GetFunctionParameter(shaderFunction, i, argument, context);
          argumentOps.Add(paramIR);
        }
      }

      var fnShaderReturnType = shaderFunction.GetReturnType();
      var functionCallOp = CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, fnShaderReturnType, argumentOps);
      return functionCallOp;
    }

    public ShaderOp TryGenerateFunctionCall(ShaderFunction shaderFunction, IShaderIR selfOp, List<IShaderIR> arguments, FrontEndContext context)
    {
      if (shaderFunction == null)
        return null;
      return GenerateFunctionCall(shaderFunction, selfOp, arguments, context);
    }

    public ShaderOp GenerateCompositeExtract(IShaderIR selfIR, string fieldName, FrontEndContext context)
    {
      var selfOp = selfIR as ShaderOp;
      var selfType = selfOp.mResultType.GetDereferenceType();
      ShaderField shaderField;
      int fieldIndex;
      FindField(selfType, fieldName, out shaderField, out fieldIndex);

      var resultType = shaderField.mType.GetDereferenceType();
      var constantLiteral = CreateConstantLiteral<uint>((uint)fieldIndex);
      var memberVariableOp = CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeExtract, resultType, new List<IShaderIR> { selfOp, constantLiteral });
      return memberVariableOp;
    }

    public ShaderOp DefaultConstructPrimitive(ShaderType resultType, FrontEndContext context)
    {
      if (resultType.mBaseType == OpType.Bool)
        return CreateConstantOp(resultType, false);
      else if (resultType.mBaseType == OpType.Int)
      {
        if (ShaderType.IsSignedInt(resultType))
          return CreateConstantOp<int>(resultType, 0);
        else
          return CreateConstantOp<uint>(resultType, 0u);
      }
      else if (resultType.mBaseType == OpType.Float)
        return CreateConstantOp<float>(resultType, 0.0f);
      else
      {
        var leafType = ShaderType.FindLeafType(resultType);
        var leafTypeValue = DefaultConstructPrimitive(leafType, context);
        return CompositeSplatConstruct(leafType, leafTypeValue, context);
      }
    }

    public ShaderOp CompositeSplatConstruct(ShaderType resultType, IShaderIR value, FrontEndContext context)
    {
      var opArgs = new List<IShaderIR>();
      if (resultType.mBaseType == OpType.Vector)
      {
        var vectorResultType = VectorType.Load(resultType);
        for (var i = 0; i < vectorResultType.GetComponentCount(); ++i)
          opArgs.Add(value);
      }
      else if (resultType.mBaseType == OpType.Matrix)
      {
        var matrixResultType = MatrixType.Load(resultType);
        var vectorElement = CompositeSplatConstruct(matrixResultType.GetComponentType(), value, context);
        for (var i = 0; i < matrixResultType.GetComponentCount(); ++i)
          opArgs.Add(vectorElement);
      }
      else
        throw new Exception("invalid type");

      return CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeConstruct, resultType, opArgs);
    }

    ///////////////////////////////////////////////////////////////Conversions
    public ShaderOp CastFromBool(ShaderType resultType, IShaderIR expressionOp, IShaderIR zeroScalar, IShaderIR oneScalar, FrontEndContext context)
    {
      var zeroExpression = zeroScalar;
      var oneExpression = oneScalar;
      if(resultType.mBaseType == OpType.Vector)
      {
        zeroExpression = CompositeSplatConstruct(resultType, zeroScalar, context);
        oneExpression = CompositeSplatConstruct(resultType, oneScalar, context);
      }
      var castOp = CreateOp(context.mCurrentBlock, OpInstructionType.OpSelect, resultType, new List<IShaderIR> { expressionOp, zeroScalar, oneScalar });
      context.Push(castOp);
      return castOp;
    }

    public ShaderOp SimpleCast(ShaderType resultType, OpInstructionType instructionType, IShaderIR expressionOp, FrontEndContext context)
    {
      var castOp = CreateOp(context.mCurrentBlock, instructionType, resultType, new List<IShaderIR> { expressionOp });
      context.Push(castOp);
      return castOp;
    }

    public ShaderOp SimpleCompareCast(ShaderType resultType, OpInstructionType instructionType, IShaderIR expressionOp, IShaderIR constantOp, FrontEndContext context)
    {
      var castOp = CreateOp(context.mCurrentBlock, instructionType, resultType, new List<IShaderIR> { expressionOp, constantOp });
      context.Push(castOp);
      return castOp;
    }

    public ShaderOp CastBoolToInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp0 = CreateConstantOp<int>(0);
      var constantOp1 = CreateConstantOp<int>(1);
      return CastFromBool(resultType, expressionOp, constantOp0, constantOp1, context);
    }

    public ShaderOp CastBoolToUInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp0 = CreateConstantOp<uint>(0u);
      var constantOp1 = CreateConstantOp<uint>(1u);
      return CastFromBool(resultType, expressionOp, constantOp0, constantOp1, context);
    }

    public ShaderOp CastBoolToFloat(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp0 = CreateConstantOp<float>(0.0f);
      var constantOp1 = CreateConstantOp<float>(1.0f);
      return CastFromBool(resultType, expressionOp, constantOp0, constantOp1, context);
    }

    public ShaderOp CastSignedConvert(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpSConvert, expressionOp, context);
    }

    public ShaderOp CastUnsignedConvert(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpUConvert, expressionOp, context);
    }

    public ShaderOp CastFloatConvert(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpFConvert, expressionOp, context);
    }

    public ShaderOp CastIntToBool(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp = CreateConstantOp<int>(0);
      return SimpleCompareCast(resultType, OpInstructionType.OpINotEqual, expressionOp, constantOp, context);
    }

    public ShaderOp CastIntToUInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpBitcast, expressionOp, context);
    }

    public ShaderOp CastIntToFloat(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpConvertSToF, expressionOp, context);
    }

    public ShaderOp CastUIntToBool(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp = CreateConstantOp<uint>(0u);
      return SimpleCompareCast(resultType, OpInstructionType.OpINotEqual, expressionOp, constantOp, context);
    }

    public ShaderOp CastUIntToInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpBitcast, expressionOp, context);
    }

    public ShaderOp CastUIntToFloat(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpConvertSToF, expressionOp, context);
    }

    public ShaderOp CastFloatToBool(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      var constantOp = CreateConstantOp<float>(0.0f);
      return SimpleCompareCast(resultType, OpInstructionType.OpFOrdNotEqual,expressionOp, constantOp , context);
    }

    public ShaderOp CastFloatToInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpConvertFToS, expressionOp, context);
    }

    public ShaderOp CastFloatToUInt(ShaderType resultType, IShaderIR expressionOp, FrontEndContext context)
    {
      return SimpleCast(resultType, OpInstructionType.OpConvertFToU, expressionOp, context);
    }

    public ShaderBlock CreateBlock(string debugBlockName)
    {
      var block = new ShaderBlock();
      block.DebugInfo.Name = debugBlockName;
      return block;
    }
  }
}
