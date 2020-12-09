using Microsoft.CodeAnalysis;

namespace CSShaders
{
  /// <summary>
  /// Resolver for creating a sampler primitive type.
  /// </summary>
  class SamplerResolvers
  {
    public static ShaderType ProcessSamplerType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var shaderType = translator.CreateType(typeSymbol, OpType.Sampler);
      shaderType.mStorageClass = StorageClass.UniformConstant;
      return shaderType;
    }
  }
}
