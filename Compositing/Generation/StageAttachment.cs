using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// An attachment type represents a path for which data is transferred between stages.
  public enum StageAttachmentType
  {
    None = -1,
    /// The standard vertex stream of data. This is how most everything is transferred between stages.
    Vertex = 0,
    /// The primitive stream is for geometry/tessellation stages that transfer some data about primitives as well as vertex data.
    Primitive = 1
  }

  /// An attachment for a stage is is one path in which data transfers between stages.
  /// The most common way for data to flow is via a vertex. This is how the typical vertex->pixel flow works.
  /// Some stages can contain other ways to transfer data as well,
  /// such as geometry shaders can transfer data about the primitive.
  public class StageAttachment
  {
    public StageAttachmentType AttachmentType { get; internal set; } = StageAttachmentType.None;
    public StageDefinition OwningStageDefinition { get; internal set; }
    /// The variable instances on the attachment are typically the fragments in the staeg
    public List<VariableInstance> Variables = new List<VariableInstance>();
    /// The links for the output fields of the stage
    public VariableLinkList StageLinks = new VariableLinkList();
    /// Links for how the inputs of each variable are connected
    private Dictionary<VariableInstance, VariableLinkList> VariableLinks = new Dictionary<VariableInstance, VariableLinkList>();

    public VariableLinkList FindLinks(VariableInstance varInstance)
    {
      return VariableLinks.GetValueOrDefault(varInstance);
    }

    public VariableLinkList FindOrCreateLinks(VariableInstance varInstance)
    {
      var links = FindLinks(varInstance);
      if (links == null)
      {
        links = new VariableLinkList();
        VariableLinks.Add(varInstance, links);
      }
      return links;
    }
  }
}
