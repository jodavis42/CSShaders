using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Special resolvers for matrix intrinsic types
  /// </summary>
  class MatrixResolvers
  {
    public static ShaderType ProcessMatrixType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      ShaderType columnType = null;
      UInt32 columnCount = 0;
      if (!ValidateMatrixPrimitive(translator, typeSymbol, attribute, out columnType, out columnCount))
        return null;

      var shaderType = translator.CreateType(typeSymbol, OpType.Matrix);
      shaderType.mParameters.Add(columnType);
      shaderType.mParameters.Add(translator.CreateConstantLiteral(columnCount));

      RegisterMatrixConstructors(translator, typeSymbol, shaderType, columnType, columnCount);

      return shaderType;
    }

    public static bool ValidateMatrixPrimitive(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute, out ShaderType columnType, out UInt32 columnCount)
    {
      columnCount = (UInt32)attribute.ConstructorArguments[1].Value;
      // The component type must be a vector type
      ISymbol componentTypeSymbol = attribute.ConstructorArguments[0].Value as ISymbol;
      columnType = translator.FindType(new TypeKey(componentTypeSymbol));
      if (columnType == null || columnType.mBaseType != OpType.Vector)
        return false;
      return true;
    }

    public static void RegisterMatrixConstructors(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, ShaderType shaderType, ShaderType columnType, UInt32 columnCount)
    {
      ShaderLibrary.InstrinsicDelegate defaultConstructorResolver = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ResolveMatrixDefaultConstructor(translator, context, shaderType);
      };
      foreach (var constructor in typeSymbol.Constructors)
      {
        if (constructor.Parameters.Length == 0 && constructor.IsImplicitlyDeclared)
          translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(constructor), defaultConstructorResolver);
      }
    }

    public static void ResolveMatrixDefaultConstructor(FrontEndTranslator translator, FrontEndContext context, ShaderType vectorType)
    {
      var componentType = vectorType.mParameters[0] as ShaderType;
      var componentCount = (UInt32)(vectorType.mParameters[1] as ShaderConstantLiteral).mValue;

      // This direct call to the vector resolver is less than ideal, but works for now
      VectorResolvers.ResolveVectorDefaultConstructor(translator, context, componentType);
      var zeroConstant = context.Pop();

      var constructorArgs = new List<IShaderIR>();
      for (var i = 0; i < componentCount; ++i)
        constructorArgs.Add(zeroConstant);
      var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeConstruct, vectorType, constructorArgs);
      context.Push(op);
    }
  }
}
