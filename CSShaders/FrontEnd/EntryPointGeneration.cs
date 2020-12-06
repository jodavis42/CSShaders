using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class ShaderInterfaceField
  {
    public ShaderField ShaderField;
  }

  public class EntryPointInterfaceInfo
  {
    public List<ShaderInterfaceField> StageInputs = new List<ShaderInterfaceField>();
    public List<ShaderInterfaceField> StageOutputs = new List<ShaderInterfaceField>();
    public List<GlobalShaderField> GlobalFields = new List<GlobalShaderField>();
    public ShaderFunction CopyInputsFunction;
    public ShaderFunction CopyOutputsFunction;
  }

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
      return GenerateCommonEntryPointInfo(translator, shaderType, function);
    }

    public static ShaderEntryPointInfo GeneratePixel(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var entryPoint = GenerateCommonEntryPointInfo(translator, shaderType, function);
      AddExecutionMode(translator, entryPoint, entryPoint.mEntryPointFunction, Spv.ExecutionMode.ExecutionModeOriginUpperLeft);
      return entryPoint;
    }

    public static ShaderEntryPointInfo GenerateCommonEntryPointInfo(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var entryPoint = new ShaderEntryPointInfo();
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      var interfaceInfo = new EntryPointInterfaceInfo();
      CollectInterface(translator, shaderType, interfaceInfo);

      // Create the functions required to run an entry point
      string entryPointName = GenerateEntryPointFunctionName(translator, shaderType, function);
      CreateGlobalVariableInitializeFunction(translator, shaderType, entryPoint, interfaceInfo, context);
      GenerateCopyInputsFunction(translator, shaderType, entryPoint, interfaceInfo, context);
      GenerateCopyOutputsFunction(translator, shaderType, entryPoint, interfaceInfo, context);
      GenerateSpirVEntryPointFunction(translator, shaderType, entryPointName, context);

      // Generate the entry point function body
      ShaderFunction spirvEntryPointFn = context.mCurrentFunction;
      context.mCurrentFunction = spirvEntryPointFn;
      context.mCurrentBlock = spirvEntryPointFn.mBlocks[0];
      CallGlobalVariableInitializeFunction(translator, entryPoint, interfaceInfo, context);
      ConstructShaderType(translator, shaderType, context);
      CallCopyInputsFunction(translator, interfaceInfo, context);
      if(function != null)
        CallEntryPoint(translator, function, context);
      CallCopyOutputsFunction(translator, interfaceInfo, context);
      translator.FixupBlockTerminators(spirvEntryPointFn);

      // Make sure all global variables are added to the interface of the entry point
      AddGlobalVariables(translator, entryPoint, interfaceInfo);

      entryPoint.mEntryPointFunction = spirvEntryPointFn;
      entryPoint.mStageType = shaderType.mFragmentType;
      shaderType.mEntryPoints.Add(entryPoint);
      
      return entryPoint;
    }

    public static string GenerateEntryPointFunctionName(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction shaderFunction)
    {
      if (shaderFunction == null)
        return "EntryPoint";
      return shaderFunction.mMeta.mName + "_EntryPoint";
    }

    public static void GenerateSpirVEntryPointFunction(FrontEndTranslator translator, ShaderType shaderType, string entryPointFnName, FrontEndContext context)
    {
      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var entryPointFn = translator.CreateFunctionAndType(shaderType, voidType, entryPointFnName, null, null);
      entryPointFn.mBlocks.Add(new ShaderBlock());

      context.mCurrentFunction = entryPointFn;
      context.mCurrentBlock = entryPointFn.mBlocks[0];
    }

    public static void ConstructShaderType(FrontEndTranslator translator, ShaderType shaderType, FrontEndContext context)
    {
      var selfOp = translator.ConstructAndInitializeOpVariable(shaderType, context);
      selfOp.DebugInfo.Name = "self";
      context.mThisOp = selfOp;
    }

    public static void CallFunction(FrontEndTranslator translator, ShaderFunction function, List<IShaderIR> fnParamOps, FrontEndContext context)
    {
      var library = translator.mCurrentLibrary;
      var functionType = function.GetFunctionType();
      var resultType = functionType.mParameters[0] as ShaderType;
      translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, resultType, fnParamOps);
    }

    public static void CallEntryPoint(FrontEndTranslator translator, ShaderFunction function, FrontEndContext context)
    {
      var library = translator.mCurrentLibrary;
      var fnParamOps = new List<IShaderIR>();
      fnParamOps.Add(function);
      fnParamOps.Add(context.mThisOp);
      CallFunction(translator, function, fnParamOps, context);
    }

    public static void AddExecutionMode(FrontEndTranslator translator, ShaderEntryPointInfo entryPointInfo, ShaderFunction entryPointFn, Spv.ExecutionMode executionMode)
    {
      var library = translator.mCurrentLibrary;
      var executionModeLiteral = translator.CreateConstantLiteral(library.FindType(new TypeKey(typeof(int))), ((int)executionMode).ToString());
      var executionModeOp = translator.CreateOp(OpInstructionType.OpExecutionMode, null, new List<IShaderIR> { entryPointFn, executionModeLiteral });
      entryPointInfo.mExecutionModesBlock.mOps.Add(executionModeOp);
    }

    public static void CreateGlobalVariableInitializeFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
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

    public static void GenerateCopyInputsFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      interfaceInfo.CopyInputsFunction = GenerateFunction_ShaderTypeParam(translator, shaderType, "CopyInputs");
      translator.FixupBlockTerminators(interfaceInfo.CopyInputsFunction);
    }

    public static void GenerateCopyOutputsFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      interfaceInfo.CopyOutputsFunction = GenerateFunction_ShaderTypeParam(translator, shaderType, "CopyOutputs");
      translator.FixupBlockTerminators(interfaceInfo.CopyOutputsFunction);
    }

    public static ShaderFunction GenerateFunction_ShaderTypeParam(FrontEndTranslator translator, ShaderType shaderType, string functionName)
    {
      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var thisType = shaderType.FindPointerType(StorageClass.Function);
      var fnArgsTypes = new List<ShaderType>() { thisType };

      var function = translator.CreateFunctionAndType(shaderType, voidType, functionName, null, fnArgsTypes);
      var thisOp = translator.CreateOp(function.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
      thisOp.DebugInfo.Name = "self";
      function.mBlocks.Add(new ShaderBlock());
      return function;
    }

    public static void CallCopyInputsFunction(FrontEndTranslator translator, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      var function = interfaceInfo.CopyInputsFunction;
      var fnParamOps = new List<IShaderIR>() { function, context.mThisOp };
      CallFunction(translator, function, fnParamOps, context);
    }

    public static void CallCopyOutputsFunction(FrontEndTranslator translator, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      var function = interfaceInfo.CopyOutputsFunction;
      var fnParamOps = new List<IShaderIR>() { function, context.mThisOp };
      CallFunction(translator, function, fnParamOps, context);
    }

    public static void CallGlobalVariableInitializeFunction(FrontEndTranslator translator, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo, FrontEndContext context)
    {
      var fnParamOps = new List<IShaderIR>() { entryPoint.mGlobalsInitializationFunction };
      CallFunction(translator, entryPoint.mGlobalsInitializationFunction, fnParamOps, context);
    }

    public static void AddGlobalVariables(FrontEndTranslator translator, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo)
    {
      foreach (var globalShaderField in interfaceInfo.GlobalFields)
      {
        entryPoint.mGlobalVariablesBlock.mLocalVariables.Add(globalShaderField.InstanceOp);
      }
    }

    public static void CollectInterface(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      CollectStageInputsOuputs(translator, shaderType, interfaceInfo);

      // @JoshD: Hack for now, this should really check what's reachable.
      foreach (var globalStatic in translator.mCurrentLibrary.mStaticGlobals.Values)
        interfaceInfo.GlobalFields.Add(globalStatic);
    }

    public static void CollectStageInputsOuputs(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      foreach(var field in shaderType.mFields)
      {
        var attributes = field.mMeta.mAttributes;
        if (attributes.Contains(typeof(Shader.StageInput)))
        {
          interfaceInfo.StageInputs.Add(new ShaderInterfaceField() { ShaderField = field });
        }
        if (attributes.Contains(typeof(Shader.StageOutput)))
        {
          interfaceInfo.StageOutputs.Add(new ShaderInterfaceField() { ShaderField = field });
        }
      }
    }
  }
}
