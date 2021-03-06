﻿using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

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
    public TypeKey(TypeName typeName)
    {
      mKey = typeName.FullName;
    }

    public TypeKey(ShaderType shaderType)
    {
      mKey = shaderType.mMeta.mName;
    }

    public TypeKey(Type type)
    {
      mKey = TypeAliases.GetFullTypeName(type);
    }

    public TypeKey(ISymbol symbol)
    {
      mKey = TypeAliases.GetFullTypeName(symbol);
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

  [DebuggerDisplay("{mKey}")]
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

  public class FieldKey
  {
    string mKey = "";

    public FieldKey(string key)
    {
      mKey = key;
    }

    public FieldKey(string fieldType, string fieldName)
    {
      mKey = $"{fieldType}_{fieldName}";
    }

    public FieldKey(TypeName fieldType, string fieldName)
    {
      mKey = $"{fieldType.FullName}_{fieldName}";
    }

    public FieldKey(ShaderField field)
    {
      mKey = $"{field.mType.mMeta.mName}_{field.mMeta.mName}";
    }

    public override int GetHashCode()
    {
      return mKey.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is FieldKey fieldKey)
        return mKey.Equals(fieldKey.mKey);
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

  public class UnaryOpKey
  {
    string OpToken;
    ShaderType OperandType;

    public UnaryOpKey(string opToken, ShaderType operandType)
    {
      OpToken = opToken;
      OperandType = operandType;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(OpToken, OperandType);
    }

    public override bool Equals(object obj)
    {
      if (obj is UnaryOpKey unaryOpKey)
        return OperandType == unaryOpKey.OperandType && OpToken == unaryOpKey.OpToken;
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
