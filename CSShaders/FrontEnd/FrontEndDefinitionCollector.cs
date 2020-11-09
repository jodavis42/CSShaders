using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSShaders
{
  /// <summary>
  /// Visits the bodies of functions and translates them.
  /// </summary>
  public class FrontEndDefinitionCollector : FrontEndPass
  {
    FrontEndCommonPass mCommonPass = new FrontEndCommonPass();
    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      mCommonPass.mFrontEnd = mFrontEnd;
      mCommonPass.mContext = mContext;
      mContext.mCurrentType = FindDeclaredType(node);

      base.VisitStructDeclaration(node);

      mContext.mCurrentType = null;
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
      VisitMethod(node);
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      VisitMethod(node);
    }

    public void VisitMethod(BaseMethodDeclarationSyntax node)
    {
      var fnSymbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node) as IMethodSymbol;
      
      // Add the current function as the active one and the parameter
      var shaderFunction = FindFunction(node);
      mContext.mCurrentFunction = shaderFunction;

      // Start writing out the parameters (so the parameter block is the active one)
      mContext.mCurrentBlock = shaderFunction.mParametersBlock;
      // Add the 'this' op if it exists
      if (!fnSymbol.IsStatic)
      {
        var selfType = mContext.mCurrentType;
        var thisType = selfType.FindPointerType(StorageClass.Function);
        var selfOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionParameter, thisType, null);
        selfOp.DebugInfo.Name = "self";
        mContext.mThisOp = selfOp;
      }
      // Add the function parameter ops
      foreach (var parameter in node.ParameterList.Parameters)
      {
        var parameterSymbol = GetDeclaredSymbol(parameter) as IParameterSymbol;
        var parameterType = FindParameterType(parameter);
        var paramOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionParameter, parameterType, null);
        ExtractDebugInfo(paramOp, parameter);
        paramOp.DebugInfo.Name = parameterSymbol.Name;
        // Also map parameter symbols in this scope to their ops so we can look them up later when visiting identifiers
        mContext.mSymbolToIRMap.Add(parameterSymbol, paramOp);
      }

      // Now set the body as the current scope and visit the function
      var firstBlock = new ShaderBlock();
      shaderFunction.mBlocks.Add(firstBlock);
      mContext.mCurrentBlock = firstBlock;
      mCommonPass.Visit(node);
      // SpirV has some special rules about returns. Make sure to address these by fixing up missing terminators or removing extra functions.
      mFrontEnd.FixupBlockTerminators(shaderFunction);

      // Clear the function out of the current context
      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }
  }
}
