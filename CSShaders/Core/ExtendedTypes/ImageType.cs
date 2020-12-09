using System;

namespace CSShaders
{
  /// <summary>
  /// A helper class to fetch information about an image.
  /// </summary>
  public class ImageType
  {
    /// <summary>
    /// Load this shader type as an image. If this isn't an image, null is returned.
    /// </summary>
    public static ImageType Load(ShaderType type)
    {
      if (type == null || type.mBaseType != OpType.Image)
        return null;

      return new ImageType() { Type = type };
    }

    public ShaderType GetSampledType()
    {
      return Type.mParameters[0] as ShaderType;
    }

    public Shader.ImageDimension GetDimension()
    {
      return (Shader.ImageDimension)GetValueParameter(1);
    }

    public Shader.ImageArrayedMode GetArrayed()
    {
      return (Shader.ImageArrayedMode)GetValueParameter(2);
    }

    public Shader.ImageMultiSampledMode GetMultiSampled()
    {
      return (Shader.ImageMultiSampledMode)GetValueParameter(3);
    }

    public Shader.ImageSampledMode GetSampledMode()
    {
      return (Shader.ImageSampledMode)GetValueParameter(4);
    }

    public Shader.ImageFormat GetFormat()
    {
      return (Shader.ImageFormat)GetValueParameter(5);
    }

    UInt32 GetValueParameter(int paramIndex)
    {
      var constantLiteral = Type.mParameters[paramIndex] as ShaderConstantLiteral;
      if (constantLiteral == null)
        throw new Exception("Parameter should be a constant literal");
      return (UInt32)constantLiteral.mValue;
    }

    public ShaderType Type;
  }
}
