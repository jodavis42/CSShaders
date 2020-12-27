using System.Collections.Generic;

namespace IntrinsicsGenerators
{
  public class ImageIntrinsics
  {
    public static IntrinsicGroup Generate(TypeGroups typeGroups)
    {
      var imageIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Image" };
      imageIntrinsicsGroup.GroupComment = "// Image Intrinsics ------------------------------------------------------";

      var imageSet = new ImageSamplerSet { SamplerType = typeGroups.SamplerType, ImageType = typeGroups.FloatImage2dType, SampledImageType = typeGroups.FloatSampledImage2dType };
      GenerateSampleImplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleExplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleGradExplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleDrefImplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleDrefExplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleProjImplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleProjExplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleProjDrefImplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateSampleProjDrefExplicitLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateImageFetch(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateImageFetchLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateImageQuerySizeLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateImageQueryLod(typeGroups, imageIntrinsicsGroup, imageSet);
      GenerateImageQueryLevels(typeGroups, imageIntrinsicsGroup, imageSet);

      return imageIntrinsicsGroup;
    }

    static IntrinsicDescription GenerateSampleImplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription> { new ParameterDescription(typeGroups.Float2Type, "coordinates") };
      var opImageSampleImplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleImplicitLod", "SampleImplicitLod", "ImageOperands.None", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageSampleImplicitLod.AddIntrinsicComment("Sample an image with an implicit level of detail.");
      return opImageSampleImplicitLod;
    }

    static IntrinsicDescription GenerateSampleExplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription> { new ParameterDescription(typeGroups.Float2Type, "coordinates"), new ParameterDescription(typeGroups.FloatType, "lod") };
      var opImageSampleExplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleExplicitLod", "SampleExplicitLod", "ImageOperands.Lod", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageSampleExplicitLod.AddIntrinsicComment("Sample an image with an explicit level of detail.");
      return opImageSampleExplicitLod;
    }

