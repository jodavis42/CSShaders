using System.Collections.Generic;
using Utilities;

namespace CSShaders.Compositing
{
  /// Runtime linking information for a stage attachment. This contains all of the
  /// temporary information needed to lookup and resolve fields when compositing fragments.
  public class StageAttachmentLinkage
  {
    /// A field that is an output variable for this stage.
    public OrderedMap<FieldKey, FieldInstance> ResolvedOutputs = new OrderedMap<FieldKey, FieldInstance>();
    /// A field that is an input variable for this stage.
    public OrderedMap<FieldKey, FieldInstance> ResolvedInputs = new OrderedMap<FieldKey, FieldInstance>();
    /// Any uniform buffer that is declared as being used for this stage.
    /// Helps track that a buffer is only declared once even though it's fields can be used multiple times.
    public Dictionary<UniformBufferDescription, FieldInstance> ResolvedUniformBuffers = new Dictionary<UniformBufferDescription, FieldInstance>();
    /// A field to declare as a member on this stage's type. This typically means the field is some
    /// combination of an input, output, uniform. Fields are grouped together since a field can be both an input and an output.
    public OrderedMap<FieldKey, FieldInstance> ResolvedFields = new OrderedMap<FieldKey, FieldInstance>();
    
    /// A field that is tagged as a stage output is marked as a potential output until another stage links an input to it
    public Dictionary<FieldKey, FieldInstance> PotentialStageOutputs = new Dictionary<FieldKey, FieldInstance>();
    /// For every potential stage output, this stores what field from what fragment instance would create that field.
    /// This allows us to declare the actual assignment to the stage output only if needed.
    public Dictionary<FieldKey, FieldInstance> StageOutputSource = new Dictionary<FieldKey, FieldInstance>();

    public StageType StageType => OwningStageDefinition.StageType;
    public FragmentType FragmentType => StageType.GetFragmentType();
    public StageLinkage Owner { get; }
    public StageAttachment StageAttachmentDefinition { get; }
    public StageAttachmentType AttachmentType { get; }
    public StageDefinition OwningStageDefinition => StageAttachmentDefinition.OwningStageDefinition;
    public TypeDefinition TypeDefinition => OwningStageDefinition;
    public VariableInstance SelfInstance => StageAttachmentDefinition.OwningStageDefinition.Self;
    public List<VariableInstance> Variables => StageAttachmentDefinition.Variables;
    public VariableLinkList StageLinks => StageAttachmentDefinition.StageLinks;
    public ShaderReflection Reflection => StageAttachmentDefinition.OwningStageDefinition.Reflection;

    public StageAttachmentLinkage(StageLinkage owner, StageAttachment stageAttachmentDefinition, StageAttachmentType attachmentType)
    {
      Owner = owner;
      StageAttachmentDefinition = stageAttachmentDefinition;
      AttachmentType = attachmentType;
    }

    /// Adds a new field definition to the stage definition
    public FieldDefinition AddField(TypeDefinition type, string name)
    {
      return StageAttachmentDefinition.OwningStageDefinition.AddField(type, name);
    }

    /// Finds the links for the given variable instance
    public VariableLinkList FindOrCreateLinks(VariableInstance varInstance)
    {
      return StageAttachmentDefinition.FindOrCreateLinks(varInstance);
    }
  }

  /// Runtime linking information for a stage.
  public class StageLinkage
  {
    private Dictionary<StageAttachmentType, StageAttachmentLinkage> Attachments = new Dictionary<StageAttachmentType, StageAttachmentLinkage>();

    public CompositeLinkage Owner { get; }
    public StageDefinition StageDefinition { get; }

    public StageLinkage(CompositeLinkage owner, StageDefinition stageDefinition)
    {
      Owner = owner;
      StageDefinition = stageDefinition;
    }

    public StageAttachmentLinkage FindAttachment(StageAttachmentType attachmentType)
    {
      return Attachments.GetValueOrDefault(attachmentType);
    }

    public StageAttachmentLinkage FindOrCreateAttachment(StageAttachmentType attachmentType)
    {
      var attachmentLinkage = FindAttachment(attachmentType);
      if(attachmentLinkage == null)
      {
        var attachmentDef = StageDefinition.FindOrCreateAttachment(attachmentType);
        attachmentLinkage = new StageAttachmentLinkage(this, attachmentDef, attachmentType);
        Attachments.Add(attachmentType, attachmentLinkage);
      }
      return attachmentLinkage;
    }

    public IEnumerable<StageAttachmentLinkage> GetAttachments()
    {
      return Attachments.Values;
    }
  }

  /// Runtime linking information for a composite. This is used to effeciently look
  /// up and handle information when building the composite but tossed later.
  public class CompositeLinkage
  {
    private List<StageLinkage> mStages = new List<StageLinkage>();
    public GeneratedCompositeDefinition CompositeDefinition { get; } = null;

    public CompositeLinkage(GeneratedCompositeDefinition compositeDefinition)
    {
      CompositeDefinition = compositeDefinition;
      // Preallocate all of the stages.
      for (var i = StageType.Start; i < StageType.Count + 1; ++i)
      {
        var stageDef = CompositeDefinition.FindStage(i);
        mStages.Add(new StageLinkage(this, stageDef));
      }
    }

    public StageLinkage FindStage(StageType stageType)
    {
      if (stageType < StageType.Start || stageType >= StageType.Count)
        return null;
      return mStages[(int)stageType];
    }

    public IEnumerable<StageLinkage> GetStages()
    {
      return mStages;
    }
  }
}
