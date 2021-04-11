using Microsoft.CodeAnalysis;
using System;


namespace CSShaders
{
  /// <summary>
  /// Resolver for creating an image primitive type.
  /// </summary>
  public class ImageResolvers
  {
    public static ShaderType ProcessImageType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      ShaderType sampledType = null;
      Shader.ImageDimension dimension = Shader.ImageDimension.Dim2D;
      Shader.ImageDepthMode depthMode = Shader.ImageDepthMode.None;
      Shader.ImageArrayedMode arrayedMode = Shader.ImageArrayedMode.None;
      Shader.ImageMultiSampledMode multiSampledMode = Shader.ImageMultiSampledMode.SingleSampled;
      Shader.ImageSampledMode sampledMode = Shader.ImageSampledMode.Sampling;
      Shader.ImageFormat imageFormat = Shader.ImageFormat.Unknown;

      // Load all constructor args by type.
      foreach(var argument in attribute.ConstructorArguments)
      {
        var argName = TypeAliases.GetTypeName(argument.Type);
        if (argName == TypeAliases.GetTypeName<Type>())
          sampledType = translator.FindType(new TypeKey(argument.Value as ITypeSymbol));
        else if (argName == TypeAliases.GetTypeName<Shader.ImageDimension>())
          dimension = (Shader.ImageDimension)argument.Value;
        else if (argName == TypeAliases.GetTypeName<Shader.ImageDepthMode>())
          depthMode = (Shader.ImageDepthMode)argument.Value;
        else if (argName == TypeAliases.GetTypeName<Shader.ImageArrayedMode>())
          arrayedMode = (Shader.ImageArrayedMode)argument.Value;
        else if (argName == TypeAliases.GetTypeName<Shader.ImageMultiSampledMode>())
          multiSampledMode = (Shader.ImageMultiSampledMode)argument.Value;
        else if (argName == TypeAliases.GetTypeName<Shader.ImageSampledMode>())
          sampledMode = (Shader.ImageSampledMode)argument.Value;
        else if (argName == TypeAliases.GetTypeName<Shader.ImageFormat>())
          imageFormat = (Shader.ImageFormat)argument.Value;
      }
      // Handle named arguments
      foreach (var pair in attribute.NamedArguments)
      {
        if (pair.Key == "SampledType")
          sampledType = translator.FindType(new TypeKey(pair.Value.Value as ITypeSymbol));
        else if (pair.Key == "Dimension")
          dimension = (Shader.ImageDimension)pair.Value.Value;
        else if (pair.Key == "DepthMode")
          depthMode = (Shader.ImageDepthMode)pair.Value.Value;
        else if (pair.Key == "ArrayedMode")
          arrayedMode = (Shader.ImageArrayedMode)pair.Value.Value;
        else if (pair.Key == "MultiSampledMode")
          multiSampledMode = (Shader.ImageMultiSampledMode)pair.Value.Value;
        else if (pair.Key == "SampledMode")
          sampledMode = (Shader.ImageSampledMode)pair.Value.Value;
        else if (pair.Key == "ImageFormat")
          imageFormat = (Shader.ImageFormat)pair.Value.Value;
      }

      // Validate input type. SpirV spec says: "Sampled Type is the type of the components that result
      // from sampling or reading from this image type. Must be ascalar numerical type or OpTypeVoid"
      if (sampledType == null || (sampledType.mBaseType != OpType.Bool && sampledType.mBaseType != OpType.Int && sampledType.mBaseType != OpType.Float && sampledType.mBaseType != OpType.Void))
        throw new Exception();

      var shaderType = translator.CreateType(typeSymbol, OpType.Image);
      shaderType.mParameters.Add(sampledType);
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)dimension));
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)depthMode));
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)arrayedMode));
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)multiSampledMode));
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)sampledMode));
      shaderType.mParameters.Add(translator.CreateConstantLiteral((UInt32)imageFormat));
      shaderType.mStorageClass = StorageClass.UniformConstant;

      return shaderType;
    }
  }
}
