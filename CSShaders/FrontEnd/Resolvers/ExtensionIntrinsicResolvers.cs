using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ExtensionIntrinsicResolvers
  {
    /// <summary>
    /// Processes a function to create a simple extension intrinsic (one whos ops are just value types of the args one-for-one) callback for the given symbol information from the provided attribute.
    /// </summary>
    static public void ProcessSimpleExtensionIntrinsic(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol, AttributeData attribute)
    {
      var extensionName = attribute.ConstructorArguments[0].Value.ToString();
      var opTypeStr = attribute.ConstructorArguments[1].Value.ToString();
      // @JoshD: Hack that this is tied to GLSLstd450. This should ideally be extended to work  on any extension library type
      var extOpType = (GLSLstd450)Enum.Parse(typeof(GLSLstd450), opTypeStr, true);
      var shaderReturnType = translator.FindType(new TypeKey(methodSymbol.ReturnType));

      ShaderLibrary.InstrinsicDelegate callback = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ResolveSimpleExtensionIntrinsic(translator, context, extensionName, (int)extOpType, shaderReturnType, arguments);
      };
      translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(methodSymbol), callback);
    }

    /// <summary>
    /// Does a simple extension intrinsic (one where all ops are value types).
    /// </summary>
    static void ResolveSimpleExtensionIntrinsic(FrontEndTranslator translator, FrontEndContext context, string extensionName, int extOpType, ShaderType returnType, List<IShaderIR> arguments)
    {
      var extensionImportOp = translator.mCurrentLibrary.GetOrCreateExtensionLibraryImport(extensionName);
      var extOpTypeOp = translator.CreateConstantLiteral(extOpType);

      var valueOps = new List<IShaderIR>();
      valueOps.Add(extensionImportOp);
      valueOps.Add(extOpTypeOp);
      foreach (var argument in arguments)
        valueOps.Add(translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, argument));
      var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpExtInst, returnType, valueOps);
      context.Push(op);
    }
  }
}
