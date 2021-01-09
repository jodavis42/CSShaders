using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class EntryPointFunction
  {
    public FrontEndContext Context;
    public ShaderFunction ShaderFunction;
    public ShaderOp ShaderTypeInstanceOp;
  }

  public class EntryPointInterfaceInfo
  {
    public UniformBufferSet UniformBuffers = new UniformBufferSet();
    public UniformBufferSet ConstantUniforms = new UniformBufferSet();
    public ShaderInterfaceSet StageInputs = new ShaderInterfaceSet();
    public ShaderInterfaceSet StageOutputs = new ShaderInterfaceSet();
    public ShaderInterfaceSet HardwareBuiltInInputs = new ShaderInterfaceSet();
    public ShaderInterfaceSet HardwareBuiltInOutputs = new ShaderInterfaceSet();
    public List<GlobalShaderField> GlobalFields = new List<GlobalShaderField>();
    public EntryPointFunction CopyInputsFunction = new EntryPointFunction();
    public EntryPointFunction CopyOutputsFunction = new EntryPointFunction();
  }
  enum InterfaceFieldCopyMode { Input, Output };

  class EntryPointGenerationShared
  {
    public static void CollectInterface(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      CollectStageInputsOuputs(translator, shaderType, interfaceInfo);

      // @JoshD: Hack for now, this should really check what's reachable. This also needs to walk all dependent libraries
      foreach (var globalStatic in translator.mCurrentLibrary.mStaticGlobals.Values)
        interfaceInfo.GlobalFields.Add(globalStatic);
    }

    public static void CollectStageInputsOuputs(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      foreach (var field in shaderType.mFields)
      {
        var attributes = field.mMeta.mAttributes;
        ShaderAttributes.AttributeCallback stageInputCallback = (ShaderAttribute attribute) =>
        {
          interfaceInfo.StageInputs.Add(new ShaderInterfaceField() { ShaderField = field });
        };
        ShaderAttributes.AttributeCallback stageOutputCallback = (ShaderAttribute attribute) =>
        {
          interfaceInfo.StageOutputs.Add(new ShaderInterfaceField() { ShaderField = field });
        };
        ShaderAttributes.AttributeCallback uniformCallback = (ShaderAttribute attribute) =>
        {
          var interfaceField = new ShaderInterfaceField() { ShaderField = field };
          var uniformInput = UniformInputs.ParseAttribute(attribute);
          var uniformBuffer = interfaceInfo.UniformBuffers.GetOrCreate(uniformInput.DescriptorSet, uniformInput.BindingId);
          uniformBuffer.TypeName = string.Format("{0}_MaterialBuffer_{1}_{2}", shaderType.mMeta.mName, uniformInput.DescriptorSet, uniformInput.BindingId);
          uniformBuffer.InstanceName = string.Format("{0}_MaterialBuffer_{1}_{2}_Instance", shaderType.mMeta.mName, uniformInput.DescriptorSet, uniformInput.BindingId);
          uniformBuffer.Fields.Add(interfaceField);
        };
        ShaderAttributes.AttributeCallback hardwareBuiltInInputCallback = (ShaderAttribute attribute) =>
        {
          var arg0 = attribute.Attribute.ConstructorArguments[0];
          var interfaceField = new ShaderInterfaceField() { ShaderField = field };
          interfaceField.BuiltInType = (Spv.BuiltIn)Enum.Parse(typeof(Spv.BuiltIn), Enum.GetName(typeof(Shader.HardwareBuiltinType), arg0.Value), true);
          interfaceInfo.HardwareBuiltInInputs.Add(interfaceField);
        };
        ShaderAttributes.AttributeCallback hardwareBuiltInOutputCallback = (ShaderAttribute attribute) =>
        {
          var arg0 = attribute.Attribute.ConstructorArguments[0];
          var interfaceField = new ShaderInterfaceField() { ShaderField = field };
          interfaceField.BuiltInType = (Spv.BuiltIn)Enum.Parse(typeof(Spv.BuiltIn), Enum.GetName(typeof(Shader.HardwareBuiltinType), arg0.Value), true);
          interfaceInfo.HardwareBuiltInOutputs.Add(interfaceField);
        };

        attributes.ForeachAttribute(typeof(Shader.StageInput), stageInputCallback);
        attributes.ForeachAttribute(typeof(Shader.StageOutput), stageOutputCallback);
        attributes.ForeachAttribute(typeof(Shader.UniformInput), uniformCallback);
        attributes.ForeachAttribute(typeof(Shader.HardwareBuiltInInput), hardwareBuiltInInputCallback);
        attributes.ForeachAttribute(typeof(Shader.HardwareBuiltInOutput), hardwareBuiltInOutputCallback);
      }

      foreach (var field in shaderType.mStaticFields)
      {
        var attributes = field.mMeta.mAttributes;
        ShaderAttributes.AttributeCallback uniformCallback = (ShaderAttribute attribute) =>
        {
          var staticGlobalInstance = translator.mCurrentLibrary.mStaticGlobals.GetValueOrDefault(field);
          if (staticGlobalInstance == null)
            return;

          var uniformAttribute = UniformInputs.ParseAttribute(attribute);
          var uniformConstant = interfaceInfo.ConstantUniforms.GetOrCreate(uniformAttribute.DescriptorSet, uniformAttribute.BindingId);
          uniformConstant.InterfaceInstance = staticGlobalInstance.InstanceOp;
        };
        attributes.ForeachAttribute(typeof(Shader.UniformInput), uniformCallback);
      }
    }

    public static void GenerateInterfaceGlobalFields(FrontEndTranslator translator, List<ShaderInterfaceField> interfaceFields, string structName, string instanceName, StorageClass storageClass)
    {
      foreach (var interfaceField in interfaceFields)
      {
        var fieldPointerType = translator.FindOrCreatePointerType(interfaceField.ShaderField.mType, storageClass);
        var fieldOp = translator.CreateOp(OpInstructionType.OpVariable, fieldPointerType, null);
        ShaderInterfaceField.InstanceAccessDelegate fieldInstanceGetFunction = (FrontEndTranslator translator, ShaderInterfaceField interfaceField, FrontEndContext context) =>
        {
          return fieldOp;
        };
        fieldOp.DebugInfo.Name = interfaceField.ShaderField.DebugInfo.Name;
        interfaceField.GetInstance = fieldInstanceGetFunction;
      }
    }

    public static ShaderOp GenerateInterfaceStructAndOp(FrontEndTranslator translator, List<ShaderInterfaceField> interfaceFields, string structName, string instanceName, StorageClass storageClass)
    {
      var interfaceStruct = translator.CreateType(new TypeKey(structName), structName, OpType.Struct, null);
      foreach (var interfaceField in interfaceFields)
      {
        interfaceStruct.mFields.Add(interfaceField.ShaderField);
      }
      interfaceStruct.mStorageClass = storageClass;
      var opPointerType = translator.FindOrCreatePointerType(new TypeKey(structName), structName, storageClass);
      var op = translator.CreateOp(OpInstructionType.OpVariable, opPointerType, null);

      ShaderInterfaceField.InstanceAccessDelegate fieldInstanceGetFunction = (FrontEndTranslator translator, ShaderInterfaceField interfaceField, FrontEndContext context) =>
      {
        return translator.GenerateAccessChain(op, interfaceField.ShaderField.mMeta.mName, context);
      };

      foreach (var interfaceField in interfaceFields)
      {
        interfaceField.GetInstance = fieldInstanceGetFunction;
      }

      op.DebugInfo.Name = instanceName;
      return op;
    }

    public static void GenerateHardwareBuiltIns(FrontEndTranslator translator, ShaderInterfaceSet interfaceSet, StorageClass storageClass, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      foreach (var builtInInterfaceField in interfaceSet)
      {
        var pointerType = translator.FindOrCreatePointerType(builtInInterfaceField.ShaderField.mType, storageClass);
        var fieldInstanceOp = translator.CreateOp(OpInstructionType.OpVariable, pointerType, null);
        declarationBlock.mLocalVariables.Add(fieldInstanceOp);

        Decorations.AddDecorationBuiltIn(translator, fieldInstanceOp, builtInInterfaceField.BuiltInType, decorationsBlock);

        ShaderInterfaceField.InstanceAccessDelegate fieldInstanceGetFunction = (FrontEndTranslator translator, ShaderInterfaceField interfaceField, FrontEndContext context) =>
        {
          return fieldInstanceOp;
        };
        builtInInterfaceField.GetInstance = fieldInstanceGetFunction;
      }
    }

    public static void ConstructShaderType(FrontEndTranslator translator, ShaderType shaderType, FrontEndContext context)
    {
      var selfOp = translator.ConstructAndInitializeOpVariable(shaderType, context);
      selfOp.DebugInfo.Name = "self";
      context.mThisOp = selfOp;
    }

    public static void CopyInterfaceFields(FrontEndTranslator translator, ShaderOp thisOp, ShaderInterfaceSet interfaceSet, InterfaceFieldCopyMode copyMode, FrontEndContext context)
    {
      foreach (var interfaceField in interfaceSet)
      {
        var interfaceInstance = interfaceSet.GetFieldInstance(translator, interfaceField, context);
        if (interfaceInstance == null)
          continue;

        var shaderFieldInstance = translator.GenerateAccessChain(thisOp, interfaceField.ShaderField.mMeta.mName, context);
        if (copyMode == InterfaceFieldCopyMode.Input)
          translator.CreateStoreOp(context.mCurrentBlock, shaderFieldInstance, interfaceInstance);
        else
          translator.CreateStoreOp(context.mCurrentBlock, interfaceInstance, shaderFieldInstance);
      }
    }

    public static void AddExecutionMode(FrontEndTranslator translator, ShaderEntryPointInfo entryPointInfo, ShaderFunction entryPointFn, Spv.ExecutionMode executionMode)
    {
      var library = translator.mCurrentLibrary;
      var executionModeLiteral = translator.CreateConstantLiteral(library.FindType(new TypeKey(typeof(int))), ((int)executionMode).ToString());
      var executionModeOp = translator.CreateOp(OpInstructionType.OpExecutionMode, null, new List<IShaderIR> { entryPointFn, executionModeLiteral });
      entryPointInfo.mExecutionModesBlock.mOps.Add(executionModeOp);
    }

    public static string GenerateEntryPointFunctionName(FrontEndTranslator translator, ShaderType shaderType, ShaderFunction shaderFunction)
    {
      if (shaderFunction == null)
        return "EntryPoint";
      return shaderFunction.mMeta.mName + "_EntryPoint";
    }

    public static ShaderFunction GenerateFunction_ShaderTypeParam(FrontEndTranslator translator, ShaderType shaderType, string functionName, out ShaderOp thisOp)
    {
      var library = translator.mCurrentLibrary;
      var voidType = library.FindType(new TypeKey(typeof(void)));
      var thisType = shaderType.FindPointerType(StorageClass.Function);
      var fnArgsTypes = new List<ShaderType>() { thisType };

      var function = translator.CreateFunctionAndType(shaderType, voidType, functionName, null, fnArgsTypes);
      thisOp = translator.CreateOp(function.mParametersBlock, OpInstructionType.OpFunctionParameter, thisType, null);
      thisOp.DebugInfo.Name = "self";
      function.mBlocks.Add(new ShaderBlock());
      return function;
    }

    public static EntryPointFunction GenerateEntryPointCopyFunction(FrontEndTranslator translator, ShaderType shaderType, string functionName)
    {
      ShaderOp thisOp = null;
      var shaderFunction = GenerateFunction_ShaderTypeParam(translator, shaderType, functionName, out thisOp);
      var context = new FrontEndContext();

      context.mCurrentBlock = shaderFunction.mBlocks[0];
      context.mCurrentFunction = shaderFunction;

      var entryPointFunction = new EntryPointFunction();
      entryPointFunction.Context = context;
      entryPointFunction.ShaderFunction = shaderFunction;
      entryPointFunction.ShaderTypeInstanceOp = thisOp;
      return entryPointFunction;
    }
  }
}
