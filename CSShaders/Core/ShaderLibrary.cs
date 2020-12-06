using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderLibrary
  {
    public ShaderModule mDependencies;
    public CSharpCompilation SourceCompilation;

    public delegate void InstrinsicDelegate(FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context);
    public delegate void InstrinsicSetterDelegate(FrontEndTranslator translator, IShaderIR selfInstance, IShaderIR rhsIR, FrontEndContext context);

    public Dictionary<ConstantOpKey, ShaderConstantLiteral> mConstantLiterals = new Dictionary<ConstantOpKey, ShaderConstantLiteral>();
    public Dictionary<ConstantOpKey, ShaderOp> mConstantOps = new Dictionary<ConstantOpKey, ShaderOp>();
    public Dictionary<ShaderField, GlobalShaderField> mStaticGlobals = new Dictionary<ShaderField, GlobalShaderField>();
    public Dictionary<TypeKey, ShaderType> mTypeMap = new Dictionary<TypeKey, ShaderType>();
    public Dictionary<FunctionKey, ShaderFunction> mFunctionMap = new Dictionary<FunctionKey, ShaderFunction>();
    public Dictionary<FunctionKey, InstrinsicDelegate> IntrinsicFunctions = new Dictionary<FunctionKey, InstrinsicDelegate>();
    public Dictionary<FunctionKey, InstrinsicSetterDelegate> IntrinsicSetterFunctions = new Dictionary<FunctionKey, InstrinsicSetterDelegate>();

    //---------------------------------------------------------------Types
    public bool AddType(TypeKey key, ShaderType shaderType)
    {
      return mTypeMap.TryAdd(key, shaderType);
    }

    public ShaderType CreateType(TypeKey key)
    {
      var shaderType = new ShaderType();
      mTypeMap.Add(key, shaderType);
      return shaderType;
    }

    public ShaderType FindType(TypeKey key, bool checkDependencies = true)
    {
      var result = mTypeMap.GetValueOrDefault(key);
      if (result == null && checkDependencies && mDependencies != null)
        result = mDependencies.FindType(key, checkDependencies);
      return result;
    }

    public ShaderType FindOrCreateType(TypeKey key, bool checkDependencies = true)
    {
      var result = FindType(key, checkDependencies);
      if (result == null)
        result = CreateType(key);
      return result;
    }

    public IEnumerable<ShaderType> GetTypes()
    {
      return mTypeMap.Values;
    }

    //---------------------------------------------------------------Constants
    public bool AddConstant(ConstantOpKey key, ShaderOp shaderOp)
    {
      return mConstantOps.TryAdd(key, shaderOp);
    }

    public ShaderOp CreateConstant(ConstantOpKey key)
    {
      ShaderOp op = new ShaderOp();
      mConstantOps.Add(key, op);
      return op;
    }
    public ShaderOp FindConstant(ConstantOpKey key, bool checkDependencies = true)
    {
      var result = mConstantOps.GetValueOrDefault(key);
      if (result == null && checkDependencies && mDependencies != null)
        result = mDependencies.FindConstant(key, checkDependencies);
      return result;
    }

    public ShaderOp FindOrCreateConstant(ConstantOpKey key, bool checkDependencies = true)
    {
      var result = FindConstant(key, checkDependencies);
      if (result == null)
        result = CreateConstant(key);
      return result;
    }

    //---------------------------------------------------------------Functions
    public bool AddFunction(FunctionKey key, ShaderFunction shaderFunction)
    {
      return mFunctionMap.TryAdd(key, shaderFunction);
    }

    public ShaderFunction CreateFunction(FunctionKey key)
    {
      ShaderFunction shaderFunction = new ShaderFunction();
      mFunctionMap.Add(key, shaderFunction);
      return shaderFunction;
    }
    
    public ShaderFunction FindFunction(FunctionKey key, bool checkDependencies = true)
    {
      var result = mFunctionMap.GetValueOrDefault(key);
      if (result == null && checkDependencies && mDependencies != null)
        result = mDependencies.FindFunction(key, checkDependencies);
      return result;
    }

    public ShaderFunction FindOrCreateFunction(FunctionKey key, bool checkDependencies = true)
    {
      var result = FindFunction(key, checkDependencies);
      if (result == null)
        result = CreateFunction(key);
      return result;
    }

    //---------------------------------------------------------------IntrinsicFunctions
    public void CreateIntrinsicFunction(FunctionKey key, InstrinsicDelegate intrinsic)
    {
      IntrinsicFunctions.TryAdd(key, intrinsic);
    }

    public InstrinsicDelegate FindIntrinsicFunction(FunctionKey key, bool checkDependencies = true)
    {
      var result = IntrinsicFunctions.GetValueOrDefault(key, null);
      if(result == null && mDependencies != null)
      {
        foreach(var dependency in mDependencies)
        {
          result = dependency.FindIntrinsicFunction(key, checkDependencies);
          if (result != null)
            break;
        }
      }
      return result;
    }

    public void CreateIntrinsicSetterFunction(FunctionKey key, InstrinsicSetterDelegate intrinsic)
    {
      IntrinsicSetterFunctions.TryAdd(key, intrinsic);
    }

    public InstrinsicSetterDelegate FindIntrinsicSetterFunction(FunctionKey key, bool checkDependencies = true)
    {
      var result = IntrinsicSetterFunctions.GetValueOrDefault(key, null);
      if (result == null && mDependencies != null)
      {
        foreach (var dependency in mDependencies)
        {
          result = dependency.FindIntrinsicSetterFunction(key, checkDependencies);
          if (result != null)
            break;
        }
      }
      return result;
    }

    //---------------------------------------------------------------Constant Literals
    public ShaderConstantLiteral GetOrCreateConstantLiteral(ShaderConstantLiteral constantLiteral)
    {
      var key = new ConstantOpKey(constantLiteral);
      var result = mConstantLiterals.GetValueOrDefault(key);
      if(result == null)
      {
        result = constantLiteral;
        mConstantLiterals.Add(key, result);
      }
      return result;
    }
  }
}
