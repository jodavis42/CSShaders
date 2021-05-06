using System.Collections.Generic;
using Utilities;

namespace CSShaders.Compositing
{
  using LinkContext = CompositorUtilities.LinkContext;
  using Utils = CompositorUtilities;
  
  /// Generates the definition of a composited shader. This is all of the information needed to later emit the composited shader code.
  public class CompositeDefinitionGenerator
  {
    CompositorSettings Settings;
    ShaderLibrary ShaderLibrary;
    CompositeLibrary ResultLibrary;

    static TypeName FragmentOutputAttributeName = TypeAliases.GetTypeName<Shader.FragmentOutput>();
    static TypeName StageOutputAttributeName = TypeAliases.GetTypeName<Shader.StageOutput>();
    static TypeName HardwareBuiltInOutputAttributeName = TypeAliases.GetTypeName<Shader.HardwareBuiltInOutput>();
    static TypeName FragmentInputAttributeName = TypeAliases.GetTypeName<Shader.FragmentInput>();
    static TypeName StageInputAttributeName = TypeAliases.GetTypeName<Shader.StageInput>();
    static TypeName HardwareBuiltInInputAttributeName = TypeAliases.GetTypeName<Shader.HardwareBuiltInInput>();
    static TypeName AppBuiltInInputAttributeName = TypeAliases.GetTypeName<Shader.AppBuiltInInput>();
    static TypeName PropertyInputAttributeName = TypeAliases.GetTypeName<Shader.PropertyInput>();

    /// Generate a composite for the standard render pipeline (Vertex/Pixel).
    /// The individual result of this composite is returned, but the information
    /// is all appended into the given composite library as shared types might be created.
    public GeneratedCompositeDefinition CompositeRender(CompositorSettings settings, CompositeDefinition compositeDef, ShaderLibrary shaderLibrary, CompositeLibrary resultLibary)
    {
      Settings = settings;
      ShaderLibrary = shaderLibrary;
      ResultLibrary = resultLibary;

      GeneratedCompositeDefinition result = new GeneratedCompositeDefinition();
      result.Name = compositeDef.Name;
      // Create the internal data we need to track all the links between fields
      var compositeLinkage = new CompositeLinkage(result);
      
      // First organize the definition data into per stage information
      CollectFragmentsByStage(compositeDef, compositeLinkage);
      CreateCpuStage(compositeLinkage);
      CreateGpuStage(compositeLinkage);
      ResolveComposite(compositeDef, compositeLinkage);
      ResolveFieldOrder(compositeLinkage);
      return result;
    }

    void CreateCpuStage(CompositeLinkage compositeLinkage)
    {
      var cpuStage = compositeLinkage.FindStage(StageType.Cpu);
      var cpuStageVertex = cpuStage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      foreach (var vertexAttribute in Settings.AttributeSettings.VertexAttributes)
      {
        var fieldType = Utils.GetOrCreateTypeDefinition(ResultLibrary, vertexAttribute.Type);
        var fieldDef = cpuStageVertex.AddField(fieldType, vertexAttribute.Name);
        var fieldInstance = cpuStageVertex.SelfInstance.GetFieldInstance(fieldDef.Name);
        Utils.AddAttributeAndParameterUnique(fieldInstance.Field.Attributes, typeof(Shader.StageOutput), nameof(Shader.StageOutput.Location), vertexAttribute.Location);

        cpuStageVertex.PotentialStageOutputs.Add(new FieldKey(fieldType.FullName, fieldDef.Name), fieldInstance);
      }
    }

    void CreateGpuStage(CompositeLinkage compositeLinkage)
    {
      var gpuStage = compositeLinkage.FindStage(StageType.Gpu);
      var gpuStageVertex = gpuStage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      foreach (var renderTargets in Settings.RenderTargetSettings.RenderTargets)
      {
        var fieldType = Utils.GetOrCreateTypeDefinition(ResultLibrary, renderTargets.Type);
        var fieldDef = gpuStageVertex.AddField(fieldType, renderTargets.Name);
        var fieldInstance = gpuStageVertex.SelfInstance.GetFieldInstance(fieldDef.Name);
        Utils.AddAttributeAndParameterUnique(fieldInstance.Field.Attributes, typeof(Shader.StageInput), nameof(Shader.StageInput.Location), renderTargets.Location);

        gpuStageVertex.PotentialStageOutputs.Add(new FieldKey(fieldType.FullName, fieldDef.Name), fieldInstance);
      }
    }

