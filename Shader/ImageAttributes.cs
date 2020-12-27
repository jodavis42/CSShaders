using System;

namespace Shader
{
  public enum ImageDimension { Dim1D = 0, Dim2D = 1, Dim3D = 2, Cube = 3, Rect = 4, Buffer = 5, SubpassData = 6}
  public enum ImageDepthMode { None = 0, Depth = 1, Unknown = 2}
  public enum ImageArrayedMode { None = 0, Arrayed = 1}
  public enum ImageMultiSampledMode { SingleSampled = 0, MultieSampled = 1}
  public enum ImageSampledMode { RunTime = 0, Sampling = 1, NoSampling = 2}
  public enum ImageFormat { Unknown = 0, Rgba32f = 1, Rgba16f = 2, R32f = 3, Rgba8 = 4, Rgba32i = 21}

  /// <summary>
  /// Declares that a type is actually a sampler primitive.
  /// </summary>
  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false)]
  public class SamplerPrimitive : Attribute
  {
  }

  /// <summary>
  /// Declares that a type is actually an image primitive.
  /// </summary>
  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false)]
  public class ImagePrimitive : Attribute
  {
    Type SampledType;
    ImageDimension Dimension = ImageDimension.Dim2D;
    ImageDepthMode DepthMode = ImageDepthMode.None;
    ImageArrayedMode ArrayedMode = ImageArrayedMode.None;
    ImageMultiSampledMode MultiSampledMode = ImageMultiSampledMode.SingleSampled;
    ImageSampledMode SampledMode = ImageSampledMode.RunTime;
    ImageFormat ImageFormat = ImageFormat.Unknown;

    public ImagePrimitive(Type sampledType, ImageDimension dimension, ImageFormat imageFormat)
    {
      SampledType = sampledType;
      Dimension = dimension;
      ImageFormat = imageFormat;
    }

    public ImagePrimitive(Type sampledType, ImageDimension dimension, ImageDepthMode depthMode, ImageArrayedMode arrayedMode, ImageMultiSampledMode multiSampledMode, ImageSampledMode sampledMode, ImageFormat imageFormat)
    {
      SampledType = sampledType;
      Dimension = dimension;
      DepthMode = depthMode;
      ArrayedMode = arrayedMode;
      MultiSampledMode = multiSampledMode;
      SampledMode = sampledMode;
      ImageFormat = imageFormat;
    }
  }

  /// <summary>
  /// Declares that a type is actually a sampled image primitive. This is constructed from an image primitive type.
  /// </summary>
  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false)]
  public class SampledImagePrimitive : Attribute
  {
    Type ImageType;

    public SampledImagePrimitive(Type imageType)
    {
      ImageType = imageType;
    }
  }

  /// <summary>
  /// Flags specifying all available extra image operands. This is for use with image intrinsic functions.
  /// </summary>
  [Flags]
  public enum ImageOperands
  {
    None = 0,
    Bias = 0x1,
    Lod = 0x2,
    Grad = 0x4,
    ConstOffset = 0x8,
    Offset = 0x10,
    ConstOffsets = 0x20,
    Sample = 0x40,
    MinLod = 0x80,
  }

  /// <summary>
  /// Specifies an intrinsic function for a  sampled image. Image intrinsics are more complicated as they might take a mask
  /// that controls what extra parameters exist. This mask isn't always at a consistent location like the end of the function either,
  /// so the location was added for user control. The location specifies what function argument this should be placed before,
  /// so if location is 2 and there are 3 arguments then it's: [arg0, arg1, operandsMask, arg2].
  /// If no mask and location are specified, then no mask is written (not all intrinsics specify a mask either).
  /// A SampledImage intrinsic function expects the first parameter to either be a sampled image, or the first two to be an image and a sampler.
  /// </summary>
  public class SampledImageIntrinsicFunction : Attribute
  {
    public string OpName;
    public ImageOperands Operands = ImageOperands.None;
    public UInt32 OperandsLocation = 0;

    public SampledImageIntrinsicFunction(string opName)
    {
      OpName = opName;
    }

    public SampledImageIntrinsicFunction(string opName, ImageOperands operands, UInt32 operandsLocation)
    {
      OpName = opName;
      Operands = operands;
      OperandsLocation = operandsLocation;
    }
  }

  /// <summary>
  /// Specifies an intrinsic function for an image. Image intrinsics are more complicated as they may take a mask
  /// that controls what extra parameters exist. This mask isn't always at a consistent location like the end of the function either,
  /// so the location was added for user control. The location specifies what function argument this should be placed before,
  /// so if location is 2 and there are 3 arguments then it's: [arg0, arg1, operandsMask, arg2].
  /// If no mask and location are specified, then no mask is written (not all intrinsics specify a mask either).
  /// A Image intrinsic function expects the first parameter to either be an image or a sampled image.
  /// </summary>
  public class ImageIntrinsicFunction : Attribute
  {
    public string OpName;
    public ImageOperands Operands = ImageOperands.None;
    public UInt32 OperandsLocation = 0;

    public ImageIntrinsicFunction(string opName)
    {
      OpName = opName;
    }

    public ImageIntrinsicFunction(string opName, ImageOperands operands, UInt32 operandsLocation)
    {
      OpName = opName;
      Operands = operands;
      OperandsLocation = operandsLocation;
    }
  }
}
