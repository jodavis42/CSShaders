using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Some types, functions, methods need to be handled specially (typically intrinsics). This is a unified place to handle the callback registration and some common helpers.
  /// </summary>
  public class SpecialResolvers
  {
    public delegate ShaderType SpecialTypeCreationAttributeProcessor(FrontEndTranslator frontEnd, INamedTypeSymbol typeSymbol, AttributeData attribute);
    public delegate void FieldAttributeProcessor(FrontEndTranslator frontEnd, INamedTypeSymbol typeSymbol, ISymbol member, AttributeData attribute);
    public delegate void MethodAttributeProcessor(FrontEndTranslator frontEnd, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute);
    
    public Dictionary<string, SpecialTypeCreationAttributeProcessor> SpecialTypeCreationAttributeProcessors = new Dictionary<string, SpecialTypeCreationAttributeProcessor>();
    public Dictionary<string, FieldAttributeProcessor> FieldProcessors = new Dictionary<string, FieldAttributeProcessor>();
    public Dictionary<string, MethodAttributeProcessor> MethodProcessors = new Dictionary<string, MethodAttributeProcessor>();

    public SpecialResolvers()
    {
      // Setup a few special processing functors here
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Math.IntegerPrimitive>(), IntegerResolvers.ProcessIntegerType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Math.FloatPrimitive>(), FloatResolvers.ProcessFloatType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Math.VectorPrimitive>(), VectorResolvers.ProcessVectorType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Math.MatrixPrimitive>(), MatrixResolvers.ProcessMatrixType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Shader.SamplerPrimitive>(), SamplerResolvers.ProcessSamplerType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Shader.ImagePrimitive>(), ImageResolvers.ProcessImageType);
      SpecialTypeCreationAttributeProcessors.Add(TypeAliases.GetFullTypeName<Shader.SampledImagePrimitive>(), SampledSamplerResolvers.ProcessSampledImageType);
      FieldProcessors.Add(TypeAliases.GetFullTypeName<Math.Swizzle>(), VectorResolvers.ProcessVectorSwizzle);
      MethodProcessors.Add(TypeAliases.GetFullTypeName<Shader.SimpleIntrinsicFunction>(), CreateSimpleIntrinsicType);
      MethodProcessors.Add(TypeAliases.GetFullTypeName<Math.CompositeConstruct>(), CreateCompositeConstructIntrinsic);
      MethodProcessors.Add(TypeAliases.GetFullTypeName<Shader.SampledImageIntrinsicFunction>(), SampledImageIntrinsicResolvers.CreateSampledImageIntrinsicFunction);
      MethodProcessors.Add(TypeAliases.GetFullTypeName<Shader.ImageIntrinsicFunction>(), ImageIntrinsicResolvers.CreateImageIntrinsicFunction);
      MethodProcessors.Add(TypeAliases.GetFullTypeName<Shader.SimpleExtensionIntrinsic>(), ExtensionIntrinsicResolvers.ProcessSimpleExtensionIntrinsic);
    }

    public bool TryProcessIntrinsicMethod(FrontEndTranslator translator, IMethodSymbol methodSymbol)
    {
      foreach (var fieldAttribute in methodSymbol.GetAttributes())
      {
        var attributeName = TypeAliases.GetFullTypeName(fieldAttribute.AttributeClass);
        var processor = MethodProcessors.GetValueOrDefault(attributeName);
        if (processor != null)
        {
          processor?.Invoke(translator, methodSymbol.ContainingType, methodSymbol, fieldAttribute);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Creates a simple intrinsic type (one whos ops are just value types of the args one-for-one) callback for the given symbol information from the provided attribute.
    /// </summary>
    static public void CreateSimpleIntrinsicType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var opTypeStr = attribute.ConstructorArguments[0].Value.ToString();
      var opType = (OpInstructionType)Enum.Parse(typeof(OpInstructionType), opTypeStr, true);
      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));

      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        SimpleValueTypeIntrinsic(translator, context, opType, shaderReturnType, arguments);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    /// <summary>
    /// Creates a simple intrinsic function (one whos ops are just value types of the args one-for-one) callback for the given symbol information.
    /// </summary>
    static public void CreateSimpleIntrinsicFunction(FrontEndTranslator translator, FunctionKey functionKey, OpInstructionType opType, ShaderType returnType)
    {
      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        SimpleValueTypeIntrinsic(translator, context, opType, returnType, arguments);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(functionKey, callback);
    }

    static public void CreateCompositeConstructIntrinsic(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var shaderReturnType = translator.FindType(new TypeKey(typeSymbol));

      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ProcessCompositeConstructIntrinsic(translator, shaderReturnType, arguments, context);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    static void ProcessCompositeConstructIntrinsic(FrontEndTranslator translator, ShaderType returnType, List<IShaderIR> arguments,FrontEndContext context)
    {
      // Extract how many elements this composite is made up of
      var compositeCountLiteral = returnType.mParameters[1] as ShaderConstantLiteral;
      var compositeCount = (uint)compositeCountLiteral.mValue;

      var valueOps = new List<IShaderIR>();
      // If this is a splat constructor (only 1 arg) then splat the argument for each composite element
      if (arguments.Count == 1)
      {
        var splatArgument = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[0]);
        for (uint i = 0; i < compositeCount; ++i)
          valueOps.Add(splatArgument);
      }
      // Otherwise, just copy all args over (@JoshD: Needs validation)
      else
      {
        foreach (var argument in arguments)
          valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, argument));
      }
      var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeConstruct, returnType, valueOps);
      context.Push(op);
    }

    /// <summary>
    /// Does a simple intrinsic (one where all ops are value types).
    /// </summary>
    static void SimpleValueTypeIntrinsic(FrontEndTranslator translator, FrontEndContext context, OpInstructionType opType, ShaderType returnType, List<IShaderIR> arguments)
    {
      var valueOps = new List<IShaderIR>();
      foreach (var argument in arguments)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, argument));
      var op = translator.CreateOp(context.mCurrentBlock, opType, returnType, valueOps);
      context.Push(op);
    }
  }
}
