using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderLibrary
  {
    public ShaderModule mDependencies;
    public Dictionary<TypeKey, ShaderType> mTypeMap = new Dictionary<TypeKey, ShaderType>();
    public Dictionary<ConstantOpKey, ShaderOp> mConstantOps = new Dictionary<ConstantOpKey, ShaderOp>();
    public Dictionary<FunctionKey, ShaderFunction> mFunctionMap = new Dictionary<FunctionKey, ShaderFunction>();

    public Dictionary<string, ShaderConstantLiteral> mConstantLiterals = new Dictionary<string, ShaderConstantLiteral>();

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






    public ShaderConstantLiteral GetOrCreateConstantLiteral(ShaderConstantLiteral constantLiteral)
    {
      var key = constantLiteral.mValue.ToString();
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