    void CollectFragmentsByStage(CompositeDefinition compositeDef, CompositeLinkage compositeLinkage)
    {
      string DeDuplicateFragmentName(Dictionary<string, int> fragmentInstanceCount, TypeDefinition fragmentType)
      {
        string instanceName = fragmentType.ShortName.ToLower();
        int count = fragmentInstanceCount.GetValueOrDefault(instanceName, 0);
        if (count != 0)
        {
          ++count;
          instanceName = $"instanceName{count}";
        }
        fragmentInstanceCount[instanceName] = count;
        return instanceName;
      };

      for (StageType stageType = StageType.Vertex; stageType <= StageType.Pixel; ++stageType)
      {
        var stageLinkage = compositeLinkage.FindStage(stageType);
        var stageDef = stageLinkage.StageDefinition;

        // Create the type for the composited shader
        stageDef.TypeName = new TypeName { Namespace = Settings.ResultNamespace, Name = $"{compositeDef.Name}_{stageType.ToString()}" };
        ResultLibrary.Types.Add(stageDef.TypeName, stageDef);

        var fragmentInstanceCount = new Dictionary<string, int>();

        // Create and fillout the vertex attachment (this is the standard attachment type)
        var vertexAttachment = stageDef.FindOrCreateAttachment(StageAttachmentType.Vertex);
        foreach (var fragment in compositeDef.Fragments)
        {
          if (fragment.mFragmentType == stageType.GetFragmentType())
          {
            var fragmentInstance = new VariableInstance();
            // Create the type and local variable instance for the fragment and add it to the stage
            fragmentInstance.Type = Utils.GetOrCreateTypeDefinition(ResultLibrary, fragment);
            fragmentInstance.Name = DeDuplicateFragmentName(fragmentInstanceCount, fragmentInstance.Type);
            stageDef.Fragments.Add(fragmentInstance);
            vertexAttachment.Variables.Add(fragmentInstance);
          }
        }
      }
    }

    void ResolveComposite(CompositeDefinition compositeDef, CompositeLinkage compositeLinkage)
    {
      // Find every potential output of each stage. This allows propogating outputs across multiple stages if needed
      for (var stageType = StageType.Cpu; stageType < StageType.Gpu; ++stageType)
      {
        var prevStage = compositeLinkage.FindStage(stageType);
        var currStage = compositeLinkage.FindStage(stageType + 1);
        CollectPotentialStageOutputs(prevStage, currStage);
      }

      // Now actually link all stages together
      for (var stageType = StageType.Cpu; stageType < StageType.Gpu; ++stageType)
      {
        var prevStage = compositeLinkage.FindStage(stageType);
        var currStage = compositeLinkage.FindStage(stageType + 1);
        LinkStage(prevStage, currStage);
      }

      ResolveGpuStage(compositeDef, compositeLinkage);
    }

    void CollectPotentialStageOutputs(StageLinkage prevStageLinkage, StageLinkage currStageLinkage)
    {
      // Visit the vertex stage attachment only for now (@JoshD)
      var currVertexAttachment = currStageLinkage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      CollectPotentialStageOutputs(currStageLinkage, currVertexAttachment.Variables, currVertexAttachment);

      // All potential stage outputs from previous stages are available to later stages (implicit pass throughs)
      foreach (var prevAttachment in prevStageLinkage.GetAttachments())
      {
        var currAttachment = currStageLinkage.FindOrCreateAttachment(prevAttachment.AttachmentType);
        foreach (var potentialOutputPair in prevAttachment.PotentialStageOutputs)
        {
          if (!currAttachment.PotentialStageOutputs.ContainsKey(potentialOutputPair.Key))
          {
            var prevStage = prevStageLinkage.StageDefinition;
            var potentialOutput = potentialOutputPair.Value.Clone(prevStage.Self);
            currAttachment.PotentialStageOutputs.Add(potentialOutputPair.Key, potentialOutput);
          }
        }
      }
    }

    void CollectPotentialStageOutputs(StageLinkage stageLinkage, IEnumerable<VariableInstance> types, StageAttachmentLinkage attachmentLinkage)
    {
      foreach (var typeDef in types)
      {
        foreach (var fieldDef in typeDef.Type.Fields)
        {
          foreach (var attribute in fieldDef.Attributes)
          {
            if (attribute.Name == StageOutputAttributeName)
            {
              var outputName = Utils.GetInputOutputFieldName(fieldDef, attribute);

              // Create a potential new field but owned by the stage
              var fieldInstance = typeDef.GetFieldInstance(fieldDef.Name);
              var potentialOutput = fieldInstance.Clone(stageLinkage.StageDefinition.Self);
              potentialOutput.Name = outputName;

              // Add this as a potential output of the stage
              var fieldKey = new FieldKey(fieldDef.Type.TypeName, outputName);
              attachmentLinkage.PotentialStageOutputs[fieldKey] = potentialOutput;
              attachmentLinkage.StageOutputSource[fieldKey] = fieldInstance;
            }
          }
        }
      }
    }

