using Microsoft.CodeAnalysis;

namespace CSShaders
{
  /// <summary>
  /// Resolver for creating a sampled image primitive type.
  /// </summary>
  class SampledSamplerResolvers
  {
    public static ShaderType ProcessSampledImageType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var imageType = translator.FindType(new TypeKey(attribute.ConstructorArguments[0].Value as ITypeSymbol));
      // Validate the image type param is an image type
      if (imageType == null || imageType.mBaseType != OpType.Image)
        throw new System.Exception();

      var shaderType = translator.CreateType(typeSymbol, OpType.SampledImage);
      shaderType.mParameters.Add(imageType);
      shaderType.mStorageClass = StorageClass.UniformConstant;
      return shaderType;
    }
  }
}
