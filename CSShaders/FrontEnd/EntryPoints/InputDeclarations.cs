namespace CSShaders
{
  /// <summary>
  /// Helper to generate inputs. This handles declarations of inputs, copying of inputs, etc...
  /// </summary>
  class InputDeclarations
  {
    public static ShaderOp GenerateInputStruct(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      if (interfaceSet.Fields.Count == 0)
        return null;

      var typeName = shaderType.mMeta.mName + "_Inputs";
      var instanceName = "In";
      var instanceOp = EntryPointGenerationShared.GenerateInterfaceStructAndOp(translator, interfaceSet.Fields, typeName, instanceName, StorageClass.Input);
      var instanceType = instanceOp.mResultType.GetDereferenceType();
      Decorations.AddDecorationBlock(translator, instanceType, decorationsBlock);
      Decorations.AddDecorationLocation(translator, instanceOp, 0, decorationsBlock);
      declarationBlock.mLocalVariables.Add(instanceOp);
      return instanceOp;
    }

    public static void GenerateInputFields(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      if (interfaceSet.Fields.Count == 0)
        return;

      var inputsName = shaderType.mMeta.mName + "_Inputs";
      EntryPointGenerationShared.GenerateInterfaceGlobalFields(translator, interfaceSet.Fields, inputsName, "In", StorageClass.Input);
      var location = 0;
      foreach (var inputField in interfaceSet)
      {
        var fieldOp = interfaceSet.GetFieldInstance(translator, inputField, null);
        Decorations.AddDecorationLocation(translator, fieldOp, location, decorationsBlock);
        ++location;
        declarationBlock.mLocalVariables.Add(fieldOp);
      }
    }

    public static void GenerateCopyInputsFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo)
    {
      // No inputs to copy, don't generate anything
      if (interfaceInfo.HardwareBuiltInInputs.Count == 0 && interfaceInfo.StageInputs.Count == 0 && interfaceInfo.UniformBuffers.Count == 0)
        return;

      interfaceInfo.CopyInputsFunction = EntryPointGenerationShared.GenerateEntryPointCopyFunction(translator, shaderType, "CopyInputs");

      var thisOp = interfaceInfo.CopyInputsFunction.ShaderTypeInstanceOp;
      var context = interfaceInfo.CopyInputsFunction.Context;

      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.HardwareBuiltInInputs, InterfaceFieldCopyMode.Input, context);
      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.StageInputs, InterfaceFieldCopyMode.Input, context);

      foreach (var uniformBuffer in interfaceInfo.UniformBuffers.Buffers.Values)
        EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, uniformBuffer.Fields, InterfaceFieldCopyMode.Input, context);

      translator.FixupBlockTerminators(interfaceInfo.CopyInputsFunction.ShaderFunction);
    }
  }
}
