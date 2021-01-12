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
        else
        {
          var typeSymbol = GetSymbol(node.Declaration.Type) as INamedTypeSymbol;
          DefaultConstructType(typeSymbol, variableOp);
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
          var staticField = FindStaticField(fieldSymbol);
          mContext.Push(staticField.InstanceOp);
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
          var left = WalkAndGetResult(node.Expression) as ShaderOp;
          if(left.mResultType.mBaseType == OpType.Pointer)
          {
            var memberVariableOp = mFrontEnd.GenerateAccessChain(left, fieldSymbol.Name, mContext);
            mContext.Push(memberVariableOp);
          }
          else
          {
            var memberVariableOp = mFrontEnd.GenerateCompositeExtract(left, fieldSymbol.Name, mContext);
            mContext.Push(memberVariableOp);
          }
        }
        else
        {
          var staticField = FindStaticField(fieldSymbol);
          mContext.Push(staticField.InstanceOp);
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
      var storeOp = mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, leftOp, rightValueOp);
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

    public void VisitUnaryExpression(ExpressionSyntax node, SyntaxToken operatorToken, ExpressionSyntax operand, IShaderIR operandIR)
    {
      var symbol = GetSymbol(node);
      var methodSymbol = symbol as IMethodSymbol;
      if (methodSymbol == null)
        // Can this happen?
        throw new Exception("Invalid unary expression");

      var operandType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(methodSymbol.Parameters[0].Type));

      // Handle intrinsics (declared by the compiler but don't have symbols we can look up in advance)
      var unaryOpIntrinsic = mFrontEnd.mCurrentLibrary.FindUnaryOpIntrinsic(new UnaryOpKey(operatorToken.Text, operandType));
      if (unaryOpIntrinsic != null)
      {
        var result = unaryOpIntrinsic(operandIR, mContext);
        mContext.Push(result);
        return;
      }

      // Find if this is an intrinsic declared via attribute
      var intrinsicCallback = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(new FunctionKey(methodSymbol));
      if (intrinsicCallback != null)
      {
        var valueIR = WalkAndGetResult(operand);
        intrinsicCallback(mFrontEnd, new List<IShaderIR> { valueIR }, mContext);
        return;
      }

      // Find if this is a user defined function
      var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(symbol));
      if (shaderFunction != null)
      {
        var fnCallResult = mFrontEnd.GenerateFunctionCall(shaderFunction, null, new List<IShaderIR> { operandIR }, mContext);
        mContext.Push(fnCallResult);
        return;
      }

      throw new Exception("Unhandled");
    }

    public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
    {
      var operandIR = WalkAndGetResult(node.Operand);
      var operandValueIR = mFrontEnd.GetOrGenerateValueTypeFromIR(mContext.mCurrentBlock, operandIR);
      VisitUnaryExpression(node, node.OperatorToken, node.Operand, operandValueIR);

      // Handle pre/post fix modification operators
      if (node.OperatorToken.Text == "++" || node.OperatorToken.Text == "--")
      {
        // Get the result of the operation and store it back into the variable.
        var result = mContext.Pop();
        mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, operandIR, result);
        // Push the original value of the variable onto the stack so any later uses grab the old value.
        mContext.Push(operandValueIR);
      }
    }

    public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
    {
      var operandIR = WalkAndGetResult(node.Operand);
      VisitUnaryExpression(node, node.OperatorToken, node.Operand, operandIR);

      // Handle pre/post fix modification operators
      if (node.OperatorToken.Text == "++" || node.OperatorToken.Text == "--")
      {
        // Get the result of the operation and store it back into the variable.
        var result = mContext.Pop();
        mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, operandIR, result);
        // Push the newly assigned to variable onto the stack for any future operations.
        mContext.Push(operandIR);
      }
    }

    public override void VisitBinaryExpression(BinaryExpressionSyntax node)
    {
      var symbol = GetSymbol(node);

      var methodSymbol = symbol as IMethodSymbol;
      if (methodSymbol == null)
        // Can this happen?
        throw new Exception("Invalid binary expression");

      var lhsIR = WalkAndGetResult(node.Left);
      var rhsIR = WalkAndGetResult(node.Right);
      var lhsType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(methodSymbol.Parameters[0].Type));
      var rhsType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(methodSymbol.Parameters[1].Type));

      // Handle intrinsics (declared by the compiler but don't have symbols we can look up in advance)
      var binaryOpIntrinsic = mFrontEnd.mCurrentLibrary.FindBinaryOpIntrinsic(new BinaryOpKey(lhsType, node.OperatorToken.Text, rhsType));
      if (binaryOpIntrinsic != null)
      {
        var result = binaryOpIntrinsic(lhsIR, rhsIR, mContext);
        mContext.Push(result);
        return;
      }

      // Find if this is an intrinsic declared via attribute
      var intrinsicCallback = mFrontEnd.mCurrentLibrary.FindIntrinsicFunction(new FunctionKey(methodSymbol));
      if(intrinsicCallback != null)
      {
        intrinsicCallback(mFrontEnd, new List<IShaderIR> { lhsIR, rhsIR }, mContext);
        return;
      }

      // Find if this is a user defined function
      var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(symbol));
      if (shaderFunction != null)
      {
        var fnCallResult = mFrontEnd.GenerateFunctionCall(shaderFunction, null, new List<IShaderIR> { lhsIR, rhsIR }, mContext);
        mContext.Push(fnCallResult);
        return;
      }

      throw new Exception("Unhandled");
    }

    public override void VisitCastExpression(CastExpressionSyntax node)
    {
      var expressionOp = WalkAndGetValueTypeResult(node.Expression) as ShaderOp;
      var castedType = FindType(node.Type);
      // If we're casting to the same type, make this a no-op
      if (castedType == expressionOp.mResultType)
      {
        mContext.Push(expressionOp);
        return;
      }

      var symbol = GetSymbol(node);
      // Try to find a cast intrinsic for a primtive type
      var castIntrinsic = mFrontEnd.mCurrentLibrary.FindCastIntrinsics(castedType, expressionOp.mResultType);
      if (castIntrinsic != null)
      {
        var castOp = castIntrinsic(castedType, expressionOp, mContext);
        if (castOp == null)
          throw new Exception("invalid cast");
        return;
      }
      // If this is a method symbold, try to find the function call for a cast function
      else if (symbol != null && symbol is IMethodSymbol methodSymbol)
      {
        var shaderFunction = mFrontEnd.mCurrentLibrary.FindFunction(new FunctionKey(methodSymbol));
        if(shaderFunction == null)
          throw new Exception("invalid cast");
        var castOp = mFrontEnd.GenerateFunctionCall(shaderFunction, null, new List<IShaderIR> { expressionOp }, mContext);
        mContext.Push(castOp);
        return;
      }
      throw new Exception("invalid cast");
    }
  }
}
