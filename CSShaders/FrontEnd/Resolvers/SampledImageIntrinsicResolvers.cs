using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class SampledImageIntrinsicResolvers
  {
    static public void CreateSampledImageIntrinsicFunction(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var intrinsicData = ImageIntrinsicAttributeData.Parse(attribute);
      if (intrinsicData == null)
      {
        throw new Exception("Failed to parse attribute");
      }

      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));
      ShaderLibrary.InstrinsicDelegate callback = null;

      var param0Type = translator.FindType(new TypeKey(methodSymbol.Parameters[0].Type));
      if (param0Type.mBaseType == OpType.SampledImage)
      {
        callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
        {
          SampledImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
        };
      }
      else if (param0Type.mBaseType == OpType.Image)
      {
        var param1Type = translator.FindType(new TypeKey(methodSymbol.Parameters[1].Type));
        if (param1Type.mBaseType == OpType.Sampler)
        {
          callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
          {
            SplitSampledImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
          };
        }
      }

      if (callback == null)
        throw new Exception("Failed to parse attribute");

      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    static void SampledImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicAttributeData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();
      WriteArguments(translator, context, valueOps, intrinsicData, arguments, 0);
      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }

    static void SplitSampledImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicAttributeData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
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
      WriteArguments(translator, context, valueOps, intrinsicData, arguments, 2);
      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }

    static void WriteArguments(FrontEndTranslator translator, FrontEndContext context, List<IShaderIR> valueOps, ImageIntrinsicAttributeData intrinsicData, List<IShaderIR> arguments, int startIndex)
    {
      // If there's no operands location, then just write the arguments as is. It's only if there's a mask and location that we write this (since not all image ops have a mask).
      if(intrinsicData.OperandsLocation == -1)
      {
        for (var i = startIndex; i < arguments.Count; ++i)
          valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));
      }
      // Otherwise, add all of the given arguments, but put the operands mask in-between based upon the specified location.
      // This location isn't consistent for all image functions so it became an exposed value.
      else
      {
        for (var i = startIndex; i < intrinsicData.OperandsLocation && i < arguments.Count; ++i)
          valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));
        valueOps.Add(translator.CreateConstantLiteral((UInt32)intrinsicData.Operands));
        for (var i = intrinsicData.OperandsLocation; i < arguments.Count; ++i)
          valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[i]));
      }
    }
  }
}