    static IntrinsicDescription GenerateSampleGradExplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float2Type, "coordinates"),
        new ParameterDescription(typeGroups.Float2Type, "ddx"),
        new ParameterDescription(typeGroups.Float2Type, "ddy")
      };
      var opImageSampleExplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleExplicitLod", "SampleGradExplicitLod", "ImageOperands.Grad", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageSampleExplicitLod.AddIntrinsicComment("Sample an image with an explicit level of detail. The explicit derivatvies of ddx and ddy are used to compute the lod.");
      return opImageSampleExplicitLod;
    }

    static IntrinsicDescription GenerateSampleDrefImplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float2Type, "coordinates"),
        new ParameterDescription(typeGroups.FloatType, "depth")
      };
      var opImageSampleDrefImplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleDrefImplicitLod", "SampleDrefImplicitLod", "ImageOperands.None", 3, typeGroups.FloatType, set, additionalArgs);
      opImageSampleDrefImplicitLod.AddIntrinsicComment("Sample an image doing depth-comparison with an implicit level of detail.");
      return opImageSampleDrefImplicitLod;
    }

    static IntrinsicDescription GenerateSampleDrefExplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float2Type, "coordinates"),
        new ParameterDescription(typeGroups.FloatType, "depth"),
        new ParameterDescription(typeGroups.FloatType, "lod")
      };
      var opImageSampleDrefExplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleDrefExplicitLod", "SampleDrefExplicitLod", "ImageOperands.Lod", 3, typeGroups.FloatType, set, additionalArgs);
      opImageSampleDrefExplicitLod.AddIntrinsicComment("Sample an image doing depth-comparison using an explicit level of detail.");
      return opImageSampleDrefExplicitLod;
    }
    
    static IntrinsicDescription GenerateSampleProjImplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float3Type, "coordinates")
      };
      var opImageSampleProjImplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleProjImplicitLod", "SampleProjImplicitLod", "ImageOperands.None", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageSampleProjImplicitLod.AddIntrinsicComment("Sample an image with with a project coordinate and an implicit level of detail.");
      opImageSampleProjImplicitLod.AddIntrinsicComment("Coordinates is a floating-point vector containing (u, v, q), with the q component consumed for the projective division.");
      opImageSampleProjImplicitLod.AddIntrinsicComment("That is, the actual sample coordinate will be (u/q, v/q]).");
      return opImageSampleProjImplicitLod;
    }
    
    static IntrinsicDescription GenerateSampleProjExplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float3Type, "coordinates"),
        new ParameterDescription(typeGroups.FloatType, "lod")
      };
      var opImageSampleProjExplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleProjExplicitLod", "SampleProjExplicitLod", "ImageOperands.Lod", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageSampleProjExplicitLod.AddIntrinsicComment("Sample an image with a project coordinate using an explicit level of detail.");
      opImageSampleProjExplicitLod.AddIntrinsicComment("Coordinates is a floating-point vector containing (u, v, q), with the q component consumed for the projective division.");
      opImageSampleProjExplicitLod.AddIntrinsicComment("That is, the actual sample coordinate will be (u/q, v/q]).");
      return opImageSampleProjExplicitLod;
    }
    
    static IntrinsicDescription GenerateSampleProjDrefImplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float3Type, "coordinates"),
        new ParameterDescription(typeGroups.FloatType, "depth")
      };
      var opImageSampleProjDrefImplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleProjDrefImplicitLod", "SampleProjDrefImplicitLod", "ImageOperands.None", 3, typeGroups.FloatType, set, additionalArgs);
      opImageSampleProjDrefImplicitLod.AddIntrinsicComment("Sample an image with a project coordinate, doing depth-comparison, with an implicit level of detail.");
      opImageSampleProjDrefImplicitLod.AddIntrinsicComment("Coordinates is a floating-point vector containing (u, v, q), with the q component consumed for the projective division.");
      opImageSampleProjDrefImplicitLod.AddIntrinsicComment("That is, the actual sample coordinate will be (u/q, v/q]).");
      return opImageSampleProjDrefImplicitLod;
    }
    
    static IntrinsicDescription GenerateSampleProjDrefExplicitLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Float3Type, "coordinates"),
        new ParameterDescription(typeGroups.FloatType, "depth"),
        new ParameterDescription(typeGroups.FloatType, "lod")
      };
      var opImageSampleProjDrefExplicitLod = GenerateImageSamplerIntrinsic(group, "OpImageSampleProjDrefExplicitLod", "SampleProjDrefExplicitLod", "ImageOperands.Lod", 3, typeGroups.FloatType, set, additionalArgs);
      opImageSampleProjDrefExplicitLod.AddIntrinsicComment("Sample an image with a project coordinate, doing depth-comparison, using an explicit level of detail.");
      opImageSampleProjDrefExplicitLod.AddIntrinsicComment("Coordinates is a floating-point vector containing (u, v, q), with the q component consumed for the projective division.");
      opImageSampleProjDrefExplicitLod.AddIntrinsicComment("That is, the actual sample coordinate will be (u/q, v/q]).");
      return opImageSampleProjDrefExplicitLod;
    }
    
    static IntrinsicDescription GenerateImageFetch(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      { 
        new ParameterDescription(typeGroups.Int2Type, "coordinates")
      };
      var opImageFetch = GenerateImageIntrinsic(group, "OpImageFetch", "ImageFetch", "ImageOperands.None", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageFetch.AddIntrinsicComment("Fetch a single texel from an image.");
      return opImageFetch;
    }

    static IntrinsicDescription GenerateImageFetchLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      {
        new ParameterDescription(typeGroups.Int2Type, "coordinates"),
        new ParameterDescription(typeGroups.IntType, "lod")
      };
      var opImageFetch = GenerateImageIntrinsic(group, "OpImageFetch", "ImageFetchLod", "ImageOperands.Lod", 2, typeGroups.Float4Type, set, additionalArgs);
      opImageFetch.AddIntrinsicComment("Fetch a single texel from an image with an explicit lod level.");
      return opImageFetch;
    }

    static IntrinsicDescription GenerateImageQuerySizeLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>();
      var opImageQuerySizeLod = GenerateImageIntrinsic(group, "OpImageQuerySizeLod", "QuerySizeLod", "ImageOperands.None", 1, typeGroups.Int2Type, set, additionalArgs);
      opImageQuerySizeLod.Disabled = true;
      opImageQuerySizeLod.AddIntrinsicComment("Requires Capabilities: ImageQuery.");
      opImageQuerySizeLod.AddIntrinsicComment("Query the dimensions of 'Image' for mipmap level for 'Level of Detail'.");
      return opImageQuerySizeLod;
    }

    static IntrinsicDescription GenerateImageQueryLod(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>
      {
        new ParameterDescription(typeGroups.Float2Type, "coordinates"),
      };
      var opImageQueryLod = GenerateImageSamplerIntrinsic(group, "OpImageQueryLod", "QueryLod", "ImageOperands.None", 1, typeGroups.Float2Type, set, additionalArgs);
      opImageQueryLod.Disabled = true;
      opImageQueryLod.AddIntrinsicComment("Requires Capabilities: ImageQuery.");
      opImageQueryLod.AddIntrinsicComment("Query the mipmap level and the level of detail for a hypothetical sampling of 'Image' at Coordinate using an implicit level of detail.");
      return opImageQueryLod;
    }

    static IntrinsicDescription GenerateImageQueryLevels(TypeGroups typeGroups, IntrinsicGroup group, ImageSamplerSet set)
    {
      var additionalArgs = new List<ParameterDescription>();
      var opImageQueryLevels = GenerateImageIntrinsic(group, "OpImageQueryLevels", "QueryLevels", "ImageOperands.None", 1, typeGroups.IntType, set, additionalArgs);
      opImageQueryLevels.Disabled = true;
      opImageQueryLevels.AddIntrinsicComment("Requires Capabilities: ImageQuery.");
      opImageQueryLevels.AddIntrinsicComment("Query the number of mipmap levels accessible through Image.");
      return opImageQueryLevels;
    }

    static IntrinsicDescription GenerateImageSamplerIntrinsic(IntrinsicGroup group, string opName, string fnName, string optionalArgs, int operandsLocation, TypeName returnType, ImageSamplerSet set, List<ParameterDescription> additionalArgs)
    {
      var intrinsic = group.Build(opName, fnName);
      // Use the function name for the test. This is because there are multiple intrinsics that
      // have the same op but different masks and they need to not overwrite each other in tests.
      intrinsic.TestName = fnName;

      var splitParams = new List<ParameterDescription>();
      splitParams.Add(new ParameterDescription() { TypeName = set.ImageType, ParameterName = "image" });
      splitParams.Add(new ParameterDescription() { TypeName = set.SamplerType, ParameterName = "sampler" });
      splitParams.AddRange(additionalArgs);
      var splitSignature = intrinsic.AddSignature(returnType, splitParams);
      if (operandsLocation != -1)
        splitSignature.SetAttributes(new SampledImageIntrinsicFunctionDescription(opName, optionalArgs, operandsLocation + 1, fnName));
      else
        splitSignature.SetAttributes(new SampledImageIntrinsicFunctionDescription(opName, fnName));

      var combinedParams = new List<ParameterDescription>();
      combinedParams.Add(new ParameterDescription() { TypeName = set.SampledImageType, ParameterName = "sampledImage" });
      combinedParams.AddRange(additionalArgs);
      var combinedSignature = intrinsic.AddSignature(returnType, combinedParams);
      if (operandsLocation != -1)
        combinedSignature.SetAttributes(new SampledImageIntrinsicFunctionDescription(opName, optionalArgs, operandsLocation, fnName));
      else
        combinedSignature.SetAttributes(new SampledImageIntrinsicFunctionDescription(opName, fnName));
      return intrinsic;
    }

    static IntrinsicDescription GenerateImageIntrinsic(IntrinsicGroup group, string opName, string fnName, string optionalArgs, int operandsLocation, TypeName returnType, ImageSamplerSet set, List<ParameterDescription> additionalArgs)
    {
      var intrinsic = group.Build(opName, fnName);
      // Use the function name for the test. This is because there are multiple intrinsics that
      // have the same op but different masks and they need to not overwrite each other in tests.
      intrinsic.TestName = fnName;

      var imageParams = new List<ParameterDescription>();
      imageParams.Add(new ParameterDescription() { TypeName = set.ImageType, ParameterName = "image" });
      imageParams.AddRange(additionalArgs);
      var splitSignature = intrinsic.AddSignature(returnType, imageParams);
      if (operandsLocation != -1)
        splitSignature.SetAttributes(new ImageIntrinsicFunctionDescription(opName, optionalArgs, operandsLocation, fnName));
      else
        splitSignature.SetAttributes(new ImageIntrinsicFunctionDescription(opName, fnName));

      var combinedParams = new List<ParameterDescription>();
      combinedParams.Add(new ParameterDescription() { TypeName = set.SampledImageType, ParameterName = "sampledImage" });
      combinedParams.AddRange(additionalArgs);
      var combinedSignature = intrinsic.AddSignature(returnType, combinedParams);
      if (operandsLocation != -1)
        combinedSignature.SetAttributes(new ImageIntrinsicFunctionDescription(opName, optionalArgs, operandsLocation, fnName));
      else
        combinedSignature.SetAttributes(new ImageIntrinsicFunctionDescription(opName, fnName));
      return intrinsic;
    }
  }
}
