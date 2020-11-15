using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSShaders
{
  /// <summary>
  /// Pass to collect declarations of primitive types. This processes intrinsic primitives in particular
  /// since they don't visit certain elements a normal struct type would (e.g. preconstructors)
  /// </summary>
  class FrontEndPrimitiveDeclarationCollector : FrontEndPass
  {
    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      // Only visit primitive types
      var shaderType = FindDeclaredType(node);
      if (!shaderType.IsPrimitiveType())
        return;

      mContext.mCurrentType = shaderType;

      // Walk all children (functions and fields)
      base.VisitStructDeclaration(node);

      mContext.mCurrentType = null;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      // Visit any remaining methods (not intrinsics) on the primitive type. This is necessary for any helper functions to work
      var attributes = mFrontEnd.ParseAttributes(GetDeclaredSymbol(node));
      if (IsIntrinsic(attributes))
        return;

      var returnType = FindType(node.ReturnType);
      CreateFunction(node, node.Identifier.Text, returnType);
    }
  }
}
