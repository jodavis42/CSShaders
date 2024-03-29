﻿using System;

namespace Math
{
  public class IntegerPrimitive : Attribute
  {
    UInt32 Width = 32;
    bool Signed = false;
    public IntegerPrimitive(UInt32 width, bool signed)
    {
      Width = width;
      Signed = signed;
    }
  }

  public class FloatPrimitive : Attribute
  {
    UInt32 Width = 32;
    public FloatPrimitive(UInt32 width)
    {
      Width = width;
    }
  }

  public class VectorPrimitive : Attribute
  {
    Type ComponentType;
    UInt32 ComponentCount;
    public VectorPrimitive(Type componentType, UInt32 componentCount)
    {
      ComponentType = componentType;
      ComponentCount = componentCount;
    }
  }

  public class MatrixPrimitive : Attribute
  {
    Type ColumnType;
    UInt32 ColumnCount;
    public MatrixPrimitive(Type columnType, UInt32 columnCount)
    {
      ColumnType = columnType;
      ColumnCount = columnCount;
    }
  }

  public class FixedArrayPrimitive : Attribute
  {
    Type ComponentType;
    UInt32 ComponentCount;
    public FixedArrayPrimitive(Type componentType, UInt32 componentCount)
    {
      ComponentType = componentType;
      ComponentCount = componentCount;
    }
  }

  public class Swizzle : System.Attribute
  {
    public Swizzle(params UInt32[] list)
    {

    }
  }

  public class CompositeConstruct : System.Attribute
  {
  }
}
