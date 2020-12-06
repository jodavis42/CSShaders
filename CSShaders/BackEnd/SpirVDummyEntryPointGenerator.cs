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
        if (type.mMeta.mAttributes.Contains("UnitTest") && type.mEntryPoints.Count == 0)
          unitTestTypes.Add(type);
      }
      foreach (var type in unitTestTypes)
        CreateDummyEntryPoint(typeCollector, library, frontEnd, type);
    }

    public void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderLibrary library, FrontEndTranslator frontEnd, ShaderType shaderType)
    {
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      EntryPointGeneration.GenerateSpirVEntryPointFunction(frontEnd, shaderType, "dummyMain", context);
      ShaderFunction spirvEntryPointFn = context.mCurrentFunction;
      EntryPointGeneration.ConstructShaderType(frontEnd, shaderType, context);

      foreach (var function in shaderType.mFunctions)
      {
        if (function.mMeta.mAttributes.Contains("EntryPoint"))
        {
          EntryPointGeneration.CallEntryPoint(frontEnd, function, context);
          frontEnd.FixupBlockTerminators(spirvEntryPointFn);
        }
      }

      frontEnd.FixupBlockTerminators(spirvEntryPointFn);
      typeCollector.Visit(spirvEntryPointFn);

      var dummyEntryPoint = new ShaderEntryPointInfo();
      dummyEntryPoint.mEntryPointFunction = spirvEntryPointFn;
      dummyEntryPoint.mStageType = shaderType.mFragmentType;
      // Execution modes are only valid in pixel shaders
      if(dummyEntryPoint.mStageType == FragmentType.Pixel || dummyEntryPoint.mStageType == FragmentType.None)
        EntryPointGeneration.AddExecutionMode(frontEnd, dummyEntryPoint, dummyEntryPoint.mEntryPointFunction, Spv.ExecutionMode.ExecutionModeOriginUpperLeft);
      
      foreach (var globalStatic in library.mStaticGlobals)
        dummyEntryPoint.mGlobalVariablesBlock.mLocalVariables.Add(globalStatic.Value);
      
      typeCollector.Visit(dummyEntryPoint);
    }
  }
}