    void LinkStage(StageLinkage prevStageLinkage, StageLinkage currStageLinkage)
    {
      var prevStageVertexAttachment = prevStageLinkage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      var currStageVertexAttachment = currStageLinkage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      LinkStage(prevStageVertexAttachment, currStageVertexAttachment);
    }

    void LinkStage(StageAttachmentLinkage prevAttachmentLinkage, StageAttachmentLinkage currAttachmentLinkage)
    {
      var availableFragmentOutputs = new Dictionary<FieldKey, FieldInstance>();
      var availableStageOutputs = new Dictionary<FieldKey, FieldInstance>();
      foreach (var pair in prevAttachmentLinkage.PotentialStageOutputs)
      {
        availableStageOutputs.Add(pair.Key, pair.Value);
      }

      var fragmentType = currAttachmentLinkage.FragmentType;
      var linkContext = new LinkContext()
      {
        Settings = Settings,
        ResultLibrary = ResultLibrary,
        ShaderLibrary = ShaderLibrary,
        FragmentType = fragmentType,
        CurrAttachmentLinkage = currAttachmentLinkage,
        PrevAttachmentLinkage = prevAttachmentLinkage,
        AvailableFragmentOutputs = availableFragmentOutputs,
        AvailableStageOutputs = availableStageOutputs,
      };

      foreach (var fragmentInstance in currAttachmentLinkage.Variables)
      {
        linkContext.FragmentInstance = fragmentInstance;
        linkContext.FragmentReflection = new FragmentReflection { ShaderType = fragmentInstance.Type.ShaderType };
        currAttachmentLinkage.Reflection.Fragments.Add(linkContext.FragmentReflection);

        foreach (var fieldDef in fragmentInstance.Type.Fields)
        {
          linkContext.FieldDefinition = fieldDef;
          VisitInputAttributes(linkContext);
          VisitOutputAttributes(linkContext);
        }
      }
    }

    void ResolveGpuStage(CompositeDefinition compositeDef, CompositeLinkage compositeLinkage)
    {
      // Gpu stage linkage is special because normally we check all fragments and see what available inputs match to potential outputs.
      // Instead we know what inputs exist and have to match potential outputs against it.
      var prevStageLinkage = compositeLinkage.FindStage(StageType.Pixel);
      var currStageLinkage = compositeLinkage.FindStage(StageType.Gpu);
      var prevStageVertexAttachment = prevStageLinkage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      var currStageVertexAttachment = currStageLinkage.FindOrCreateAttachment(StageAttachmentType.Vertex);
      foreach (var renderTarget in Settings.RenderTargetSettings.RenderTargets)
      {
        var fieldType = Utils.GetOrCreateTypeDefinition(ResultLibrary, renderTarget.Type);
        var fieldName = renderTarget.Name;
        var fieldKey = new FieldKey(fieldType.TypeName, fieldName);

        var potentialOutput = prevStageVertexAttachment.PotentialStageOutputs.GetValueOrDefault(fieldKey);
        if (potentialOutput == null)
          continue;

        currStageVertexAttachment.AddField(fieldType, fieldName);
        var destinstance = currStageVertexAttachment.SelfInstance.GetFieldInstance(fieldName);
        Utils.AddStageLink(prevStageVertexAttachment, currStageVertexAttachment, fieldName, potentialOutput, destinstance);
      }
    }

    void VisitInputAttributes(LinkContext linkContext)
    {
      foreach (var attribute in linkContext.FieldDefinition.Attributes)
      {
        linkContext.Attribute = attribute;

        if (attribute.Name == FragmentInputAttributeName)
        {
          if (Utils.HandleFragmentInput(linkContext))
            break;
        }
        else if (attribute.Name == StageInputAttributeName)
        {
          if (Utils.HandleStageInput(linkContext))
            break;
        }
        else if (attribute.Name == HardwareBuiltInInputAttributeName)
        {
          if (Utils.HandleHardwareBuiltInInput(linkContext))
            break;
        }
        else if (attribute.Name == AppBuiltInInputAttributeName)
        {
          if (Utils.HandleAppBuiltInInput(linkContext))
            break;
        }
        else if (attribute.Name == PropertyInputAttributeName)
        {
          if (Utils.HandlePropertyInput(linkContext))
            break;
        }
      }
    }

