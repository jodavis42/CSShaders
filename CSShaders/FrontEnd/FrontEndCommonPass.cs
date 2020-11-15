using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  class FrontEndCommonPass : FrontEndPass
  {
    public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
      var localVariableType = FindType(node.Declaration.Type);
      foreach (var variable in node.Declaration.Variables)
      {
        var symbol = GetDeclaredSymbol(variable);
        var variableOp = mFrontEnd.CreateOpVariable(localVariableType, mContext);
        variableOp.DebugInfo.Name = variable.Identifier.ToString();
        AddIRForSymbol(symbol, variableOp);

        if (variable.Initializer != null)
        {
          var initialValue = WalkAndGetResult(variable.Initializer);
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValue);
        }
      }
    }
    public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
      var symbol = GetSymbol(node);
      var fnKey = new FunctionKey(symbol);
      var resultSymbol = GetSymbol(node.Type);
      var resultType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(resultSymbol));

      // Handle constructing intrinsics types
      if (TryCallIntrinsicsFunction(fnKey, null, node.ArgumentList.Arguments))
        return;

      if (resultType.mBaseType != OpType.Struct)
        throw new Exception("ToDo");

      var fieldPtrType = resultType.FindPointerType(StorageClass.Function);
      var localVariableOp = mFrontEnd.CreateOpVariable(fieldPtrType, mContext);
      localVariableOp.DebugInfo.Name = "temp" + resultType.mMeta.mName;
      
      var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(fnKey);
      if (shaderFunction == null && node.ArgumentList.Arguments.Count != 0)
        throw new Exception("Failed to find function");

      // If this is a function call then invoke it
      if (shaderFunction != null)
      {
        var arguments = new List<CSharpSyntaxNode>();
        foreach (var arg in node.ArgumentList.Arguments)
          arguments.Add(arg);
        GenerateFunctionCall(shaderFunction, localVariableOp, arguments);
      }

      // If there's an initializer expression then visit each one
      if (node.Initializer != null)
      {
        // The context of 'this' changes for the initializer expression
        var thisOp = mContext.mThisOp;
        mContext.mThisOp = localVariableOp;
        foreach (var initExpression in node.Initializer.Expressions)
        {
          Visit(initExpression);
        }
        mContext.mThisOp = thisOp;
      }
      
      mContext.Push(localVariableOp);
    }

    public override void VisitIdentifierName(IdentifierNameSyntax node)
    {
      var symbol = GetSymbol(node);
      if (symbol is IFieldSymbol fieldSymbol)
      {
        var instrinsicDelegate = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(new FunctionKey(symbol));
        if (instrinsicDelegate != null)
        {
          instrinsicDelegate(mFrontEnd, new List<IShaderIR> { mContext.mThisOp }, mContext);
          return;
        }

        if (!fieldSymbol.IsStatic)
        {
          var memberAccessOp = mFrontEnd.GenerateAccessChain(mContext.mThisOp, fieldSymbol.Name, mContext);
          mContext.Push(memberAccessOp);
        }
        else
        {
          var staticOp = FindStaticField(fieldSymbol);
          mContext.Push(staticOp);
        }
      }
      else if (symbol is IPropertySymbol propertySymbol)
      {
        var instrinsicDelegate = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(new FunctionKey(propertySymbol.GetMethod));
        if (instrinsicDelegate != null)
        {
          instrinsicDelegate(mFrontEnd, new List<IShaderIR> { mContext.mThisOp }, mContext);
          return;
        }
        throw new Exception("ToDo");
      }
      else if(symbol is ILocalSymbol localSymbol)
      {
        mContext.Push(FindIRFromSymbol(localSymbol));
      }
      else if (symbol is IParameterSymbol paramSymbol)
      {
        mContext.Push(FindIRFromSymbol(paramSymbol));
      }
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {
      var symbol = GetSymbol(node);

      var selfExpression = symbol.IsStatic ? null : node.Expression;
      if (TryCallIntrinsicsFunction(new FunctionKey(symbol), selfExpression, node.ArgumentList.Arguments))
        return;

      var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(symbol));
      var fnShaderReturnType = shaderFunction.GetReturnType();

      var argumentOps = new List<IShaderIR>();
      argumentOps.Add(shaderFunction);
      if(!shaderFunction.IsStatic)
      {
        var selfOp = WalkAndGetResult(node.Expression);
        argumentOps.Add(selfOp);
      }

      for(var i = 0; i < node.ArgumentList.Arguments.Count; ++i)
      {
        var argumentOp = GetFunctionParameter(shaderFunction, i, node.ArgumentList.Arguments[i]);
        argumentOps.Add(argumentOp);
      }
      
      var functionCallOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, fnShaderReturnType, argumentOps);
      mContext.Push(functionCallOp);
    }

    public override void VisitThisExpression(ThisExpressionSyntax node)
    {
      mContext.Push(mContext.mThisOp);
    }

    public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
      var symbol = GetSymbol(node);

      if (symbol is IMethodSymbol methodSymbol)
      {
        base.VisitMemberAccessExpression(node);
        return;
      }
      else if (symbol is IFieldSymbol fieldSymbol)
      {
        if (TryCallIntrinsicsFunction(new FunctionKey(fieldSymbol), node.Expression, null))
          return;

        if (!fieldSymbol.IsStatic)
        {
          var left = WalkAndGetResult(node.Expression);
          var memberVariableOp = mFrontEnd.GenerateAccessChain(left, fieldSymbol.Name, mContext);
          mContext.Push(memberVariableOp);
        }
        else
        {
          var staticOp = FindStaticField(fieldSymbol);
          mContext.Push(staticOp);
        }
        return;
      }
      else if (symbol is IPropertySymbol propertySymbol)
      {
        if (TryCallIntrinsicsFunction(new FunctionKey(propertySymbol.GetMethod), node.Expression, null))
          return;
        // @JoshD: Handle property getter/setter function calls
      }
    }

    public override void VisitLiteralExpression(LiteralExpressionSyntax node)
    {
      var resultType = FindType(node);
      var constantLiteral = mFrontEnd.CreateConstantLiteral(resultType, node.Token.ValueText);
      var constantOp = mFrontEnd.CreateConstantOp(resultType, constantLiteral);
      mContext.Push(constantOp);
    }

    public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
      // If the left side is actually a setter, then we have to flip the assignment around to call the setter
      var leftSymbol = GetSymbol(node.Left);
      // One complicated case is if the left is actually a member access (e.g. Vec3.XY = rhs).
      // In this case, we need to call the setter on Vec3 (the expression of the member access)
      if (node.Left is MemberAccessExpressionSyntax memberAccessNode)
      {
        FunctionKey functionKey = null;
        // Try and get the function key for this symbol depending on if it's a field, property, etc...
        if (leftSymbol is IPropertySymbol propertySymbol && !propertySymbol.IsReadOnly)
          functionKey = new FunctionKey(propertySymbol.SetMethod);
        else if (leftSymbol is IFieldSymbol fieldSymbol)
          functionKey = new FunctionKey(fieldSymbol);

        // If we found a function key, then try and use it call the setter 
        if(functionKey != null)
        {
          if (TryCallIntrinsicsSetterFunction(functionKey, memberAccessNode.Expression, node.Right))
            return;
          if (TryCallSetterFunction(functionKey, memberAccessNode.Expression, node.Right))
            return;
        }
      }

      var leftOp = WalkAndGetResult(node.Left);
      var rightValueOp = WalkAndGetValueTypeResult(node.Right);
      var storeOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpStore, null, new List<IShaderIR> { leftOp, rightValueOp });
      mContext.Push(storeOp);
    }

    public override void VisitReturnStatement(ReturnStatementSyntax node)
    {
      IShaderIR returnExpression = null;
      // Evaluate the return if necessary
      if (node.Expression != null)
        returnExpression = WalkAndGetResult(node.Expression);

      ShaderOp returnOp = null;
      if (returnExpression == null)
        returnOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpReturn, null, null);
      else
      {
        var returnType = mContext.mCurrentFunction.GetReturnType();
        if (returnType.mBaseType == OpType.Pointer)
        {
          var op = returnExpression as ShaderOp;
          if (op.mResultType != returnType)
            throw new Exception();
        }
        else
        {
          returnExpression = mFrontEnd.GetOrGenerateValueTypeFromIR(mContext.mCurrentBlock, returnExpression);
        }
        returnOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpReturnValue, null, new List<IShaderIR> { returnExpression });
      }
      mContext.mCurrentBlock.mTerminatorOp = returnOp;
    }

    public override void VisitUnaryPattern(UnaryPatternSyntax node)
    {
      base.VisitUnaryPattern(node);
    }

    public override void VisitBinaryExpression(BinaryExpressionSyntax node)
    {
      var symbol = GetSymbol(node);
      // If the symbol for this binary op is actually a method, then try to either call the intrinsic or function if they exist
      if(symbol is IMethodSymbol methodSymbol)
      {
        var intrinsicCallback = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(new FunctionKey(methodSymbol));
        if(intrinsicCallback != null)
        {
          var l = WalkAndGetValueTypeResult(node.Left) as ShaderOp;
          var r = WalkAndGetValueTypeResult(node.Right) as ShaderOp;
          intrinsicCallback(mFrontEnd, new List<IShaderIR> { l, r }, mContext);
          return;
        }

        var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(symbol));
        if (shaderFunction != null)
        {
          // @JoshD: Call function later?
        }
        return;
      }

      var leftOp = WalkAndGetValueTypeResult(node.Left) as ShaderOp;
      var rightOp = WalkAndGetValueTypeResult(node.Right) as ShaderOp;

      var op = new ShaderOp();
      if (node.OperatorToken.Text == "+")
      {
        switch (leftOp.mResultType.mBaseType)
        {
          case OpType.Int:
            op.mOpType = OpInstructionType.OpIAdd;
            break;
          case OpType.Float:
            op.mOpType = OpInstructionType.OpFAdd;
            break;
        }
      }
      op.mParameters.Add(leftOp);
      op.mParameters.Add(rightOp);
      op.mResultType = leftOp.mResultType;
      mContext.Push(op);
    }
  }
}
