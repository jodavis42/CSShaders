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
    public SyntaxTree mSyntaxTree;

    public List<FrontEndPass> mPasses = new List<FrontEndPass>()
    {
      // Collect all types
      new FrontEndTypeCollector(),
      // Collect all functions/vars definitions
      new FrontEndDeclarationCollector(),
      //new FrontEndPreconstructorPass(),
      // Collect all function contents, var definitions
      new FrontEndDefinitionCollector()
    };

    public void Translate(SyntaxTree tree, SemanticModel semanticModel)
    {
      mSyntaxTree = tree;
      mSemanticModel = semanticModel;

      FrontEndContext context = new FrontEndContext();
      foreach (var pass in mPasses)
        pass.Visit(this, tree.GetRoot(), context);

      // Validation passes
    }

    public ShaderType CreatePrimitive(Type type, OpType opType)
    {
      var shaderType = CreateType(new TypeKey(type), type.Name, opType, null);
      shaderType.DebugInfo.Name = "";
      shaderType.mTypeStorage.mPointerType.DebugInfo.Name = "";
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
      shaderType.mTypeStorage = new ShaderTypeStorage();
      shaderType.mTypeStorage.mDereferenceType = shaderType;

      shaderType.mTypeStorage.mPointerType = CreatePointerType(key, typeName);
      shaderType.mTypeStorage.mPointerType.mMeta.mName = typeName + "_ptr";

      return shaderType;
    }

    public ShaderType CreatePointerType(TypeKey key, string typeName)
    {
      string ptrTypeName = typeName + "_ptr";
      var baseType = mCurrentLibrary.FindType(key);
      var pointerType = mCurrentLibrary.FindOrCreateType(new TypeKey(ptrTypeName));
      pointerType.mBaseType = OpType.Pointer;
      pointerType.mTypeStorage = baseType.mTypeStorage;
      pointerType.DebugInfo.Name = "";
      return pointerType;
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
            constantOp.mValue = int.Parse(value);
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
      mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }
    public ShaderConstantLiteral CreateConstantLiteral(int value)
    {
      var constantOp = new ShaderConstantLiteral();
      constantOp.mType = mCurrentLibrary.FindType(new TypeKey(typeof(int)));
      constantOp.mValue = value;
      mCurrentLibrary.GetOrCreateConstantLiteral(constantOp);
      return constantOp;
    }

    public ShaderOp CreateConstantOp(ShaderType constantType, ShaderConstantLiteral constantLiteral)
    {
      var constantOpKey = new ConstantOpKey(constantLiteral);
      var constantOp = mCurrentLibrary.FindConstant(constantOpKey);
      if (constantOp == null)
      {
        constantOp = mCurrentLibrary.CreateConstant(constantOpKey);
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
        constantOp = mCurrentLibrary.CreateConstant(constantOpKey);
        if (constantLiteral == true)
          InitializeOp(constantOp, OpInstructionType.OpConstantTrue, constantType, null);
        else
          InitializeOp(constantOp, OpInstructionType.OpConstantFalse, constantType, null);
      }
      return constantOp;
    }

    public ShaderOp CreateOpVariable(ShaderType resultType, FrontEndContext context)
    {
      var variableOp = CreateOp(OpInstructionType.OpVariable, resultType.mTypeStorage.mPointerType, null);
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
      return shaderType.mTypeStorage.mDereferenceType;
    }

    static public ShaderType GetPointerType(ShaderType shaderType)
    {
      return shaderType.mTypeStorage.mPointerType;
    }

    public void FixupBlockTerminators(ShaderFunction shaderFunction)
    {
      if(shaderFunction.mBlocks.Count == 0)
      {
        shaderFunction.mBlocks.Add(new ShaderBlock());
      }

      var lastBlock = shaderFunction.mBlocks[shaderFunction.mBlocks.Count - 1];
      if (lastBlock.mTerminatorOp == null)
        CreateOp(lastBlock, OpInstructionType.OpReturn, null, null);

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
  }
}
