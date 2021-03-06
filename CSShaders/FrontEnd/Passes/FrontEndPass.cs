﻿using Microsoft.CodeAnalysis;
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
      mContext.mCurrentPass = this;
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

    public ShaderType GetSymbolType(SyntaxNode node)
    {
      var typeInfo = mFrontEnd.mSemanticModel.GetTypeInfo(node);
      return mFrontEnd.mCurrentLibrary.FindType(new TypeKey(typeInfo.Type));
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
      var ir = WalkAndGetResult(node);
      // Walking the result could change the block. The load op (if required) must be emitted in the actual current block.
      return mFrontEnd.GetOrGenerateValueTypeFromIR(mContext.mCurrentBlock, ir);
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
      return TypeAliases.GetTypeName(symbol.BaseType) == TypeAliases.GetTypeName<Attribute>();
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

    /////////////////////////////////////////////////////////////// Loops
    public void GenerateGenericLoop(IEnumerable<SyntaxNode> initializerNodes, IEnumerable<SyntaxNode> iteratorNodes, SyntaxNode conditionalNode, StatementSyntax loopScopeNode, FrontEndContext context)
    {
      // Always walk the initializer node statements first if they exists. The contents of this go before any loop block.
      if (initializerNodes != null)
      {
        foreach(var initializerNode in initializerNodes)
          WalkAndGetResult(initializerNode);
      }

      // A basic while looks like a header block that always jumps to a condition block.
      // The condition block will choose to jump either to the loop block or to the merge point.
      // The loop block will always branch to the continue target unless a break happens which
      // will branch to the merge block (after the loop).
      // The continue block will always jump back to the header block.
      var headerBlock = mFrontEnd.CreateBlock("headerBlock");
      var conditionBlock = mFrontEnd.CreateBlock("conditionBlock");
      var loopTrueBlock = mFrontEnd.CreateBlock("loop-body");
      var continueBlock = mFrontEnd.CreateBlock("continueBlock");
      var mergeBlock = mFrontEnd.CreateBlock("after-loop");

      // Always jump to the header block
      mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { headerBlock });

      // The header always jumps to the conditional
      context.mCurrentFunction.mBlocks.Add(headerBlock);
      GenerateLoopHeaderBlock(headerBlock, conditionBlock, mergeBlock, continueBlock, context);

      // The conditional will jump to either the loop body or the merge point (after the loop)
      context.mCurrentFunction.mBlocks.Add(conditionBlock);
      GenerateLoopConditionBlock(conditionalNode, conditionBlock, loopTrueBlock, mergeBlock, context);

      // Walk all of the statements in the loop body and jump to either the merge or continue block
      context.mCurrentFunction.mBlocks.Add(loopTrueBlock);
      GenerateLoopStatements(loopScopeNode, loopTrueBlock, mergeBlock, continueBlock, context);

      // The continue block always just jumps to the header block
      context.mCurrentFunction.mBlocks.Add(continueBlock);
      GenerateLoopContinueBlock(iteratorNodes, continueBlock, headerBlock, context);

      // Afterwards the active block is always the merge point
      context.mCurrentFunction.mBlocks.Add(mergeBlock);
      context.mCurrentBlock = mergeBlock;
    }

    public void GenerateLoopHeaderBlock(ShaderBlock headerBlock, ShaderBlock branchTarget, ShaderBlock mergeBlock, ShaderBlock continueBlock, FrontEndContext context)
    {
      // Mark the header block as a loop block (so we emit the LoopMerge instruction)
      headerBlock.mBlockType = BlockType.Loop;
      // Being a LoopMerge requires setting the merge and continue points
      headerBlock.mMergePoint = mergeBlock;
      headerBlock.mContinuePoint = continueBlock;

      var loopControlMask = mFrontEnd.CreateConstantLiteral<uint>((uint)Spv.LoopControlMask.LoopControlMaskNone);
      mFrontEnd.CreateOp(headerBlock, OpInstructionType.OpLoopMerge, null, new List<IShaderIR> { mergeBlock, continueBlock, loopControlMask } );
      // The header always jumps to the branch target (typically a continue)
      mFrontEnd.CreateOp(headerBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { branchTarget });
    }

    public void GenerateLoopConditionBlock(SyntaxNode conditionalNode, ShaderBlock conditionBlock, ShaderBlock branchTrueBlock, ShaderBlock branchFalseBlock, FrontEndContext context)
    {
      // The condition builds the conditional and then jumps either to the body of the loop or to the end
      context.mCurrentBlock = conditionBlock;
      // If the conditional node exists
      if (conditionalNode != null)
      {
        //ExtractDebugInfo(conditionalNode->Condition, context->mDebugInfo);
        // Get the conditional value (must be a bool via how zilch works)
        IShaderIR conditional = WalkAndGetValueTypeResult(context.mCurrentBlock, conditionalNode);
        // Branch to either the true or false branch
        mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpBranchConditional, null, new List<IShaderIR> { conditional, branchTrueBlock, branchFalseBlock });
      }
      // Otherwise there is no conditional (e.g. loop) so unconditionally branch to the true block
      else
        mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { branchTrueBlock });
    }

    public void GenerateLoopStatements(StatementSyntax loopScopeNode, ShaderBlock loopBlock, ShaderBlock mergeBlock, ShaderBlock continueBlock, FrontEndContext context)
    {
      context.mCurrentBlock = loopBlock;
      // Set the continue and merge points for this block (mainly needed for nested loops)
      loopBlock.mContinuePoint = continueBlock;
      loopBlock.mMergePoint = mergeBlock;
      context.PushMergePoint(continueBlock, mergeBlock);

      // Iterate over all of the statements in the loop body
      Visit(loopScopeNode);

      // Write out a jump back to the continue block of the loop. Only write this to the active block
      // which will either be the end of the loop block or something like an after if
      if (context.mCurrentBlock.mTerminatorOp == null)
      {
        var currentBlockContinue = mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { continueBlock });
      }
      context.PopMergePoint();
    }

    public void GenerateLoopContinueBlock(IEnumerable<SyntaxNode> iteratorNodes, ShaderBlock continueBlock, ShaderBlock headerBlock, FrontEndContext context)
    {
      // Mark the continue block as the active block
      context.mCurrentBlock = continueBlock;
      // If it exists, walk the iterator statement
      if (iteratorNodes != null)
      {
        foreach(var iteratorNode in iteratorNodes)
          Visit(iteratorNode);
      }
      // Always jump back to the header block
      mFrontEnd.CreateOp(continueBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { headerBlock });
    }
  }
}
