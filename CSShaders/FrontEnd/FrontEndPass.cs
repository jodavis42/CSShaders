using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class FrontEndPass : CSharpSyntaxWalker
  {
    public FrontEndTranslator mFrontEnd;
    public FrontEndContext mContext;

    public virtual void Visit(FrontEndTranslator translator, SyntaxNode node, FrontEndContext context)
    {
      mFrontEnd = translator;
      mContext = context;
      base.Visit(node);
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
      if (!IsValidClassDeclaration(node))
        AddError(node, "Keyword 'class' not allowed. Use 'struct' instead");
    }

    public void AddError(SyntaxNode node, string errorMsg)
    {
      Console.WriteLine(String.Format("Error {0}: {1}", node.GetLocation().ToString(), errorMsg));
    }

    public ISymbol GetDeclaredSymbol(SyntaxNode node)
    {
      return mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
    }
    public ISymbol GetSymbol(SyntaxNode node)
    {
      return mFrontEnd.mSemanticModel.GetSymbolInfo(node).Symbol;
    }

    public IShaderIR GetIR(SyntaxNode node)
    {
      var symbol = GetSymbol(node);
      return mContext.mSymbolToIRMap.GetValueOrDefault(symbol, null);
    }

    public ShaderType FindDeclaredType(SyntaxNode node)
    {
      var symbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
      if (symbol == null)
        return null;
      return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(symbol));
    }

    public ShaderType FindDeclaredType(BaseTypeDeclarationSyntax node)
    {
      var symbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
      if (symbol == null)
        return null;
      return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(symbol));
    }

    public ShaderType FindType(Type type)
    {
      return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(type));
    }

    public ShaderType FindType(SyntaxNode node)
    {
      var typeInfo = mFrontEnd.mSemanticModel.GetTypeInfo(node);
      if (typeInfo.Type != null)
        return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(typeInfo.Type));
      return null;
    }

    public ShaderType FindType(TypeSyntax node)
    {
      var typeInfo = mFrontEnd.mSemanticModel.GetTypeInfo(node);
      if (typeInfo.Type != null)
        return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(typeInfo.Type));
      return null;
    }

    public ShaderType CreateType(ISymbol symbol, OpType opType)
    {
      return mFrontEnd.CreateType(symbol, opType);
    }

    public ShaderFunction FindFunction(BaseMethodDeclarationSyntax node)
    {
      var symbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
      if (symbol == null)
        return null;
      return mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(symbol));
    }

    public void AddIRForSymbol(ISymbol symbol, IShaderIR ir)
    {
      mContext.mSymbolToIRMap.Add(symbol, ir);
    }

    public IShaderIR FindIRFromSymbol(ISymbol symbol)
    {
      return mContext.mSymbolToIRMap.GetValueOrDefault(symbol);
    }
    public ShaderOp CreateVariable(ShaderType fieldType)
    {
      var op = new ShaderOp();
      op.mOpType = OpInstructionType.OpVariable;
      op.mResultType = fieldType;
      op.mParameters.Add(fieldType);
      return op;
    }

    public ShaderOp FindStaticField(ISymbol symbol)
    {
      // Try and find the type that owns this symbol
      var owningType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(symbol.ContainingType));
      if (owningType == null)
        return null;

      // Find the field on this symbol
      ShaderField shaderField;
      int fieldIndex;
      mFrontEnd.FindField(owningType, symbol.Name, out shaderField, out fieldIndex);
      // Look up the stored spirv field in the library
      var staticOp = mFrontEnd.mCurrentLibrary.mStaticGlobals.GetValueOrDefault(shaderField);
      return staticOp;
    }

    public ShaderOp CreateOp(OpInstructionType opType, ShaderType resultType, List<IShaderIR> arguments)
    {
      return mFrontEnd.CreateOp(opType, resultType, arguments);
    }

    public ShaderOp CreateOp(ShaderBlock block, OpInstructionType opType, ShaderType resultType, List<IShaderIR> arguments)
    {
      return mFrontEnd.CreateOp(block, opType, resultType, arguments);
    }

    public IShaderIR WalkAndGetResult(SyntaxNode node)
    {
      base.Visit(node);
      return mContext.Pop();
    }

    public IShaderIR WalkAndGetValueTypeResult(SyntaxNode node)
    {
      return WalkAndGetValueTypeResult(mContext.mCurrentBlock, node);
    }

    public IShaderIR WalkAndGetValueTypeResult(ShaderBlock block, SyntaxNode node)
    {
      var ir = WalkAndGetResult(node);
      return mFrontEnd.GetOrGenerateValueTypeFromIR(block, ir);
    }

    public void ExtractDebugInfo(IShaderIR ir, SyntaxNode node)
    {
      ir.DebugInfo.Location = node.GetLocation();
    }

    public void ExtractDebugInfo(IShaderIR ir, ISymbol symbol, SyntaxNode node)
    {
      ir.DebugInfo.Name = symbol.Name;
      ir.DebugInfo.Location = node.GetLocation();
    }

    public void ParseAttributes(ShaderIRMeta shaderMeta, ISymbol symbol)
    {
      shaderMeta.mAttributes = mFrontEnd.ParseAttributes(symbol);
    }

    public ShaderType FindParameterType(ShaderType paramType, IParameterSymbol paramSymbol)
    {
      if (paramSymbol == null || paramSymbol.RefKind == RefKind.None)
        return paramType;

      return paramType.StorageClassCollection.FindPointerType(StorageClass.Function);
    }

    public ShaderType FindParameterType(ParameterSyntax paramSyntaxNode)
    {
      var paramType = FindType(paramSyntaxNode.Type);
      var paramSymbol = GetDeclaredSymbol(paramSyntaxNode) as IParameterSymbol;
      if (paramSymbol == null || paramSymbol.RefKind == RefKind.None)
        return paramType;

      return paramType.StorageClassCollection.FindPointerType(StorageClass.Function);
    }

    public bool IsValidClassDeclaration(ClassDeclarationSyntax node)
    {
      var symbol = GetDeclaredSymbol(node) as ITypeSymbol;
      return symbol.BaseType.Name == typeof(Attribute).Name;
    }
  }
}
