namespace CSShaders
{
  /// <summary>
  /// Helper to generate outputs. This handles declarations of outputs, copying of outputs, etc...
  /// </summary>
  class OutputDeclarations
  {
    public static ShaderOp GenerateOutputStruct(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      if (interfaceSet.Fields.Count == 0)
        return null;

      var typeName = shaderType.mMeta.mName + "_Outputs";
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
      interfaceInfo.CopyOutputsFunction = EntryPointGenerationShared.GenerateEntryPointCopyFunction(translator, shaderType, "CopyOutputs");

      var thisOp = interfaceInfo.CopyOutputsFunction.ShaderTypeInstanceOp;
      var context = interfaceInfo.CopyOutputsFunction.Context;

      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.HardwareBuiltInOutputs, InterfaceFieldCopyMode.Output, context);
      EntryPointGenerationShared.CopyInterfaceFields(translator, thisOp, interfaceInfo.StageOutputs, InterfaceFieldCopyMode.Output, context);

      translator.FixupBlockTerminators(interfaceInfo.CopyOutputsFunction.ShaderFunction);
    }
  }
}
