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

      var typeName = shaderType.mMeta.TypeName.CloneAsAppended("_Inputs");
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
      foreach (var inputField in interfaceSet)
      {
        var fieldOp = interfaceSet.GetFieldInstance(translator, inputField, null);
        declarationBlock.mLocalVariables.Add(fieldOp);
      }
      Decorations.LocationCallback locationCallback = (ShaderField field, out int location, out int component) =>
      {
        location = component = -1;
        var attribute = field.mMeta.mAttributes.FindFirstAttribute(typeof(Shader.StageInput));
        if (attribute == null)
          return false;
        var parsedAttribute = AttributeExtensions.ParseStageInput(attribute);
        location = parsedAttribute.Location;
        component = parsedAttribute.Component;
        return true;
      };
      Decorations.AddDecorationLocations(translator, shaderType, interfaceSet, locationCallback, decorationsBlock);
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
        UniformDeclarations.CopyUniformBufferFields(translator, thisOp, uniformBuffer, InterfaceFieldCopyMode.Input, context);

      translator.FixupBlockTerminators(interfaceInfo.CopyInputsFunction.ShaderFunction);
    }
  }
}
