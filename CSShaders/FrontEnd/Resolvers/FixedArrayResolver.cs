using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  class FixedArrayResolver
  {
    public static ShaderType ProcessFixedArrayType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var componentType = translator.FindType(new TypeKey(attribute.ConstructorArguments[0].Value as ISymbol));
      var componentCount = (UInt32)attribute.ConstructorArguments[1].Value;

      var shaderType = translator.CreateType(typeSymbol, OpType.Array);
      shaderType.mParameters.Add(componentType);
      shaderType.mParameters.Add(translator.CreateConstantOp(componentCount));

      ProcessVectorDefaultConstructor(translator, typeSymbol, shaderType);

      return shaderType;
    }

    public static void ProcessVectorDefaultConstructor(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, ShaderType shaderType)
    {
      ShaderLibrary.InstrinsicDelegate defaultConstructorResolver = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ResolveVectorDefaultConstructor(translator, context, shaderType);
      };
      foreach (var constructor in typeSymbol.Constructors)
      {
        if (constructor.Parameters.Length == 0 && constructor.IsImplicitlyDeclared)
          translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(constructor), defaultConstructorResolver);
      }
    }

    public static void ResolveVectorDefaultConstructor(FrontEndTranslator translator, FrontEndContext context, ShaderType vectorType)
    {
      var componentType = vectorType.mParameters[0] as ShaderType;
      var componentOp = vectorType.mParameters[1] as ShaderOp;
      var componentCount = (UInt32)(componentOp.mParameters[0] as ShaderConstantLiteral).mValue;

      var zeroConstantLiteral = translator.CreateConstantLiteralZero(componentType);
      var zeroConstant = translator.CreateConstantOp(componentType, zeroConstantLiteral);

      var constructorArgs = new List<IShaderIR>();
      for (var i = 0; i < componentCount; ++i)
        constructorArgs.Add(zeroConstant);
      var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeConstruct, vectorType, constructorArgs);
      context.Push(op);
    }
  }
}
