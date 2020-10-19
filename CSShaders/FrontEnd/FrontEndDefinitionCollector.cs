using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSShaders
{
  public class FrontEndDefinitionCollector : FrontEndPass
  {
    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      mContext.mCurrentType = FindDeclaredType(node);
      GeneratePreConstructor(mContext.mCurrentType, node);
      GenerateDefaultConstructor(mContext.mCurrentType);
      base.VisitStructDeclaration(node);

      if (mContext.mCurrentType.mPreConstructor != null)
        mFrontEnd.FixupBlockTerminators(mContext.mCurrentType.mPreConstructor);
      if (mContext.mCurrentType.mImplicitConstructor != null)
        mFrontEnd.FixupBlockTerminators(mContext.mCurrentType.mImplicitConstructor);


      mContext.mCurrentType = null;
    }

    public void GeneratePreConstructor(ShaderType owningType, StructDeclarationSyntax node)
    {
      if (owningType.mPreConstructor == null)
        return;

      var preConstuctorFn = owningType.mPreConstructor;
      var thisType = owningType.mTypeStorage.mPointerType;

      var thisOp = CreateOp(preConstuctorFn.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
      thisOp.DebugInfo.Name = "self";
      preConstuctorFn.mBlocks.Add(new ShaderBlock());

      mContext.mCurrentFunction = preConstuctorFn;
      mContext.mCurrentBlock = preConstuctorFn.mBlocks[0];
      mContext.mThisOp = thisOp;

      
      foreach (var member in node.Members)
      {
        if (member is FieldDeclarationSyntax field)
          base.Visit(field);
      }
      //foreach(var fieldDeclaration in node.)
      //  base.visitmember(fieldDeclaration);
      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }

    public void GenerateDefaultConstructor(ShaderType owningType)
    {
      if (owningType.mImplicitConstructor == null)
        return;

      var returnType = FindType(typeof(void));
      var defaultConstructorFn = owningType.mImplicitConstructor;
      var thisType = owningType.mTypeStorage.mPointerType;

      var thisOp = CreateOp(defaultConstructorFn.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
      thisOp.DebugInfo.Name = "self";

      defaultConstructorFn.mBlocks.Add(new ShaderBlock());
      mContext.mCurrentFunction = defaultConstructorFn;
      mContext.mCurrentBlock = defaultConstructorFn.mBlocks[0];
      mContext.mThisOp = thisOp;

      var fnParamOps = new List<IShaderIR>();
      fnParamOps.Add(owningType.mPreConstructor);
      fnParamOps.Add(thisOp);
      CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, returnType, fnParamOps);

      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      var fnSymbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node)as IMethodSymbol;
      
      // Add the current function as the active one
      var shaderFunction = FindFunction(node);
      mContext.mCurrentFunction = shaderFunction;
      mContext.mCurrentBlock = shaderFunction.mParametersBlock;

      // Add the 'this' op if it exists
      if (!fnSymbol.IsStatic)
      {
        var selfType = mContext.mCurrentType;
        var thisType = selfType.mTypeStorage.mPointerType;
        var selfOp = CreateOp(mContext.mCurrentFunction.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
        mContext.mThisOp = selfOp;
      }
      // Add the function parameter ops
      foreach (var parameter in node.ParameterList.Parameters)
      {
        var parameterSymbol = GetDeclaredSymbol(parameter);
        var parameterType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(parameterSymbol.ContainingType));
        var paramOp = CreateOp(mContext.mCurrentFunction.mParametersBlock, OpInstructionType.OpFunctionParameter, parameterType, null);
        mContext.mSymbolToIRMap.Add(parameterSymbol, paramOp);
      }

      // Now set the body as the current scope and visit the function
      var firstBlock = new ShaderBlock();
      shaderFunction.mBlocks.Add(firstBlock);
      mContext.mCurrentBlock = firstBlock;
      base.VisitMethodDeclaration(node);
      mFrontEnd.FixupBlockTerminators(shaderFunction);
      

      // Clear the function out of the current context
      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }

    public void FindField(ShaderType owningType, string fieldName, out ShaderField shaderField, out int fieldIndex)
    {
      for(var i = 0; i < owningType.mFields.Count; ++i)
      {
        var field = owningType.mFields[i];
        if(field.mMeta.mName == fieldName)
        {
          shaderField = field;
          fieldIndex = i;
          return;
        }
      }
      shaderField = null;
      fieldIndex = owningType.mFields.Count;
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
      // Not visiting preconstructor
      if (mContext.mCurrentBlock == null)
        return;
      //mContext.mCurrentBlock = mContext.mCurrentType.mPreConstructor.mBlocks[0];

      var declaredShaderType = FindType(node.Declaration.Type);
      foreach (var variable in node.Declaration.Variables)
      {
        ShaderField shaderField;
        int fieldIndex;
        FindField(mContext.mCurrentType, variable.Identifier.ToString(), out shaderField, out fieldIndex);

        if (shaderField.mType.mBaseType == OpType.Struct)
        {
          var fieldPtrType = shaderField.mType.mTypeStorage.mPointerType;
          var localVariableOp = mFrontEnd.CreateOpVariable(fieldPtrType, mContext);
          localVariableOp.DebugInfo.Name = "temp" + shaderField.mType.mMeta.mName;

          var resultType = FindType(typeof(void));
          var fnCall = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, resultType, new List<IShaderIR> { shaderField.mType.mImplicitConstructor, localVariableOp });

          var contantLiteral = mFrontEnd.CreateConstantLiteral(fieldIndex);
          var memberIndexConstant = mFrontEnd.CreateConstantOp(FindType(typeof(uint)), contantLiteral);
          var memberVariableOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpAccessChain, shaderField.mType.mTypeStorage.mPointerType, new List<IShaderIR> { mContext.mThisOp, memberIndexConstant });

          var loadOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpLoad, shaderField.mType, new List<IShaderIR> { localVariableOp });
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, memberVariableOp, loadOp);
          
          continue;
        }

        IShaderIR initialValueOp = null;
        if (variable.Initializer != null)
        {
          initialValueOp = WalkAndGetResult(variable.Initializer);
        }
        else
        {
          if (shaderField.mType == FindType(typeof(bool)))
          {
            initialValueOp = mFrontEnd.CreateConstantOp(shaderField.mType, false);
          }
          else if(shaderField.mType == FindType(typeof(int)))
          {
            var contantLiteral = mFrontEnd.CreateConstantLiteral(0);
            initialValueOp = mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
          }
          else if (shaderField.mType == FindType(typeof(uint)))
          {
            var contantLiteral = mFrontEnd.CreateConstantLiteral(0u);
            initialValueOp = mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
          }
          else if (shaderField.mType == FindType(typeof(float)))
          {
            var contantLiteral = mFrontEnd.CreateConstantLiteral(shaderField.mType, 0.0f.ToString());
            initialValueOp = mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
          }
        }

        if(initialValueOp != null)
        {
          var contantLiteral = mFrontEnd.CreateConstantLiteral(fieldIndex);
          var memberIndexConstant = mFrontEnd.CreateConstantOp(FindType(typeof(uint)), contantLiteral);
          var variableOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpAccessChain, shaderField.mType.mTypeStorage.mPointerType, new List<IShaderIR> { mContext.mThisOp, memberIndexConstant });
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValueOp);
        }
      }
      //mContext.mCurrentBlock = null;
    }
    public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
      var localVariableType = FindType(node.Declaration.Type);
      foreach(var variable in node.Declaration.Variables)
      {
        var symbol = GetDeclaredSymbol(variable);
        var variableOp = mFrontEnd.CreateOpVariable(localVariableType, mContext);
        variableOp.DebugInfo.Name = variable.Identifier.ToString();
        AddIRForSymbol(symbol, variableOp);

        if(variable.Initializer != null)
        {
          var initialValue = WalkAndGetResult(variable.Initializer);
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValue);
        }
      }
    }

    public override void VisitIdentifierName(IdentifierNameSyntax node)
    {
      var symbol = GetSymbol(node);
      var fieldSymbol = symbol as IFieldSymbol;
      if (fieldSymbol != null)
      {
        mContext.Push(FindIRFromSymbol(fieldSymbol));
        return;
      }

      var localSymbol = symbol as ILocalSymbol;
      if(localSymbol != null)
      {
        mContext.Push(FindIRFromSymbol(localSymbol));
        return;
      }

      var paramSymbol = symbol as IParameterSymbol;
      if (paramSymbol != null)
      {
        mContext.Push(FindIRFromSymbol(paramSymbol));
        return;
      }
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {

      var expressionInfo = mFrontEnd.mSemanticModel.GetTypeInfo(node.Expression);
      var memberSymbol = mFrontEnd.mSemanticModel.GetTypeInfo(node).Type as INamedTypeSymbol;
      var members = memberSymbol.GetMembers();
      var expression = node.Expression;
      var args = node.ArgumentList;
      base.VisitInvocationExpression(node);
    }

    public override void VisitThisExpression(ThisExpressionSyntax node)
    {
      mContext.Push(mContext.mThisOp);
    }

    public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
      var left = WalkAndGetResult(node.Expression);
      var memberSymbol = GetSymbol(node.Name);
      var methodSymbol = memberSymbol as IMethodSymbol;
      if(methodSymbol != null)
      {
        var resultType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(methodSymbol.ReturnType));
        var fnCall = CreateOp(OpInstructionType.OpFunctionCall, resultType, null);
        
      }
      base.VisitMemberAccessExpression(node);
    }

    public ShaderOp GenerateFunctionCall(IMethodSymbol methodSymbol, IShaderIR selfOp)
    {
      return new ShaderOp();
    }

    public override void VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
    {
      var symbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
      var symbolInfo = mFrontEnd.mSemanticModel.GetSymbolInfo(node);
      base.VisitSingleVariableDesignation(node);
    }

    public override void VisitUnaryPattern(UnaryPatternSyntax node)
    {
      base.VisitUnaryPattern(node);
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
      var leftOp = WalkAndGetResult(node.Left);
      var rightOp = WalkAndGetResult(node.Right);
      var storeOp = mFrontEnd.CreateOp(OpInstructionType.OpStore, null, new List<IShaderIR>{ leftOp, rightOp });
      mContext.Push(storeOp);
    }

    public override void VisitBinaryExpression(BinaryExpressionSyntax node)
    {
      var leftOp = WalkAndGetResult(node.Left);
      var rightOp = WalkAndGetResult(node.Right);

      var op = new ShaderOp();
      if (node.OperatorToken.Text == "+")
      {
        op.mOpType = OpInstructionType.OpIAdd;
      }
      op.mParameters.Add(leftOp);
      op.mParameters.Add(rightOp);
      mContext.Push(op);
    }

    public override void VisitReturnStatement(ReturnStatementSyntax node)
    {
      IShaderIR returnExpression = null;
      if (node.Expression != null)
        returnExpression = WalkAndGetResult(node.Expression);

      ShaderOp returnOp = null;
      if(returnExpression == null)
        returnOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpReturn, null, null);
      else
        returnOp = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpReturnValue, null, new List<IShaderIR> { returnExpression });
      mContext.mCurrentBlock.mTerminatorOp = returnOp;
    }


  }
}
