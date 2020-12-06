using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSShaders
{
  class FrontEndPreconstructorPass : FrontEndPass
  {
    FrontEndCommonPass mCommonPass = new FrontEndCommonPass();

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      var shaderType = FindDeclaredType(node);
      if (shaderType.IsPrimitiveType())
        return;

      mContext.mCurrentType = shaderType;
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

    IShaderIR GetInitialValue(VariableDeclaratorSyntax variable, ShaderField shaderField)
    {
      // If this field has an initializer expression then walk and visit it.
      if (variable.Initializer != null)
        return mCommonPass.WalkAndGetValueTypeResult(variable.Initializer);
      // Otherwise, build the default value based upon the type.
      else
        return GenerateDefaultValueOp(shaderField);
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
      mCommonPass.mFrontEnd = mFrontEnd;
      mCommonPass.mContext = mContext;

      var declaredShaderType = FindType(node.Declaration.Type);
      foreach (var variable in node.Declaration.Variables)
      {
        var variableSymbol = GetDeclaredSymbol(variable);
        if(variableSymbol.IsStatic)
        {
          GlobalShaderField shaderField;
          int fieldIndex;
          mFrontEnd.FindStaticField(mContext.mCurrentType, variable.Identifier.ToString(), out shaderField, out fieldIndex);
          shaderField.InitialValue = GetInitialValue(variable, shaderField);
        }
        else
        {
          ShaderField shaderField;
          int fieldIndex;
          mFrontEnd.FindField(mContext.mCurrentType, variable.Identifier.ToString(), out shaderField, out fieldIndex);

          // If there is an initial value op then store it into the field
          IShaderIR initialValueOp = GetInitialValue(variable, shaderField);
          if (initialValueOp != null)
          {
            var variableOp = mFrontEnd.GenerateAccessChain(mContext.mThisOp, shaderField.mType, (uint)fieldIndex, mContext);
            mFrontEnd.CreateStoreOp(mContext.mCurrentBlock, variableOp, initialValueOp);
          }
        }
      }
    }

    ShaderOp GenerateDefaultValueOp(ShaderField shaderField)
    {
      var shaderFieldDerefType = shaderField.mType.GetDereferenceType();
      if (shaderFieldDerefType == FindType(typeof(bool)))
      {
        return mFrontEnd.CreateConstantOp(shaderFieldDerefType, false);
      }
      else if (shaderFieldDerefType == FindType(typeof(int)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(0);
        return mFrontEnd.CreateConstantOp(shaderFieldDerefType, contantLiteral);
      }
      else if (shaderFieldDerefType == FindType(typeof(uint)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(0u);
        return mFrontEnd.CreateConstantOp(shaderFieldDerefType, contantLiteral);
      }
      else if (shaderFieldDerefType == FindType(typeof(float)))
      {
        var contantLiteral = mFrontEnd.CreateConstantLiteral(shaderFieldDerefType, 0.0f.ToString());
        return mFrontEnd.CreateConstantOp(shaderFieldDerefType, contantLiteral);
      }
      else if(shaderFieldDerefType.mBaseType == OpType.Struct)
      {
        var fieldPtrType = shaderFieldDerefType.FindPointerType(StorageClass.Function);
        var localVariableOp = mFrontEnd.CreateOpVariable(fieldPtrType, mContext);
        localVariableOp.DebugInfo.Name = "temp" + shaderFieldDerefType.mMeta.mName;

        var resultType = FindType(typeof(void));
        var fnCall = CreateOp(mContext.mCurrentBlock, OpInstructionType.OpFunctionCall, resultType, new List<IShaderIR> { shaderFieldDerefType.mImplicitConstructor, localVariableOp });
        var loadOp = mFrontEnd.CreateOp(mContext.mCurrentBlock, OpInstructionType.OpLoad, shaderFieldDerefType, new List<IShaderIR> { localVariableOp });
        return loadOp;
      }
      return null;
    }
  }
}
