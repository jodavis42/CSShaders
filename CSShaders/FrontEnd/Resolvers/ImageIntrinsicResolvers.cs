using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ImageIntrinsicResolvers
  {
    static public void CreateImageIntrinsicFunction(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var intrinsicData = ImageIntrinsicAttributeData.Parse(attribute);
      if (intrinsicData == null)
      {
        throw new Exception("Failed to parse attribute");
      }

      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));
      ShaderLibrary.InstrinsicDelegate callback = null;

      var param0Type = translator.FindType(new TypeKey(methodSymbol.Parameters[0].Type));
      if (param0Type.mBaseType == OpType.Image)
      {
        callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
        {
          ImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
        };
      }
      else if (param0Type.mBaseType == OpType.SampledImage)
      {
        callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
        {
          CombinedSampledImageValueTypeIntrinsic(translator, context, intrinsicData, shaderReturnType, arguments);
        };
      }

      if (callback == null)
        throw new Exception("Failed to parse attribute");

      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    static void ImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicAttributeData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();
      WriteArguments(translator, context, valueOps, intrinsicData, arguments, 0);
      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }

    static void CombinedSampledImageValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, ImageIntrinsicAttributeData intrinsicData, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();

      var sampledImageParam = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[0]);
      var sampledImageType = sampledImageParam.mResultType;
      var imageType = sampledImageType.mParameters[0] as ShaderType;
      
      // Pull the image out of the sampled image.
      var imageOp = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpImage, imageType, new List<IShaderIR>() { sampledImageParam });
      valueOps.Add(imageOp);
      WriteArguments(translator, context, valueOps, intrinsicData, arguments, 1);
      var op = translator.CreateOp(context.mCurrentBlock, intrinsicData.OpType, returnType, valueOps);
      context.Push(op);
    }

    static void WriteArguments(FrontEndTranslator translator, FrontEndContext context, List<IShaderIR> valueOps, ImageIntrinsicAttributeData intrinsicData, List<IShaderIR> arguments, int startIndex)
    {
      // If there's no operands location, then just write the arguments as is. It's only if there's a mask and location that we write this (since not all image ops have a mask).
      if (intrinsicData.OperandsLocation == -1)
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
