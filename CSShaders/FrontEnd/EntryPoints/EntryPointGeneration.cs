using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Helper to generate entry points for different shader types. Entry point generation is one of the most complicated areas
  /// in making shaders due to inputs/outputs/uniforms/etc... being handled very specifically for each shader stage.
  /// Several different stages require different groupings of data, descriptions of fields, etc...
  /// </summary>
  public class EntryPointGeneration
  {
    public static ShaderEntryPointInfo Generate(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      if (shaderType.mFragmentType == FragmentType.Vertex)
        return GenerateVertex(translator, shaderType, function);
      else if(shaderType.mFragmentType == FragmentType.Pixel)
        return GeneratePixel(translator, shaderType, function);
      return null;
    }

    public static ShaderEntryPointInfo GenerateVertex(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var entryPoint = new ShaderEntryPointInfo();
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      var interfaceInfo = new EntryPointInterfaceInfo();
      EntryPointGenerationShared.CollectInterface(translator, shaderType, interfaceInfo);

      // Build inputs block
      EntryPointGenerationShared.GenerateHardwareBuiltIns(translator, interfaceInfo.HardwareBuiltInInputs, StorageClass.Input, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      InputDeclarations.GenerateInputFields(translator, shaderType, interfaceInfo.StageInputs, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      EntryPointGenerationShared.GenerateHardwareBuiltIns(translator, interfaceInfo.HardwareBuiltInOutputs, StorageClass.Output, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      OutputDeclarations.GenerateOutputBlockStruct(translator, shaderType, interfaceInfo.StageOutputs, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      UniformDeclarations.DeclareUniformBuffers(translator, interfaceInfo.UniformBuffers, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      UniformDeclarations.DeclareUniformConstants(translator, interfaceInfo.ConstantUniforms, entryPoint.mDecorations);

      // Create the functions required to run an entry point
      string entryPointName = EntryPointGenerationShared.GenerateEntryPointFunctionName(translator, shaderType, function);
      CreateGlobalVariableInitializeFunction(translator, shaderType, entryPoint, interfaceInfo, context);
      InputDeclarations.GenerateCopyInputsFunction(translator, shaderType, entryPoint, interfaceInfo);
      OutputDeclarations.GenerateCopyOutputsFunction(translator, shaderType, entryPoint, interfaceInfo);
      var entryPointFunction = GenerateSpirVEntryPointFunction(translator, shaderType, entryPointName);

      // Generate the entry point function body
      translator.TryGenerateFunctionCall(entryPoint.mGlobalsInitializationFunction, null, null, entryPointFunction.Context);
      EntryPointGenerationShared.ConstructShaderType(translator, shaderType, entryPointFunction.Context);

      var entryPointThisOp = entryPointFunction.Context.mThisOp;
      translator.TryGenerateFunctionCall(interfaceInfo.CopyInputsFunction.ShaderFunction, entryPointThisOp, null, entryPointFunction.Context);
      translator.TryGenerateFunctionCall(function, entryPointThisOp, null, entryPointFunction.Context);
      translator.TryGenerateFunctionCall(interfaceInfo.CopyOutputsFunction.ShaderFunction, entryPointThisOp, null, entryPointFunction.Context);
      translator.FixupBlockTerminators(entryPointFunction.ShaderFunction);

      // Make sure all global variables are added to the interface of the entry point
      AddGlobalVariables(translator, entryPoint, interfaceInfo);

      entryPoint.mEntryPointFunction = entryPointFunction.ShaderFunction;
      entryPoint.mStageType = shaderType.mFragmentType;
      shaderType.mEntryPoints.Add(entryPoint);

      return entryPoint;
    }

    public static ShaderEntryPointInfo GeneratePixel(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var entryPoint = new ShaderEntryPointInfo();
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      var interfaceInfo = new EntryPointInterfaceInfo();
      EntryPointGenerationShared.CollectInterface(translator, shaderType, interfaceInfo);

      // Build inputs block
      EntryPointGenerationShared.GenerateHardwareBuiltIns(translator, interfaceInfo.HardwareBuiltInInputs, StorageClass.Input, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      InputDeclarations.GenerateInputStruct(translator, shaderType, interfaceInfo.StageInputs, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      EntryPointGenerationShared.GenerateHardwareBuiltIns(translator, interfaceInfo.HardwareBuiltInOutputs, StorageClass.Output, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      OutputDeclarations.GenerateOutputFields(translator, shaderType, interfaceInfo.StageOutputs, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      UniformDeclarations.DeclareUniformBuffers(translator, interfaceInfo.UniformBuffers, entryPoint.mInterfaceVariables, entryPoint.mDecorations);
      UniformDeclarations.DeclareUniformConstants(translator, interfaceInfo.ConstantUniforms, entryPoint.mDecorations);

      // Create the functions required to run an entry point
      string entryPointName = EntryPointGenerationShared.GenerateEntryPointFunctionName(translator, shaderType, function);
      CreateGlobalVariableInitializeFunction(translator, shaderType, entryPoint, interfaceInfo, context);
      InputDeclarations.GenerateCopyInputsFunction(translator, shaderType, entryPoint, interfaceInfo);
      OutputDeclarations.GenerateCopyOutputsFunction(translator, shaderType, entryPoint, interfaceInfo);
      var entryPointFunction = GenerateSpirVEntryPointFunction(translator, shaderType, entryPointName);

      // Generate the entry point function body
      translator.TryGenerateFunctionCall(entryPoint.mGlobalsInitializationFunction, null, null, entryPointFunction.Context);
      EntryPointGenerationShared.ConstructShaderType(translator, shaderType, entryPointFunction.Context);

      var entryPointThisOp = entryPointFunction.Context.mThisOp;
      translator.TryGenerateFunctionCall(interfaceInfo.CopyInputsFunction.ShaderFunction, entryPointThisOp, null, entryPointFunction.Context);
      translator.TryGenerateFunctionCall(function, entryPointThisOp, null, entryPointFunction.Context);
      translator.TryGenerateFunctionCall(interfaceInfo.CopyOutputsFunction.ShaderFunction, entryPointThisOp, null, entryPointFunction.Context);
      translator.FixupBlockTerminators(entryPointFunction.ShaderFunction);

      // Make sure all global variables are added to the interface of the entry point
      AddGlobalVariables(translator, entryPoint, interfaceInfo);

      entryPoint.mEntryPointFunction = entryPointFunction.ShaderFunction;
      entryPoint.mStageType = shaderType.mFragmentType;
      shaderType.mEntryPoints.Add(entryPoint);

      EntryPointGenerationShared.AddExecutionMode(translator, entryPoint, entryPoint.mEntryPointFunction, Spv.ExecutionMode.ExecutionModeOriginUpperLeft);
      return entryPoint;
    }

    public static EntryPointFunction GenerateSpirVEntryPointFunction(FrontEndTranslator translator, ShaderType shaderType, string entryPointFnName)
    {
      EntryPointFunction entryPointFunction = new EntryPointFunction();

      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      entryPointFunction.ShaderFunction = translator.CreateFunctionAndType(shaderType, voidType, entryPointFnName, null, null);
      entryPointFunction.ShaderFunction.mBlocks.Add(new ShaderBlock());
      entryPointFunction.Context = new FrontEndContext();
      entryPointFunction.Context.mCurrentType = shaderType;
      entryPointFunction.Context.mCurrentFunction = entryPointFunction.ShaderFunction;
      entryPointFunction.Context.mCurrentBlock = entryPointFunction.ShaderFunction.mBlocks[0];

      return entryPointFunction;
    }

    public static void CreateGlobalVariableInitializeFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      // First check if any global variables that need the variable initialization function exist. If not then don't emit anything.
      bool globalVariablesExist = false;
      foreach (var globalField in interfaceInfo.GlobalFields)
      {
        if (globalField.InitialValue != null)
        globalVariablesExist = true;
      }
      if (!globalVariablesExist)
        return;

      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var initGlobalFunction = translator.CreateFunctionAndType(shaderType, voidType, "InitGlobals", null, null);
      initGlobalFunction.mBlocks.Add(new ShaderBlock());
      entryPoint.mGlobalsInitializationFunction = initGlobalFunction;

      // Add the store op for each global variable
      context.mCurrentFunction = initGlobalFunction;
      context.mCurrentBlock = initGlobalFunction.mBlocks[0];
      foreach (var globalField in interfaceInfo.GlobalFields)
      {
        if (globalField.InitialValue != null)
          translator.CreateStoreOp(context.mCurrentBlock, globalField.InstanceOp, globalField.InitialValue);
      }
      translator.FixupBlockTerminators(context.mCurrentFunction);
    }

    public static void AddGlobalVariables(FrontEndTranslator translator, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo)
    {
      foreach (var globalShaderField in interfaceInfo.GlobalFields)
      {
        entryPoint.mGlobalVariablesBlock.mLocalVariables.Add(globalShaderField.InstanceOp);
      }
    }
  }
}
