using System;

namespace CSShaders
{
  /// <summary>
  /// A helper class to fetch information about a vector.
  /// </summary>
  public class VectorType
  {
    /// <summary>
    /// Load this shader type as a vector. If this isn't a vector, null is returned.
    /// </summary>
    public static VectorType Load(ShaderType type)
    {
      if (type == null || type.mBaseType != OpType.Vector)
        return null;

      return new VectorType() { Type = type };
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
