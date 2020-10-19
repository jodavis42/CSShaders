using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSShaders
{
  class FrontEndPreconstructorPass : FrontEndPass
  {
    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      mContext.mCurrentType = FindDeclaredType(node);
      GeneratePreConstructor(mContext.mCurrentType, node);
      GenerateDefaultConstructor(mContext.mCurrentType, node);

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
      base.VisitStructDeclaration(node);
      //foreach (var member in node.Members)
      //  base.Visit(member);
      //foreach(var fieldDeclaration in node.)
      //  base.visitmember(fieldDeclaration);
      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }

    public void GenerateDefaultConstructor(ShaderType owningType, StructDeclarationSyntax node)
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
      fnParamOps.Add(defaultConstructorFn);
      fnParamOps.Add(thisOp);
      CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, returnType, fnParamOps);

      mContext.mThisOp = null;
      mContext.mCurrentBlock = null;
      mContext.mCurrentFunction = null;
    }

    public void FindField(ShaderType owningType, string fieldName, out ShaderField shaderField, out int fieldIndex)
    {
      for (var i = 0; i < owningType.mFields.Count; ++i)
      {
        var field = owningType.mFields[i];
        if (field.mMeta.mName == fieldName)
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
      //mContext.mCurrentBlock = mContext.mCurrentType.mPreConstructor.mBlocks[0];

      var declaredShaderType = FindType(node.Declaration.Type);
      foreach (var variable in node.Declaration.Variables)
      {

        if (variable.Initializer != null)
        {
          ShaderField shaderField;
          int fieldIndex;
          FindField(mContext.mCurrentType, variable.Identifier.ToString(), out shaderField, out fieldIndex);
          var contantLiteral = mFrontEnd.CreateConstantLiteral(fieldIndex);
          var memberIndexConstant = mFrontEnd.CreateConstantOp(FindType(typeof(uint)), contantLiteral);
          var variableOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpAccessChain, shaderField.mType, new List<IShaderIR> { mContext.mThisOp, memberIndexConstant });
          var initialValue = WalkAndGetResult(variable.Initializer);
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValue);
        }
      }
      //mContext.mCurrentBlock = null;
    }
  }
}
