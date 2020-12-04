using System;

namespace Math
{
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
