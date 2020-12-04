using System.Collections.Generic;

namespace CSShaders
{
  class SpirVDummyEntryPointGenerator
  {
    public void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderLibrary library, FrontEndTranslator frontEnd)
    {
      var unitTestTypes = new List<ShaderType>();
      foreach (var type in typeCollector.mReferencedTypes)
      {
        if (type.mMeta.mAttributes.Contains("UnitTest"))
          unitTestTypes.Add(type);
      }
      foreach (var type in unitTestTypes)
        CreateDummyEntryPoint(typeCollector, library, frontEnd, type);
    }

    public void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderLibrary library, FrontEndTranslator frontEnd, ShaderType shaderType)
    {

      var voidType = library.FindType(new TypeKey(typeof(void)));
      var dummyMain = frontEnd.CreateFunction(shaderType, voidType, "dummyMain", null, null);
      dummyMain.mResultType = frontEnd.CreateFunctionType(voidType, null, null);
      dummyMain.DebugInfo.Name = "dummyMain";
      dummyMain.mBlocks.Add(new ShaderBlock());
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;
      context.mCurrentFunction = dummyMain;
      context.mCurrentBlock = dummyMain.mBlocks[0];
      var selfOp = frontEnd.CreateOpVariable(shaderType, context);
      selfOp.DebugInfo.Name = "self";
      if (shaderType.mImplicitConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mImplicitConstructor);
        fnParamOps.Add(selfOp);
        frontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }
      else if (shaderType.mPreConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mPreConstructor);
        fnParamOps.Add(selfOp);
        frontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }
      foreach (var function in shaderType.mFunctions)
      {
        if (function.mMeta.mAttributes.Contains("EntryPoint"))
        {
          var fnParamOps = new List<IShaderIR>();
          fnParamOps.Add(function);
          fnParamOps.Add(selfOp);
          frontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
        }
      }

      frontEnd.FixupBlockTerminators(dummyMain);
      typeCollector.Visit(dummyMain);

      var dummyEntryPoint = new ShaderEntryPointInfo();
      dummyEntryPoint.mEntryPointFunction = dummyMain;
      dummyEntryPoint.mStageType = shaderType.mFragmentType;
      // Execution modes are only valid in pixel shaders
      if(dummyEntryPoint.mStageType == FragmentType.Pixel)
      {
        var executionModeLiteral = frontEnd.CreateConstantLiteral(library.FindType(new TypeKey(typeof(int))), ((int)(Spv.ExecutionMode.ExecutionModeOriginUpperLeft)).ToString());
        var executionModeOp = frontEnd.CreateOp(OpInstructionType.OpExecutionMode, null, new List<IShaderIR> { dummyMain, executionModeLiteral });
        dummyEntryPoint.mExecutionModesBlock.mOps.Add(executionModeOp);
      }
      foreach (var globalStatic in library.mStaticGlobals)
      {
        dummyEntryPoint.mGlobalVariablesBlock.mLocalVariables.Add(globalStatic.Value);
      }
      typeCollector.Visit(dummyEntryPoint);
    }
  }
}
