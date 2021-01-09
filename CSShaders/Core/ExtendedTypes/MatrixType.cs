using System;

namespace CSShaders
{
  /// <summary>
  /// A helper class to fetch information about a matrix.
  /// </summary>
  public class MatrixType
  {
    /// <summary>
    /// Load this shader type as a matrix. If this isn't a matrix, null is returned.
    /// </summary>
    public static MatrixType Load(ShaderType type)
    {
      if (type == null || type.mBaseType != OpType.Vector)
        return null;

      return new MatrixType() { Type = type };
    }

    public ShaderType GetComponentType()
    {
      return Type.mParameters[0] as ShaderType;
    }

    public UInt32 GetComponentCount()
    {
      var constantLiteral = Type.mParameters[1] as ShaderConstantLiteral;
      if (constantLiteral == null)
        throw new Exception("Parameter should be a constant literal");
      return (UInt32)constantLiteral.mValue;
    }

    public ShaderType Type;
  }
}
