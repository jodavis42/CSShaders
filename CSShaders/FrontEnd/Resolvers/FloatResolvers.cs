using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Special resolvers for float intrinsic types
  /// </summary>
  class FloatResolvers
  {
    public static ShaderType ProcessFloatType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var width = (UInt32)attribute.ConstructorArguments[0].Value;

      var shaderType = translator.CreateType(typeSymbol, OpType.Float);
      shaderType.mParameters.Add(translator.CreateConstantLiteral(width));

      return shaderType;
    }
  }
}
