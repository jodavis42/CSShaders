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
    public SpecialResolvers SpecialResolvers = new SpecialResolvers();

    public virtual void Visit(FrontEndTranslator translator, CSharpCompilation compilation, List<SyntaxTree> trees, FrontEndContext context)
    {
      mFrontEnd = translator;
      mContext = context;
      VisitTrees(compilation, trees);
    }

    public virtual void VisitTrees(CSharpCompilation compilation, List<SyntaxTree> trees)
    {
      foreach (var tree in trees)
      {
        mFrontEnd.mSemanticModel = compilation.GetSemanticModel(tree);

        base.Visit(tree.GetRoot());
      }
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

    public GlobalShaderField FindStaticField(ISymbol symbol)
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
    public void ExtractDebugInfo(ShaderField field, ISymbol symbol, SyntaxNode node)
    {
      field.DebugInfo.Name = symbol.Name;
      field.DebugInfo.Location = node.GetLocation();
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

    public IMethodSymbol FindDefaultConstructorSymbol(INamedTypeSymbol typeSymbol)
    {
      foreach (var constructor in typeSymbol.Constructors)
      {
        if (constructor.Parameters.Length == 0)
          return constructor;
      }
      return null;
    }

    public void DefaultConstructType(INamedTypeSymbol typeSymbol, ShaderOp variableOp)
    {
      var defaultConstructorSymbol = FindDefaultConstructorSymbol(typeSymbol);
      if (defaultConstructorSymbol == null)
        return;

      var constructorKey = new FunctionKey(defaultConstructorSymbol);
      // If this is an intrinsic (defined by the compiler and not user defined) then invoke it.
      var constructorIntrinsic = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(constructorKey);
      if (constructorIntrinsic != null)
      {
        constructorIntrinsic(mFrontEnd, new List<IShaderIR>(), mContext);
        mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, mContext.Pop());
        return;
      }
      // Otherwise see if this is user defined
      var constructorFn = mFrontEnd.mCurrentLibrary.FindFunction(constructorKey);
      if(constructorFn != null)
      {
        mFrontEnd.GenerateFunctionCall(constructorFn, variableOp, null, mContext);
        return;
      }

      var derefType = variableOp.mResultType.GetDereferenceType();
      var defaultValue = mFrontEnd.DefaultConstructPrimitive(derefType, mContext);
      mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, defaultValue);
    }

    public ShaderFunction CreateFunction(BaseMethodDeclarationSyntax node, string fnName, ShaderType returnType)
    {
      var owningType = mContext.mCurrentType;
      var fnSymbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);

      // Collect function return and parameter types from the syntax node
      var thisType = fnSymbol.IsStatic ? null : owningType.FindPointerType(StorageClass.Function);
      var paramTypes = new List<ShaderType>();
      foreach (var parameter in node.ParameterList.Parameters)
      {
        var parameterType = FindParameterType(parameter);
        paramTypes.Add(parameterType);
      }

      var shaderFunction = mFrontEnd.CreateFunctionAndType(owningType, returnType, fnName, thisType, paramTypes);
      ParseAttributes(shaderFunction.mMeta, fnSymbol);
      ExtractDebugInfo(shaderFunction, fnSymbol, node);
      mFrontEnd.mCurrentLibrary.AddFunction(new FunctionKey(fnSymbol), shaderFunction);
      return shaderFunction;
    }

    public IShaderIR GetFunctionParameter(ShaderFunction shaderFunction, int paramIndex, IShaderIR argumentOp)
    {
      return mFrontEnd.GetFunctionParameter(shaderFunction, paramIndex, argumentOp, mContext);
    }

    public IShaderIR GetFunctionParameter(ShaderFunction shaderFunction, int paramIndex, CSharpSyntaxNode argument)
    {
      // Evaluate the expression
      var argumentOp = WalkAndGetResult(argument);
      return GetFunctionParameter(shaderFunction, paramIndex, argumentOp);
    }

    public void GenerateFunctionCall(ShaderFunction shaderFunction, IShaderIR selfOp, List<IShaderIR> arguments)
    {
      mFrontEnd.GenerateFunctionCall(shaderFunction, selfOp, arguments, mContext);
    }

    public void GenerateFunctionCall(ShaderFunction shaderFunction, IShaderIR selfOp, List<CSharpSyntaxNode> arguments)
    {
      var argumentOps = new List<IShaderIR>();
      argumentOps.Add(shaderFunction);
      if (!shaderFunction.IsStatic)
      {
        argumentOps.Add(selfOp);
      }

      for (var i = 0; i < arguments.Count; ++i)
      {
        var argument = arguments[i];
        var paramIR = GetFunctionParameter(shaderFunction, i, argument);
        argumentOps.Add(paramIR);
      }

      var fnShaderReturnType = shaderFunction.GetReturnType();
      var functionCallOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, fnShaderReturnType, argumentOps);
      mContext.Push(functionCallOp);
    }

    public bool TryCallFunction(FunctionKey functionKey, CSharpSyntaxNode selfExpressionNode, CSharpSyntaxNode rhsNode)
    {
      var setterShaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(functionKey);
      if (setterShaderFunction == null)
        return false;

      var selfInstanceIR = WalkAndGetResult(selfExpressionNode);
      var rhsIR = WalkAndGetResult(rhsNode);
      GenerateFunctionCall(setterShaderFunction, selfInstanceIR, new List<IShaderIR>() { rhsIR });
      return true;
    }

    public bool TryCallIntrinsicsFunction(FunctionKey functionKey, CSharpSyntaxNode selfExpressionNode, IEnumerable<CSharpSyntaxNode> arguments)
    {
      var instrinsicDelegate = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(functionKey);
      if (instrinsicDelegate == null)
        return false;

      // Build the arguments up
      var argumentIRs = new List<IShaderIR>();
      // Visit the self if it exists
      if (selfExpressionNode != null)
      {
        var selfOp = WalkAndGetResult(selfExpressionNode);
        argumentIRs.Add(selfOp);
      }
      // Visit all the args if they exist
      if (arguments != null)
      {
        foreach (var argument in arguments)
          argumentIRs.Add(WalkAndGetResult(argument));
      }
      instrinsicDelegate(mFrontEnd, argumentIRs, mContext);
      return true;
    }

    public bool TryCallSetterFunction(FunctionKey functionKey, IShaderIR selfInstanceIR, IShaderIR rhsIR)
    {
      var setterShaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(functionKey);
      if (setterShaderFunction == null)
        return false;

      GenerateFunctionCall(setterShaderFunction, selfInstanceIR, new List<IShaderIR>() { rhsIR });
      return true;
    }

    public bool TryCallIntrinsicsSetterFunction(FunctionKey functionKey, IShaderIR setter, IShaderIR argumentIR)
    {
      var instrinsicDelegate = mFrontEnd.mCurrentLibrary.FindIntrinsicSetterFunction(functionKey);
      if (instrinsicDelegate == null)
        return false;

      // Build the arguments up
      var argumentIRs = new List<IShaderIR>();
      instrinsicDelegate(mFrontEnd, setter, argumentIR, mContext);

      return true;
    }

    public bool IsIntrinsic(ShaderAttributes attributes)
    {
      return attributes.Contains(typeof(Shader.IntrinsicFunction)) || attributes.Contains(typeof(Shader.SimpleIntrinsicFunction)) || attributes.Contains(typeof(Shader.SimpleExtensionIntrinsic))
        || attributes.Contains(typeof(Math.CompositeConstruct)) || attributes.Contains(typeof(Math.Swizzle))
        || attributes.Contains(typeof(Shader.ImageIntrinsicFunction)) || attributes.Contains(typeof(Shader.SampledImageIntrinsicFunction));
    }
  }
}
