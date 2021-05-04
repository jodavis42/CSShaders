namespace Shader
{
  /// <summary>
  /// The basic sampler primitive. Can be declared as an input and combined at runtime to sample an image.
  /// </summary>
  [SamplerPrimitive]
  public struct Sampler
  {
  }

  /// <summary>
  /// A 2d float image. This is the the standard image for use with a 2d texture. Can be combined with a sampler at runtime.
  /// </summary>
  [ImagePrimitive(typeof(float), ImageDimension.Dim2D, ImageFormat.Unknown)]
  public struct FloatImage2d
  {
  }

  [ImagePrimitive(typeof(int), ImageDimension.Dim2D, ImageFormat.Unknown)]
  public struct IntImage2d
  {
  }

  /// <summary>
  /// A combined 2d float image and sampler. All run-time sampling of an image must be done via a sampled image. 
  /// For convenience, a sampled image is a primitive that can also be bound to a shader so that run-time combinations don't have to be made.
  /// </summary>
  [SampledImagePrimitive(typeof(FloatImage2d))]
  public struct FloatSampledImage2d
  {
  }
}
