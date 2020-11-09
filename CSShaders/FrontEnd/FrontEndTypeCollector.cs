using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CSShaders
{
  /// <summary>
  /// Pass to collect types from the syntax tree. Collecting types is typically the first pass so that subsequent passes can use any type.
  /// </summary>
  class FrontEndTypeCollector : FrontEndPass
  {
    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
      if(!IsValidClassDeclaration(node))
        AddError(node, "Keyword 'class' not allowed. Use 'struct' instead");
    }

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      var structSymbol = GetDeclaredSymbol(node);
      var shaderType = CreateType(structSymbol, OpType.Struct);
      ParseAttributes(shaderType.mMeta, structSymbol);
      ExtractDebugInfo(shaderType, structSymbol, node);
    }
  }
}
