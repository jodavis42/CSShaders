using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Helper to generate uniforms for entry points. This includes declarations, copying, etc...
  /// </summary>
  class UniformDeclarations
  {
    public static Shader.UniformBuffer Parse(ShaderAttribute attribute)
    {
      var uniformAttribute = new Shader.UniformBuffer() { BindingId = -1, DescriptorSet = 0 };
      foreach (var namedArg in attribute.Attribute.NamedArguments)
      {
        if (namedArg.Key == "DescriptorSet")
          uniformAttribute.DescriptorSet = (int)namedArg.Value.Value;
        else if (namedArg.Key == "BindingId")
          uniformAttribute.BindingId = (int)namedArg.Value.Value;
      }

      return uniformAttribute;
    }

    public static void GenerateUniforms(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      List<(ShaderField Field, Shader.UniformBuffer Attribute)> uniformBufferFieldsWithBindings = new List<(ShaderField Field, Shader.UniformBuffer Attribute)>();
      List<(ShaderField Field, Shader.UniformBuffer Attribute)> uniformBufferFieldsWithoutBindings = new List<(ShaderField Field, Shader.UniformBuffer Attribute)>();
      List<(ShaderField Field, ShaderAttribute Attribute)> uniformInputFields = new List<(ShaderField Field, ShaderAttribute Attribute)>();
      // Collect all UniformBuffers and Uniform Inputs
      foreach (var field in shaderType.mFields)
      {
        var attributes = field.mMeta.mAttributes;
        attributes.ForeachAttribute(typeof(Shader.UniformBufferField), (ShaderAttribute attribute) =>
        {
          uniformInputFields.Add((Field: field, Attribute: attribute));
        });
        // Collect Uniform Buffers into different lists depending on if they're binding is explicit or implicit
        attributes.ForeachAttribute(typeof(Shader.UniformBuffer), (ShaderAttribute attribute) =>
        {
          var uniformInput = Parse(attribute);
          if (uniformInput.BindingId == -1)
            uniformBufferFieldsWithoutBindings.Add((Field: field, Attribute: uniformInput));
          else
            uniformBufferFieldsWithBindings.Add((Field: field, Attribute: uniformInput));
        });
      }

      var usedBindings = new HashSet<UniformBindings>();
      // Create all uniform buffers that the user specified the ids of
      foreach (var uniformBufferField in uniformBufferFieldsWithBindings)
      {
        CreateUniformBuffer(translator, interfaceInfo, uniformBufferField.Field, uniformBufferField.Attribute, usedBindings);
      }
      // Now create all the ids remaining, packed down and skipping any filled slots
      foreach (var uniformBufferField in uniformBufferFieldsWithoutBindings)
      {
        var attribute = uniformBufferField.Attribute;
        attribute.BindingId = FindNextAvailableBindingId(usedBindings, attribute.DescriptorSet);
        CreateUniformBuffer(translator, interfaceInfo, uniformBufferField.Field, attribute, usedBindings);
      }

      // If there's any [UniformInput] fields then group them all together in one buffer
      if (uniformInputFields.Count != 0)
        CreateUniformInputBuffer(translator, interfaceInfo, shaderType.mMeta.TypeName, uniformInputFields, usedBindings);
    }

    public static UniformBufferInterface CreateUniformBuffer(FrontEndTranslator translator, EntryPointInterfaceInfo interfaceInfo, ShaderField field, Shader.UniformBuffer attribute, HashSet<UniformBindings> usedBindings)
    {
      var bindings = new UniformBindings { BindingId = attribute.BindingId, DescriptorSet = attribute.DescriptorSet };
      if (usedBindings.Contains(bindings))
        throw new Exception("Duplicate Buffer");
      usedBindings.Add(bindings);

      var uniformBuffer = interfaceInfo.UniformBuffers.GetOrCreate(bindings);
      uniformBuffer.TypeName = field.mType.mMeta.TypeName.Clone();
      uniformBuffer.InstanceName = string.Format("{0}_Instance", field.mType.mMeta.mName);
      uniformBuffer.GetOwnerDelegate = (FrontEndTranslator translator, UniformBufferInterface bufferInterface, ShaderOp ownerOp, FrontEndContext context) =>
      {
        return translator.GenerateAccessChain(ownerOp, field.mMeta.mName, context);
      };
      foreach (var bufferField in field.mType.mFields)
      {
        var interfaceField = new ShaderInterfaceField() { ShaderField = bufferField };
        uniformBuffer.Fields.Add(interfaceField);
      }
      return uniformBuffer;
    }

    public static UniformBufferInterface CreateUniformInputBuffer(FrontEndTranslator translator, EntryPointInterfaceInfo interfaceInfo, TypeName typeName, List<(ShaderField Field, ShaderAttribute Attribute)> uniformInputFields, HashSet<UniformBindings> usedBindings)
    {
      var materialBinding = new UniformBindings { DescriptorSet = 0 };
      materialBinding.BindingId = FindNextAvailableBindingId(usedBindings, materialBinding.DescriptorSet);
      usedBindings.Add(materialBinding);

      var uniformInputBuffer = interfaceInfo.UniformBuffers.GetOrCreate(materialBinding);
      foreach (var uniformInputField in uniformInputFields)
      {
        var interfaceField = new ShaderInterfaceField() { ShaderField = uniformInputField.Field };
        uniformInputBuffer.TypeName = typeName.CloneAsAppended("_SharedMaterialBuffer");
        uniformInputBuffer.InstanceName = string.Format("{0}_SharedMaterialBuffer_Instance", typeName.Name);
        uniformInputBuffer.Fields.Add(interfaceField);
      }
      return uniformInputBuffer;
    }

    public static void GenerateConstantUniforms(FrontEndTranslator translator, ShaderType shaderType, EntryPointInterfaceInfo interfaceInfo)
    {
      foreach (var field in shaderType.mStaticFields)
      {
        var attributes = field.mMeta.mAttributes;
        ShaderAttributes.AttributeCallback uniformCallback = (ShaderAttribute attribute) =>
        {
          var staticGlobalInstance = translator.mCurrentLibrary.mStaticGlobals.GetValueOrDefault(field);
          if (staticGlobalInstance == null)
            return;

          var uniformAttribute = UniformConstantAttribute.ParseAttribute(attribute);
          var uniformConstant = interfaceInfo.ConstantUniforms.GetOrCreate(uniformAttribute.DescriptorSet, uniformAttribute.BindingId);
          uniformConstant.InterfaceInstance = staticGlobalInstance.InstanceOp;
        };
        attributes.ForeachAttribute(typeof(Shader.UniformConstant), uniformCallback);
      }
    }

    static int FindNextAvailableBindingId(HashSet<UniformBindings> usedIndices, int descriptorSet)
    {
      var bindings = new UniformBindings() { BindingId = 0, DescriptorSet = descriptorSet };
      while (usedIndices.Contains(bindings))
        ++bindings.BindingId;
      return bindings.BindingId;
    }

    public static void CopyUniformBufferFields(FrontEndTranslator translator, ShaderOp thisOp, UniformBufferInterface uniformBuffer, InterfaceFieldCopyMode copyMode, FrontEndContext context)
    {
      var ownerOp = uniformBuffer.GetOwnerInstance(translator, thisOp, context);
      EntryPointGenerationShared.CopyInterfaceFields(translator, ownerOp, uniformBuffer.Fields, copyMode, context);
    }

    public static void DeclareUniformBuffer(FrontEndTranslator translator, UniformBufferInterface uniformBuffer, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      // Generate a unique buffer type instance. Buffer types are decorated in a way where they
      // can't used as normal structures. In addition, this helps with any duplicate buffer types.
      var bufferName = uniformBuffer.TypeName.Clone();
      for(var i = 0; ; ++i)
      {
        var interfaceStruct = translator.FindType(new TypeKey(bufferName.FullName));
        if (interfaceStruct == null)
          break;
        bufferName.Name = $"{uniformBuffer.TypeName.Name}_{i}";
      }

      uniformBuffer.InterfaceInstance = EntryPointGenerationShared.GenerateInterfaceStructAndOp(translator, uniformBuffer.Fields.Fields, bufferName, uniformBuffer.InstanceName, StorageClass.Uniform);

      var instanceType = uniformBuffer.InterfaceInstance.mResultType.GetDereferenceType();
      Decorations.AddDecorationDescriptorSet(translator, instanceType, uniformBuffer.DescriptorSet, decorationsBlock);
      Decorations.AddDecorationBinding(translator, instanceType, uniformBuffer.BindingId, decorationsBlock);
      Decorations.AddDecorationBlock(translator, instanceType, decorationsBlock);
      Decorations.DecorateOffsets(translator, instanceType, decorationsBlock);
      Decorations.DecorateStrides(translator, instanceType, decorationsBlock);
      Decorations.DecorateMatrixMajor(translator, instanceType, decorationsBlock);


      declarationBlock.mLocalVariables.Add(uniformBuffer.InterfaceInstance);
    }

    public static void DeclareUniformBuffers(FrontEndTranslator translator, UniformBufferSet uniformConstantSet, ShaderBlock declarationBlock, ShaderBlock decorationsBlock)
    {
      foreach (var uniformBuffer in uniformConstantSet.Buffers.Values)
      {
        UniformDeclarations.DeclareUniformBuffer(translator, uniformBuffer, declarationBlock, decorationsBlock);
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
        UniformDeclarations.DeclareUniformConstant(translator, uniformConstant, decorationsBlock);
      }
    }
  }

  class UniformBufferAttribute
  {
    public static Shader.UniformBuffer Parse(ShaderAttribute attribute)
    {
      var uniformAttribute = new Shader.UniformBuffer() { BindingId = -1, DescriptorSet = 0 };
      foreach (var namedArg in attribute.Attribute.NamedArguments)
      {
        if (namedArg.Key == "DescriptorSet")
          uniformAttribute.DescriptorSet = (int)namedArg.Value.Value;
        else if (namedArg.Key == "BindingId")
          uniformAttribute.BindingId = (int)namedArg.Value.Value;
      }

      return uniformAttribute;
    }
  }

  public class UniformBufferFieldAttribute
  {
    public static Shader.UniformBufferField ParseAttribute(ShaderAttribute attribute)
    {
      var uniformAttribute = new Shader.UniformBufferField();
      return uniformAttribute;
    }
  }

  public class UniformConstantAttribute
  {
    public static Shader.UniformConstant ParseAttribute(ShaderAttribute attribute)
    {
      var uniformAttribute = new Shader.UniformConstant() { BindingId = 0, DescriptorSet = 0 };
      foreach (var namedArg in attribute.Attribute.NamedArguments)
      {
        if (namedArg.Key == "DescriptorSet")
          uniformAttribute.DescriptorSet = (int)namedArg.Value.Value;
        else if (namedArg.Key == "BindingId")
          uniformAttribute.BindingId = (int)namedArg.Value.Value;
      }

      return uniformAttribute;
    }
  }
}
