using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Special resolvers for int intrinsic types
  /// </summary>
  class IntegerResolvers
  {
    public static ShaderType ProcessIntegerType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var width = (UInt32)attribute.ConstructorArguments[0].Value;
      var signed = (bool)attribute.ConstructorArguments[1].Value;

      var shaderType = translator.CreateType(typeSymbol, OpType.Int);
      shaderType.mParameters.Add(translator.CreateConstantLiteral(width));
      var signedness = translator.CreateConstantLiteral<uint>(signed ? 1u : 0u);
      shaderType.mParameters.Add(signedness);

      return shaderType;
    }
  }
}
