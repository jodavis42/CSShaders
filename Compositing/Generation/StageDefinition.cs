using System.Collections.Generic;

namespace CSShaders.Compositing
{
  public class StageDefinition : TypeDefinition
  {
    public StageType StageType { get; } = StageType.None;
    /// The fragments that belong in this stage
    public List<VariableInstance> Fragments = new List<VariableInstance>();
    /// Resultant reflection information about how fragments were connected together
    public ShaderReflection Reflection = new ShaderReflection();
    /// During compositing, fragments may need to link to the stage itself.
    /// This is the self or "this" local varaible for them to link to.
    public VariableInstance Self;

    Dictionary<StageAttachmentType, StageAttachment> Attachments = new Dictionary<StageAttachmentType, StageAttachment>();

    public StageDefinition(StageType stageType)
    {
      StageType = stageType;
      Self = VariableInstance.CreateLocalInstance(this, "this");
    }

    public StageAttachment FindAttachment(StageAttachmentType attachmentType)
    {
      return Attachments.GetValueOrDefault(attachmentType);
    }

    public StageAttachment FindOrCreateAttachment(StageAttachmentType attachmentType)
    {
      var attachment = FindAttachment(attachmentType);
      if (attachment == null)
      {
        attachment = new StageAttachment() { AttachmentType = attachmentType };
        attachment.OwningStageDefinition = this;
        Attachments.Add(attachmentType, attachment);
      }
      return attachment;
    }

    public IEnumerable<StageAttachment> GetAttachments()
    {
      return Attachments.Values;
    }
  }
}
