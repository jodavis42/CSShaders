using Microsoft.CodeAnalysis;
using System;

namespace CSShaders
{
  public class TypeKey
  {
    string mKey = "";

    // @JoshD: Ideally remove this
    public TypeKey(string typeName)
    {
      mKey = typeName;
    }

    public TypeKey(ShaderType shaderType)
    {
      mKey = shaderType.mMeta.mName;
    }

    public TypeKey(Type type)
    {
      mKey = type.Name;
    }

    public TypeKey(ISymbol symbol)
    {
      mKey = symbol.Name;
    }

    public override int GetHashCode()
    {
      return mKey.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is TypeKey typeKey)
        return mKey.Equals(typeKey.mKey);
      return false;
    }
  }

  public class FunctionKey
  {
    string mKey = "";

    // @JoshD: Ideally remove this
    public FunctionKey(string typeName)
    {
      mKey = typeName;
    }

    public FunctionKey(ISymbol symbol)
    {
      mKey = symbol.ToString();
    }

    public override int GetHashCode()
    {
      return mKey.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is FunctionKey functionKey)
        return mKey.Equals(functionKey.mKey);
      return false;
    }
  }

  public class ConstantOpKey
  {
    ShaderType mType;
    object mValue;
    public ConstantOpKey(ShaderType shaderType, object value)
    {
      mType = shaderType;
      mValue = value;
    }

    public ConstantOpKey(ShaderConstantLiteral constant)
    {
      mType = constant.mType;
      mValue = constant.mValue;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(mType.GetHashCode(), mValue.GetHashCode());
    }
  }
}
