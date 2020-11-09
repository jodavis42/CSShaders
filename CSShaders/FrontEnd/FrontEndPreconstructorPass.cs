using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSShaders
{
  class FrontEndPreconstructorPass : FrontEndPass
  {
    FrontEndCommonPass mCommonPass = new FrontEndCommonPass();

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
      var thisType = owningType.FindPointerType(StorageClass.Function);

      var thisOp = CreateOp(preConstuctorFn.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
      thisOp.DebugInfo.Name = "self";
      preConstuctorFn.mBlocks.Add(new ShaderBlock());

      mContext.mCurrentFunction = preConstuctorFn;
      mContext.mCurrentBlock = preConstuctorFn.mBlocks[0];
      mContext.mThisOp = thisOp;
      base.VisitStructDeclaration(node);
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
      var thisType = owningType.FindPointerType(StorageClass.Function);

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

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
      mCommonPass.mFrontEnd = mFrontEnd;
      mCommonPass.mContext = mContext;

      var declaredShaderType = FindType(node.Declaration.Type);
      foreach (var variable in node.Declaration.Variables)
      {
        ShaderField shaderField;
        int fieldIndex;
        mFrontEnd.FindField(mContext.mCurrentType, variable.Identifier.ToString(), out shaderField, out fieldIndex);

        IShaderIR initialValueOp = null;
        // If this field has an initializer expression then walk and visit it.
        if (variable.Initializer != null)
        {
          initialValueOp = mCommonPass.WalkAndGetValueTypeResult(variable.Initializer);
        }
        // Otherwise, build the default value based upon the type.
        else
        {
          initialValueOp = GenerateDefaultValueOp(shaderField);
        }

        // If there is an initial value op then store it into the field
        if (initialValueOp != null)
        {
          var variableOp = mFrontEnd.GenerateAccessChain(mContext.mThisOp, shaderField.mType, (uint)fieldIndex, mContext);
          mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValueOp);
        }
      }
    }

    ShaderOp GenerateDefaultValueOp(ShaderField shaderField)
    {

      if (shaderField.mType == FindType(typeof(bool)))
      {
        return mFrontEnd.CreateConstantOp(shaderField.mType, false);
      }
      else if (shaderField.mType == FindType(typeof(int)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(0);
        return mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
      }
      else if (shaderField.mType == FindType(typeof(uint)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(0u);
        return mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
      }
      else if (shaderField.mType == FindType(typeof(float)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(shaderField.mType, 0.0f.ToString());
        return mFrontEnd.CreateConstantOp(shaderField.mType, contantLiteral);
      }
      else if(shaderField.mType.mBaseType == OpType.Struct)
      {
        var fieldPtrType = shaderField.mType.FindPointerType(StorageClass.Function);
        var localVariableOp = mFrontEnd.CreateOpVariable(fieldPtrType, mContext);
        localVariableOp.DebugInfo.Name = "temp" + shaderField.mType.mMeta.mName;

        var resultType = FindType(typeof(void));
        var fnCall = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, resultType, new List<IShaderIR> { shaderField.mType.mImplicitConstructor, localVariableOp });
        var loadOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpLoad, shaderField.mType, new List<IShaderIR> { localVariableOp });
        return loadOp;
      }
      return null;
    }
  }
}
