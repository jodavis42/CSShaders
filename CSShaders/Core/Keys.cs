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
      Validate();
    }

    public ConstantOpKey(ShaderConstantLiteral constant)
    {
      mType = constant.mType;
      mValue = constant.mValue;
      Validate();
    }

    void Validate()
    {
      switch (mType.mBaseType)
      {
        case OpType.Bool:
          {
            if (!(mValue is bool value))
              throw new Exception();
            break;
          }
        case OpType.Int:
          {
            if (mType.mParameters.Count < 2)
              return;
            if (!ShaderType.IsSignedInt(mType))
            {
              if (!(mValue is uint value))
                throw new Exception();
            }
            else
            {
              if (!(mValue is int value))
                throw new Exception();
            }
            break;
          }
        case OpType.Float:
          {
            if (!(mValue is float value))
              throw new Exception();
            break;
          }
      }
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(mType.GetHashCode(), mValue.GetHashCode());
    }

    public override bool Equals(object obj)
    {
      if (obj is ConstantOpKey key)
      {
        if (key.mType != mType)
          return false;

        return mValue.Equals(key.mValue);
      }
      return false;
    }
  }

  public class BinaryOpKey
  {
    ShaderType LeftType;
    string OpToken;
    ShaderType RightType;

    public BinaryOpKey(ShaderType leftType, string opToken, ShaderType rightType)
    {
      LeftType = leftType;
      OpToken = opToken;
      RightType = rightType;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(LeftType, OpToken, RightType);
    }

    public override bool Equals(object obj)
    {
      if (obj is BinaryOpKey binaryOpKey)
        return LeftType == binaryOpKey.LeftType && RightType == binaryOpKey.RightType && OpToken == binaryOpKey.OpToken;
      return false;
    }
  }
}