    void VisitOutputAttributes(LinkContext linkContext)
    {
      foreach (var attribute in linkContext.FieldDefinition.Attributes)
      {
        linkContext.Attribute = attribute;
        if (attribute.Name == FragmentOutputAttributeName)
        {
          Utils.HandleFragmentOutput(linkContext);
        }
        else if (attribute.Name == HardwareBuiltInOutputAttributeName)
        {
          Utils.HandleHardwareBuiltInOutput(linkContext);
        }
      }
    }

    void ResolveFieldOrder(CompositeLinkage compositeLinkage)
    {
      // Find every potential output of each stage. This allows propogating outputs across multiple stages if needed
      for (var stageType = StageType.Cpu; stageType < StageType.Gpu; ++stageType)
      {
        var prevStage = compositeLinkage.FindStage(stageType);
        var currStage = compositeLinkage.FindStage(stageType + 1);

        foreach (var prevAttachentLinkage in prevStage.GetAttachments())
        {
          var currAttachentLinkage = currStage.FindAttachment(prevAttachentLinkage.AttachmentType);
          ResolveFieldOrder(prevAttachentLinkage, currAttachentLinkage);
        }
      }
    }

    void ResolveFieldOrder(StageAttachmentLinkage prevAttachmentLinkage, StageAttachmentLinkage currAttachmentLinkage)
    {
      if (prevAttachmentLinkage == null || currAttachmentLinkage == null)
        return;

      var prevParsedStageOutputs = new Dictionary<FieldKey, (ShaderAttribute Attribute, Shader.StageOutput StageOutput)>();
      ComputeStageOutputLocations(prevAttachmentLinkage, ref prevParsedStageOutputs);

      // Make sure that the inputs and outputs match in order between the two stages.
      // This is done by setting the location parameters on the input/output attributes.
      foreach (var field in currAttachmentLinkage.OwningStageDefinition.Fields)
      {
        var fieldKey = new FieldKey(field.Type.TypeName, field.Name);
        if (prevParsedStageOutputs.TryGetValue(fieldKey, out var prevFieldData))
        {
          var parsedOutput = prevFieldData.StageOutput;
          var stageInput = field.Attributes.FindFirstAttribute(typeof(Shader.StageInput));
          if (parsedOutput.Location != -1)
            stageInput.Parameters.Add(new ShaderAttributeParameter { Name = nameof(Shader.StageOutput.Location), Value = parsedOutput.Location });
          if (parsedOutput.Component != -1)
            stageInput.Parameters.Add(new ShaderAttributeParameter { Name = nameof(Shader.StageOutput.Component), Value = parsedOutput.Component });
        }
      }
    }

    // Find all locations for all stage outputs of a stage. If any output doesn't have a pre-defined location, it's computed here.
    void ComputeStageOutputLocations(StageAttachmentLinkage attachmentLinkage, ref Dictionary<FieldKey, (ShaderAttribute Attribute, Shader.StageOutput StageOutput)> parsedStageOutputs)
    {
      parsedStageOutputs = new Dictionary<FieldKey, (ShaderAttribute Attribute, Shader.StageOutput StageOutput)>();

      var usedLocations = new HashSet<int>();
      var fieldsToAddLocations = new List<FieldKey>();
      foreach (var pair in attachmentLinkage.ResolvedOutputs)
      {
        var fieldKey = pair.key;
        var field = pair.value;
        // Only process stage output
        var stageOutputAttribute = field.Field.Attributes.FindFirstAttribute(typeof(Shader.StageOutput));
        if (stageOutputAttribute == null)
          continue;

        var parsedStageOutput = AttributeExtensions.ParseStageOutput(stageOutputAttribute);
        // If the location is already specified just use that,
        // otherwise add this to what we need to check later
        if (parsedStageOutput.Location != -1)
          usedLocations.Add(parsedStageOutput.Location);
        else
          fieldsToAddLocations.Add(fieldKey);
        parsedStageOutputs.Add(fieldKey, (stageOutputAttribute, parsedStageOutput));
      }
      // Now we can compute the locations of everything that wasn't specified
      int currentLocation = 0;
      foreach (var fieldKey in fieldsToAddLocations)
      {
        // Find the next unused location
        while (usedLocations.Contains(currentLocation))
          ++currentLocation;
        usedLocations.Add(currentLocation);

        var item = parsedStageOutputs[fieldKey];
        var attribute = item.Attribute;
        var parsedAttribute = item.StageOutput;
        // Set the location and add it to the attribute. None of these have the location specified already so adding the param is always safe.
        parsedAttribute.Location = currentLocation;
        attribute.Parameters.Add(new ShaderAttributeParameter { Name = nameof(Shader.StageOutput.Location), Value = currentLocation });
      }
    }
  }
}
