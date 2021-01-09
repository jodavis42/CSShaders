namespace CSShaders
{
  /// <summary>
  /// Helper to generate uniforms for entry points. This includes declarations, copying, etc...
  /// </summary>
  public class UniformInputs
  {
    public static Shader.UniformInput ParseAttribute(ShaderAttribute attribute)
    {
      var uniformAttribute = new Shader.UniformInput();
      foreach (var namedArg in attribute.Attribute.NamedArguments)
      {
        if (namedArg.Key == "DescriptorSet")
          uniformAttribute.DescriptorSet = (int)namedArg.Value.Value;
        else if (namedArg.Key == "BindingId")
          uniformAttribute.BindingId = (int)namedArg.Value.Value;
      }

      return uniformAttribute;
    }

    public static void DeclareUniformBuffer(FrontEndTranslator translator, UniformBufferInterface uniformBuffer, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      uniformBuffer.InterfaceInstance = EntryPointGenerationShared.GenerateInterfaceStructAndOp(translator, uniformBuffer.Fields.Fields, uniformBuffer.TypeName, uniformBuffer.InstanceName, StorageClass.Uniform);

      var instanceType = uniformBuffer.InterfaceInstance.mResultType.GetDereferenceType();
      Decorations.AddDecorationDescriptorSet(translator, instanceType, uniformBuffer.DescriptorSet, decorationsBlock);
      Decorations.AddDecorationBinding(translator, instanceType, uniformBuffer.BindingId, decorationsBlock);
      Decorations.AddDecorationBlock(translator, instanceType, decorationsBlock);
      Decorations.DecorateOffsets(translator, instanceType, decorationsBlock);

      declarationBlock.mLocalVariables.Add(uniformBuffer.InterfaceInstance);
    }

    public static void DeclareUniformBuffers(FrontEndTranslator translator, UniformBufferSet uniformConstantSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      foreach (var uniformBuffer in uniformConstantSet.Buffers.Values)
      {
        UniformInputs.DeclareUniformBuffer(translator, uniformBuffer, declarationBlock, decorationsBlock);
      }
    }

    public static void DeclareUniformConstant(FrontEndTranslator translator, UniformBufferInterface uniformConstant, ShaderBlock decorationsBlock)
    {
      var instance = uniformConstant.InterfaceInstance;
      Decorations.AddDecorationDescriptorSet(translator, instance, uniformConstant.DescriptorSet, decorationsBlock);
      Decorations.AddDecorationBinding(translator, instance, uniformConstant.BindingId, decorationsBlock);
    }

    public static void DeclareUniformConstants(FrontEndTranslator translator, UniformBufferSet uniformConstantSet, ShaderBlock decorationsBlock)
    {
      foreach (var uniformConstant in uniformConstantSet.Buffers.Values)
      {
        UniformInputs.DeclareUniformConstant(translator, uniformConstant, decorationsBlock);
      }
    }
  }
}
