using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ImageIntrinsicResolvers
  {
    class ImageIntrinsicFunctionData
    {
      public OpInstructionType OpType = OpInstructionType.OpUndef;
      public Shader.ImageOperands Operands = Shader.ImageOperands.None;
      public UInt32 OperandsLocation = 0;
    }

    static ImageIntrinsicFunctionData ParseImageIntrinsicFunctionAttribute(AttributeData attribute, UInt32 defaultOperandsLocation = 0)
    {
      if (attribute.ConstructorArguments.Length == 0)
        return null;

      ImageIntrinsicFunctionData result = new ImageIntrinsicFunctionData();
      result.OperandsLocation = defaultOperandsLocation;
      // Load the attribute params if they exist. Note: This isn't really safe, but works for now.
      foreach (var attributeParam in attribute.ConstructorArguments)
      {
        if (attributeParam.Type.Name == typeof(string).Name)
          result.OpType = (OpInstructionType)Enum.Parse(typeof(OpInstructionType), attributeParam.Value as string, true);
        else if (attributeParam.Type.Name == typeof(Shader.ImageOperands).Name)
          result.Operands = (Shader.ImageOperands)attributeParam.Value;
        else if (attributeParam.Type.Name == typeof(UInt32).Name)
          result.OperandsLocation = (UInt32)attributeParam.Value;
      }
      // Also try to resolve any named parameter arguments
      foreach(var pair in attribute.NamedArguments)
      {
        if(pair.Key == "OpName")
          result.OpType = (OpInstructionType)Enum.Parse(typeof(OpInstructionType), pair.Value.Value as string, true);
        else if (pair.Key == "Operands")
          result.Operands = (Shader.ImageOperands)pair.Value.Value;
        else if (pair.Key == "OperandsLocation")
          result.OperandsLocation = (UInt32)pair.Value.Value;
      }
      return result;
    }

    static public void CreateSampledImageIntrinsicType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var intrinsicData = ParseImageIntrinsicFunctionAttribute(attribute, (UInt32)methodSymbol.Parameters.Length);
      if (intrinsicData == null)
      {
        throw new Exception("Failed to parse attribute");
      }

      // Verify the first argument is an image
      var param0Type = translator.FindType(new TypeKey(methodSymbol.Parameters[0].Type));
      if (param0Type == null || param0Type.mBaseType != OpType.SampledImage)
        throw new Exception();

      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));

      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    static public void CreateSplitSampledImageIntrinsicFunction(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var intrinsicData = ParseImageIntrinsicFunctionAttribute(attribute, (UInt32)methodSymbol.Parameters.Length);
      if (intrinsicData == null)
      {
        throw new Exception("Failed to parse attribute");
      }
      if(methodSymbol.Parameters.Length < 2)
      {
        throw new Exception("Expects at least two params for the image and sampler");
      }

      var param0Type = translator.FindType(new TypeKey(methodSymbol.Parameters[0].Type));
      var param1Type = translator.FindType(new TypeKey(methodSymbol.Parameters[1].Type));
      
      // Validate the method args. The first paramter must be an image that is sampled and isn't using subpass data, the 2nd must be a sampler
      var imageType = ImageType.Load(param0Type);
      if (imageType == null || param1Type.mBaseType != OpType.Sampler)
        throw new Exception();

      var imageDimension = imageType.GetDimension();
      var sampledMode = imageType.GetSampledMode();
      if(imageDimension == Shader.ImageDimension.SubpassData || sampledMode == Shader.ImageSampledMode.NoSampling)
        throw new Exception();


      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));

      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        SplitSampledImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    static void ImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicFunctionData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();

      // Add all of the given arguments, but put the operands mask in-between based upon the specified location.
      // This location isn't consistent for all image functions so it became an exposed value.
      int operandsLocation = (int)intrinsicData.OperandsLocation;
      for (var i = 0; i < operandsLocation && i < arguments.Count; ++i)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));
      valueOps.Add(translator.CreateConstantLiteral((UInt32)intrinsicData.Operands));
      for (var i = operandsLocation; i < arguments.Count; ++i)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));

      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }

    static void SplitSampledImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicFunctionData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();

      var imageParam = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[0]);
      var samplerParam = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[1]);

      // Try and generate a sampled image type for this image. If we do create a new type that wasn't necessary that's fine because it'll be de-duped in the binary backend.
      var sampledImageTypeStr = "GeneratedSampledImage_" + imageParam.mResultType.ToString();
      var sampledImageType = translator.FindType(new TypeKey(sampledImageTypeStr));
      if (sampledImageType == null)
      {
        sampledImageType = translator.CreateType(new TypeKey(sampledImageTypeStr), sampledImageTypeStr, OpType.SampledImage, null);
        sampledImageType.mParameters.Add(imageParam.mResultType.GetDereferenceType());
      }

      // Combine the image and sampler together into a sampled image. This is a bit annoying, but a sampled image
      // can't be stored into a variable so this has to be a temporary. Use this combined sampled image in place of the first two args to the intrinsics.
      var sampledImageOp = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpSampledImage, sampledImageType, new List<IShaderIR>() { imageParam, samplerParam });
      valueOps.Add(sampledImageOp);

      // Add all of the given arguments, but put the operands mask in-between based upon the specified location.
      // This location isn't consistent for all image functions so it became an exposed value.
      int operandsLocation = (int)intrinsicData.OperandsLocation;
      for (var i = 2; i < operandsLocation && i < arguments.Count; ++i)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));
      valueOps.Add(translator.CreateConstantLiteral((UInt32)intrinsicData.Operands));
      for (var i = operandsLocation; i < arguments.Count; ++i)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));

      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }
  }
}
