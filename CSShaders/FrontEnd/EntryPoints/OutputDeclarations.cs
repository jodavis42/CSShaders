namespace CSShaders
{
  /// <summary>
  /// Helper to generate outputs. This handles declarations of outputs, copying of outputs, etc...
  /// </summary>
  class OutputDeclarations
  {
    public static void GenerateOutputFields(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      if (interfaceSet.Fields.Count == 0)
        return;

      var outputsName = shaderType.mMeta.mName + "_Outputs";
      EntryPointGenerationShared.GenerateInterfaceGlobalFields(translator, interfaceSet.Fields, outputsName, "Out", StorageClass.Output);
      foreach (var inputField in interfaceSet)
      {
        var fieldOp = interfaceSet.GetFieldInstance(translator, inputField, null);
        declarationBlock.mLocalVariables.Add(fieldOp);
      }
      Decorations.LocationCallback locationCallback = (ShaderField field, out int location, out int component) =>
      {
        location = component = -1;
        var attribute = field.mMeta.mAttributes.FindFirstAttribute(typeof(Shader.StageOutput));
        if (attribute == null)
          return false;
        var parsedAttribute = AttributeExtensions.ParseStageOutput(attribute);
        location = parsedAttribute.Location;
        component = parsedAttribute.Component;
        return false;
      };
      Decorations.AddDecorationLocations(translator, shaderType, interfaceSet, locationCallback, decorationsBlock);
    }

    public static ShaderOp GenerateOutputStruct(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      if (interfaceSet.Fields.Count == 0)
        return null;

      var typeName = shaderType.mMeta.TypeName.CloneAsAppended("_Outputs");
      var instanceName = "Out";
      var instanceOp = EntryPointGenerationShared.GenerateInterfaceStructAndOp(translator, interfaceSet.Fields, typeName, instanceName, StorageClass.Output);
      Decorations.AddDecorationLocation(translator, instanceOp, 0, decorationsBlock);
      declarationBlock.mLocalVariables.Add(instanceOp);
      return instanceOp;
    }

    public static ShaderOp GenerateOutputBlockStruct(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      var instanceOp = GenerateOutputStruct(translator, shaderType, interfaceSet, declarationBlock, decorationsBlock);
      if (instanceOp == null)
        return null;

      var instanceType = instanceOp.mResultType.GetDereferenceType();
      Decorations.AddDecorationBlock(translator, instanceType, decorationsBlock);
      return instanceOp;
    }

    public static void GenerateCopyOutputsFunction(FrontEndTranslator translator, ShaderType shaderType, ShaderEntryPointInfo entryPoint, EntryPointInterfaceInfo interfaceInfo)
    {
      // No outputs to copy, don't generate anything
      if (interfaceInfo.HardwareBuiltInOutputs.Count == 0 && interfaceInfo.StageOutputs.Count == 0)
        return;

      interfaceInfo.CopyOutputsFunction = EntryPointGenerationShared.GenerateEntryPointCopyFunction(translator, shaderType, "CopyOutputs");

      var thisOp = interfaceInfo.CopyOutputsFunction.ShaderTypeInstanceOp;
      var context = interfaceInfo.CopyOutputsFunction.Context;

      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.HardwareBuiltInOutputs, InterfaceFieldCopyMode.Output, context);
      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.StageOutputs, InterfaceFieldCopyMode.Output, context);

      translator.FixupBlockTerminators(interfaceInfo.CopyOutputsFunction.ShaderFunction);
    }
  }
}
