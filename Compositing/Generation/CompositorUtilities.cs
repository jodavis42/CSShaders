using System;
using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Various helpful utilities for compositing
  internal class CompositorUtilities
  {
    /// A bunch of common data needed to link two fields during compositing
    internal class LinkContext
    {
      internal CompositorSettings Settings;
      internal ShaderLibrary ShaderLibrary;
      internal CompositeLibrary ResultLibrary;
      internal FragmentType FragmentType;
      internal StageAttachmentLinkage PrevAttachmentLinkage;
      internal StageAttachmentLinkage CurrAttachmentLinkage;
      internal VariableInstance FragmentInstance;
      internal FragmentReflection FragmentReflection;
      internal FieldDefinition FieldDefinition;
      internal ShaderAttribute Attribute;
      internal Dictionary<FieldKey, FieldInstance> AvailableFragmentOutputs = new Dictionary<FieldKey, FieldInstance>();
      internal Dictionary<FieldKey, FieldInstance> AvailableStageOutputs = new Dictionary<FieldKey, FieldInstance>();
    }

    /// Create a type definition for a given shader type and put it in the composite library.
    /// The types are loaded into a generic format not relying on shader type so that existing and newly generated types can be treated equally.
    internal static TypeDefinition GetOrCreateTypeDefinition(CompositeLibrary compositeLibrary, ShaderType shaderType)
    {
      var typeName = shaderType.mMeta.TypeName;
      var typeDef = compositeLibrary.Types.GetValueOrDefault(typeName);
      // If the type doesn't exist then create it
      if (typeDef == null)
      {
        typeDef = new TypeDefinition();
        typeDef.Generated = false;
        typeDef.TypeName = typeName;
        typeDef.ShaderType = shaderType;
        // Clone all of the fields on this type
        foreach (var shaderField in shaderType.mFields)
        {
          var fieldType = GetOrCreateTypeDefinition(compositeLibrary, shaderField.mType);
          var fieldDef = typeDef.AddField(shaderField, fieldType, shaderField.mMeta.mName);
          fieldDef.Attributes = shaderField.mMeta.mAttributes.Clone();
        }
      }
      return typeDef;
    }

    /// Adds the link between two fragment fields
    static internal void AddFragmentLink(StageAttachmentLinkage currStageAttachmentLinkage, VariableInstance sourceField, VariableInstance destField)
    {
      /// Find the links for the destination field (each link is recorded on the destination)
      var sourceLinks = currStageAttachmentLinkage.FindOrCreateLinks(destField.Owner);
      sourceLinks.Add(new VariableLink { Source = sourceField, Destination = destField });
    }

    /// Adds the link between two fields where the source is from the previous stage
    static internal void AddStageLink(StageAttachmentLinkage prevStageAttachmentLinkage, StageAttachmentLinkage currStageAttachmentLinkage, string inputName, VariableInstance sourceField, VariableInstance destField)
    {
      // First always add the link to the destination
      AddFragmentLink(currStageAttachmentLinkage, sourceField, destField);
      // This shouldn't happen, but if the source isn't the previous stage then don't do anything special
      if (sourceField.Owner.Type != prevStageAttachmentLinkage.TypeDefinition)
        return;

      var fieldKey = new FieldKey(sourceField.Type.TypeName, inputName);
      // Create the resolved input (the field declaraton) on the stage if it doesn't exist
      if (!currStageAttachmentLinkage.ResolvedInputs.ContainsKey(fieldKey))
      {
        FieldInstance stageField = FindOrCreateStageField(currStageAttachmentLinkage, sourceField.Type, inputName);
        if (stageField.Field.Attributes.FindFirstAttribute(typeof(Shader.StageInput)) == null)
          stageField.Field.Attributes.Add(typeof(Shader.StageInput));
        currStageAttachmentLinkage.ResolvedInputs.Add(fieldKey, stageField);
      }
      // Do the same, but for the output on the previous stage.
      // @JoshD: This needs to be properly split up to handle primitive vs vertex
      if (!prevStageAttachmentLinkage.ResolvedOutputs.ContainsKey(fieldKey))
      {
        FieldInstance stageField = FindOrCreateStageField(prevStageAttachmentLinkage, sourceField.Type, inputName);
        if(stageField.Field.Attributes.FindFirstAttribute(typeof(Shader.StageOutput)) == null)
          stageField.Field.Attributes.Add(typeof(Shader.StageOutput));
        prevStageAttachmentLinkage.ResolvedOutputs[fieldKey] = stageField;
        // Additionally make sure to tell the previous stage that it needs to write out the value to the stage
        // from whatever the final source was for the field (e.g. the last fragment who wrote to the value)
        var fieldSource = prevStageAttachmentLinkage.StageOutputSource.GetValueOrDefault(fieldKey);
        prevStageAttachmentLinkage.StageLinks.Add(new VariableLink { Source = fieldSource, Destination = stageField });
      }
    }

    /// Gets the input or output name to use for resolution. The name can be overridden by the attribute.
    static internal string GetInputOutputFieldName(FieldDefinition fieldDef, ShaderAttribute attribute)
    {
      var resolvedName = fieldDef.Name;
      // @JoshD: Type safety?
      if (attribute.Parameters.Count != 0)
        resolvedName = attribute.Parameters[0].Value.ToString();
      return resolvedName;
    }

    static internal string GeneratePropertyName(TypeName fragmentType, string instanceName, string inputName)
    {
      return  $"{fragmentType.FullName}_{instanceName}_{inputName}";
    }

    /// Adds the given attribute and parameter to the attributes list if it doesn't already exist
    static internal void AddAttributeAndParameterUnique<T>(ShaderAttributes attributes, Type attributeType, T paramValue)
    {
      bool foundAttribute = false;
      // See if the attribute already exists
      attributes.ForeachAttribute(attributeType, (ShaderAttribute attribute) =>
      {
        if (attribute.Parameters.Count != 0 && Object.Equals(attribute.Parameters[0].Value, paramValue))
          foundAttribute = true;
      });
      // If it didn't exist then add it
      if (!foundAttribute)
      {
        var stageOutputAttribute = attributes.Add(attributeType);
        stageOutputAttribute.Parameters.Add(new ShaderAttributeParameter { Value = paramValue });
      }
    }

    /// Adds the given attribute and parameter to the attributes list if it doesn't already exist
    static internal void AddAttributeAndParameterUnique<T>(ShaderAttributes attributes, Type attributeType, string paramName, T paramValue)
    {
      bool foundAttribute = false;
      // See if the attribute already exists
      attributes.ForeachAttribute(attributeType, (ShaderAttribute attribute) =>
      {
        if (attribute.Parameters.Count != 0 && Object.Equals(attribute.Parameters[0].Value, paramValue))
          foundAttribute = true;
      });
      // If it didn't exist then add it
      if (!foundAttribute)
      {
        var stageOutputAttribute = attributes.Add(attributeType);
        stageOutputAttribute.Parameters.Add(new ShaderAttributeParameter { Name = paramName, Value = paramValue });
      }
    }

    /// Creates the reflection for a field
    static internal void CreateFieldReflection(FragmentReflection fragmentReflection, FieldDefinition fieldDef, FieldResolutionType resolutionType, FieldDefinition sourceFieldDef = null)
    {
      if (fieldDef.ShaderField == null)
        return;
      var fieldReflection = new FieldReflection(resolutionType, fieldDef, sourceFieldDef);
      fragmentReflection.Fields.Add(fieldReflection);
    }

    static internal bool HandleFragmentInput(LinkContext context)
    {
      var inputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      var fieldKey = new FieldKey(context.FieldDefinition.Type.TypeName, inputName);
      var sourceInstance = context.AvailableFragmentOutputs.GetValueOrDefault(fieldKey, null);
      // If a previous fragment has output this field then link it and the source together
      if (sourceInstance != null)
      {
        FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
        AddFragmentLink(context.CurrAttachmentLinkage, sourceInstance, destInstance);
        CreateFieldReflection(context.FragmentReflection, context.FieldDefinition, FieldResolutionType.Fragment, sourceInstance.Field);
        return true;
      }
      return false;
    }

    static internal bool HandleStageInput(LinkContext context)
    {
      var inputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      var fieldKey = new FieldKey(context.FieldDefinition.Type.TypeName, inputName);
      var sourceInstance = context.AvailableStageOutputs.GetValueOrDefault(fieldKey, null);
      // If a previous stage can output this field then link it and the source together
      if (sourceInstance != null)
      {
        FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
        AddStageLink(context.PrevAttachmentLinkage, context.CurrAttachmentLinkage, inputName, sourceInstance, destInstance);
        CreateFieldReflection(context.FragmentReflection, context.FieldDefinition, FieldResolutionType.Stage);
        return true;
      }
      return false;
    }

    static internal bool HandleHardwareBuiltInInput(LinkContext context)
    {
      var inputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      // See if there's a hardware bulitin available by the given type/name
      var fieldType = context.ShaderLibrary.FindType(new TypeKey(context.FieldDefinition.Type.TypeName));
      var builtInField = FindHardwareBuiltIn(context.Settings, context.FragmentType, fieldType, inputName, true);
      if (builtInField != null)
      {
        // Create a field on the 
        FieldInstance stageField = FindOrCreateStageField(context.CurrAttachmentLinkage, context.FieldDefinition.Type, inputName);
        // Add the hardware built-in input attribute with the specified built-in type
        AddAttributeAndParameterUnique(stageField.Field.Attributes, typeof(Shader.HardwareBuiltInInput), builtInField.BuiltInType);

        FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
        AddFragmentLink(context.CurrAttachmentLinkage, stageField, destInstance);
        CreateFieldReflection(context.FragmentReflection, context.FieldDefinition, FieldResolutionType.HardwareBuiltIn);
        return true;
      }
      return false;
    }

    static internal bool HandleAppBuiltInInput(LinkContext context)
    {
      var inputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
      var fieldType = context.ShaderLibrary.FindType(new TypeKey(context.FieldDefinition.Type.TypeName));
      // Find if there is a uniform buffer field with the given type/name.
      // This will create the buffer type if needed, create the field if needed, and hook up the link to copy the values over
      bool result = FindOrCreateUniformBuffer(context.Settings, context.ResultLibrary, context.CurrAttachmentLinkage, fieldType, inputName, destInstance);
      // Also create relflection if needed
      if(result)
        CreateFieldReflection(context.FragmentReflection, context.FieldDefinition, FieldResolutionType.AppBuiltIn);
      return result;
    }

    static internal bool HandlePropertyInput(LinkContext context)
    {
      // PropertyInput is not conditional, it can always succceed.
      var inputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
      var fieldType = context.ShaderLibrary.FindType(new TypeKey(context.FieldDefinition.Type.TypeName));
      // Make sure the property name is unique
      string propFieldName = GeneratePropertyName(context.FragmentInstance.Type.TypeName, context.FragmentInstance.Name, inputName);
      CreateUniformField(context.ResultLibrary, context.CurrAttachmentLinkage, fieldType, propFieldName, destInstance);
      CreateFieldReflection(context.FragmentReflection, context.FieldDefinition, FieldResolutionType.Property);
      return true;
    }

    static internal bool HandleFragmentOutput(LinkContext context)
    {
      var outputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      var fieldKey = new FieldKey(context.FieldDefinition.Type.TypeName, outputName);
      // Add that this field is now available as a fragment output for any input to use
      FieldInstance destInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
      context.AvailableFragmentOutputs[fieldKey] = destInstance;
      return true;
    }

    static internal bool HandleHardwareBuiltInOutput(LinkContext context)
    {
      var outputName = GetInputOutputFieldName(context.FieldDefinition, context.Attribute);
      var fieldType = context.ShaderLibrary.FindType(new TypeKey(context.FieldDefinition.Type.TypeName));
      var builtInField = FindHardwareBuiltIn(context.Settings, context.FragmentType, fieldType, outputName, false);
      if (builtInField != null)
      {
        FieldInstance stageField = FindOrCreateStageField(context.CurrAttachmentLinkage, context.FieldDefinition.Type, outputName);
        AddAttributeAndParameterUnique(stageField.Field.Attributes, typeof(Shader.HardwareBuiltInOutput), builtInField.BuiltInType);

        FieldInstance sourceInstance = context.FragmentInstance.GetFieldInstance(context.FieldDefinition.Name);
        // Link the fragment's output to the stage field. There could be another fragment that wrote to this field already.
        // If so, replace the link so only the last one to write wins
        FindOrReplaceDestLinks(context.CurrAttachmentLinkage.StageLinks, sourceInstance, stageField);
        return true;
      }
      return false;
    }

    static internal void FindOrReplaceDestLinks(List<VariableLink> links, FieldInstance sourceInstance, FieldInstance destInstance)
    {
      links.RemoveAll((VariableLink link) =>
      {
        return link.Destination == destInstance;
      });
      links.Add(new VariableLink { Source = sourceInstance, Destination = destInstance });
    }

    static internal UniformBufferField FindUniformInput(CompositorSettings settings, ShaderType fieldType, string fieldName)
    {
      // @JoshD: Optimize with a cache?
      foreach (var uniformBuffer in settings.UniformSettings.BufferDescriptions)
      {
        foreach (var field in uniformBuffer.Fields)
        {
          if (field.FieldType == fieldType && field.FieldName == fieldName)
            return field;
        }
      }
      return null;
    }

    static internal void CreateUniformField(CompositeLibrary compositeLibrary, StageAttachmentLinkage currAttachmentLinkage, ShaderType fieldShaderType, string fieldName, VariableInstance fieldDefDest)
    {
      var fieldType = GetOrCreateTypeDefinition(compositeLibrary, fieldShaderType);
      var bufferFieldDef = currAttachmentLinkage.AddField(fieldType, fieldName);
      bufferFieldDef.Attributes.Add(typeof(Shader.UniformBufferField));
      var bufferFieldInstance = currAttachmentLinkage.SelfInstance.GetFieldInstance(fieldName);
      AddFragmentLink(currAttachmentLinkage, bufferFieldInstance, fieldDefDest);
    }

    static internal TypeDefinition FindOrCreateUniformBufferType(CompositeLibrary compositeLibrary, UniformBufferDescription uniformBufferDesc)
    {
      var bufferTypeName = new TypeName { Name = uniformBufferDesc.BufferName };
      var bufferTypeDef = compositeLibrary.Types.GetValueOrDefault(bufferTypeName);
      // Type doesn't exist, create it
      if (bufferTypeDef == null)
      {
        bufferTypeDef = new TypeDefinition();
        bufferTypeDef.TypeName = new TypeName { Name = uniformBufferDesc.BufferName };
        // Clone all fields from the uniform buffer description
        foreach (var field in uniformBufferDesc.Fields)
          bufferTypeDef.AddField(GetOrCreateTypeDefinition(compositeLibrary, field.FieldType), field.FieldName);

        compositeLibrary.Types.Add(bufferTypeDef.TypeName, bufferTypeDef);
      }
      return bufferTypeDef;
    }

    static internal bool FindOrCreateUniformBuffer(CompositorSettings settings, CompositeLibrary compositeLibrary, StageAttachmentLinkage currAttachmentLinkage, ShaderType fieldType, string fieldName, VariableInstance destInstance)
    {
      var uniformField = FindUniformInput(settings, fieldType, fieldName);
      if (uniformField == null)
        return false;

      UniformBufferDescription uniformBufferDesc = uniformField.Owner;

      // Create the uniform instance if it doesn't already exist
      var bufferInstanceDef = currAttachmentLinkage.ResolvedUniformBuffers.GetValueOrDefault(uniformBufferDesc);
      if (bufferInstanceDef == null)
      {
        // Create the buffer type, add it to the shader as a field, and mark that field as a uniform buffer
        var bufferTypeDef = FindOrCreateUniformBufferType(compositeLibrary, uniformBufferDesc);
        var bufferFieldDef = currAttachmentLinkage.AddField(bufferTypeDef, uniformBufferDesc.BufferName);
        bufferFieldDef.Attributes.Add(typeof(Shader.UniformBuffer));
        // Now add this field instance for anyone who ever looks up this buffer again
        bufferInstanceDef = currAttachmentLinkage.SelfInstance.GetFieldInstance(uniformBufferDesc.BufferName);
        currAttachmentLinkage.ResolvedUniformBuffers.Add(uniformBufferDesc, bufferInstanceDef);
      }

      // Now get the field off the buffer and add a link between it and the destination
      var bufferFieldInstance = bufferInstanceDef.GetFieldInstance(fieldName);
      AddFragmentLink(currAttachmentLinkage, bufferFieldInstance, destInstance);
      return true;
    }

    static internal HardwareBuiltInField FindHardwareBuiltIn(CompositorSettings settings, FragmentType fragmentType, ShaderType type, string name, bool input)
    {
      // @JoshD: Optimize
      var descriptions = settings.HardwareBuiltInSettings.GetFragmentDesciptions(fragmentType);
      var fields = input ? descriptions.Inputs : descriptions.Outputs;
      foreach (var field in fields)
      {
        if (field.FieldType == type && field.FieldName == name)
          return field;
      }
      return null;
    }

    static internal FieldInstance FindOrCreateStageField(StageAttachmentLinkage stageAttachmentLinkage, VariableInstance variableInstance)
    {
      return FindOrCreateStageField(stageAttachmentLinkage, variableInstance.Type, variableInstance.Name);
    }

    static internal FieldInstance FindOrCreateStageField(StageAttachmentLinkage stageAttachmentLinkage, TypeDefinition fieldType, string fieldName)
    {
      var fieldKey = new FieldKey(fieldType.TypeName, fieldName);
      // Find if there's already a stage field (it could be already be an input and we're checking for an output).
      FieldInstance stageField = stageAttachmentLinkage.ResolvedFields.GetValueOrDefault(fieldKey);
      // If it didn't exist, clone the field and add it to the stage
      if (stageField == null)
      {
        stageAttachmentLinkage.AddField(fieldType, fieldName);
        stageField = stageAttachmentLinkage.SelfInstance.GetFieldInstance(fieldName);
        stageAttachmentLinkage.ResolvedFields.Add(fieldKey, stageField);
      }
      return stageField;
    }
  }
}
