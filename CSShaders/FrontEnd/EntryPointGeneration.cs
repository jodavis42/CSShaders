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
  }

  /// <summary>
  /// Helper to generate entry points for different shader types. Entry point generation is one of the most complicated areas
  /// in making shaders due to inputs/outputs/uniforms/etc... being handled very specifically for each shader stage.
  /// Several different stages require different groupings of data, descriptions of fields, etc...
  /// </summary>
  public class EntryPointGeneration
  {
    public static void Generate(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      if (shaderType.mFragmentType == FragmentType.Vertex)
        GenerateVertex(translator, shaderType, function);
      else if(shaderType.mFragmentType == FragmentType.Pixel)
        GeneratePixel(translator, shaderType, function);
    }

    public static void GenerateVertex(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      var interfaceInfo = new EntryPointInterfaceInfo();
      CollectStageInputsOuputs(translator, shaderType, interfaceInfo);

      GenerateSpirVEntryPointFunction(translator, shaderType, GenerateEntryPointFunctionName(function), context);
      ShaderFunction spirvEntryPointFn = context.mCurrentFunction;
      ConstructShaderType(translator, shaderType, context);

      CallEntryPoint(translator, function, context);
      translator.FixupBlockTerminators(spirvEntryPointFn);

      var entryPoint = new ShaderEntryPointInfo();
      entryPoint.mEntryPointFunction = spirvEntryPointFn;
      entryPoint.mStageType = shaderType.mFragmentType;
      shaderType.mEntryPoints.Add(entryPoint);

      AddGlobalVariables(translator, entryPoint);
    }

    public static void GeneratePixel(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction function)
    {
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      var interfaceInfo = new EntryPointInterfaceInfo();
      CollectStageInputsOuputs(translator, shaderType, interfaceInfo);

      GenerateSpirVEntryPointFunction(translator, shaderType, GenerateEntryPointFunctionName(function), context);
      ShaderFunction spirvEntryPointFn = context.mCurrentFunction;
      ConstructShaderType(translator, shaderType, context);

      CallEntryPoint(translator, function, context);
      translator.FixupBlockTerminators(spirvEntryPointFn);

      var entryPoint = new ShaderEntryPointInfo();
      shaderType.mEntryPoints.Add(entryPoint);

      entryPoint.mEntryPointFunction = spirvEntryPointFn;
      entryPoint.mStageType = shaderType.mFragmentType;
      AddExecutionMode(translator, entryPoint, spirvEntryPointFn, Spv.ExecutionMode.ExecutionModeOriginUpperLeft);

      AddGlobalVariables(translator, entryPoint);
    }

    public static string GenerateEntryPointFunctionName(ShaderFunction shaderFunction)
    {
      return "entryPoint_" + shaderFunction.mMeta.mName;
    }

    public static void GenerateSpirVEntryPointFunction(FrontEndTranslator translator, ShaderType shaderType, string entryPointFnName, FrontEndContext context)
    {
      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var entryPointFn = translator.CreateFunction(shaderType, voidType, entryPointFnName, null, null);
      entryPointFn.mResultType = translator.CreateFunctionType(voidType, null, null);
      entryPointFn.DebugInfo.Name = entryPointFnName;
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

    public static void CallEntryPoint(FrontEndTranslator translator, ShaderFunction function, FrontEndContext context)
    {
      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var fnParamOps = new List<IShaderIR>();
      fnParamOps.Add(function);
      fnParamOps.Add(context.mThisOp);
      translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
    }

    public static void AddExecutionMode(FrontEndTranslator translator, ShaderEntryPointInfo entryPointInfo, ShaderFunction entryPointFn, Spv.ExecutionMode executionMode)
    {
      var library = translator.mCurrentLibrary;
      var executionModeLiteral = translator.CreateConstantLiteral(library.FindType(new TypeKey(typeof(int))), ((int)executionMode).ToString());
      var executionModeOp = translator.CreateOp(OpInstructionType.OpExecutionMode, null, new List<IShaderIR> { entryPointFn, executionModeLiteral });
      entryPointInfo.mExecutionModesBlock.mOps.Add(executionModeOp);
    }

    public static void AddGlobalVariables(FrontEndTranslator translator, ShaderEntryPointInfo entryPoint)
    {
      foreach (var globalStatic in translator.mCurrentLibrary.mStaticGlobals)
      {
        entryPoint.mGlobalVariablesBlock.mLocalVariables.Add(globalStatic.Value);
      }
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
