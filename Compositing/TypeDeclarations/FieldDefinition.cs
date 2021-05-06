namespace CSShaders.Compositing
{
  /// Represents the definition of a field that may or may not yet exist as a shader type (we could be generating it now).
  public class FieldDefinition
  {
    public ShaderAttributes Attributes = new ShaderAttributes();
    /// The type of this field
    public TypeDefinition Type;
    /// The name of this field
    public string Name;
    /// The type that owns this field
    public TypeDefinition Owner;
    /// If this was created from a ShaderField, this will be that field. Otherwise this will be null.
    public ShaderField ShaderField;

    public FieldDefinition Clone(TypeDefinition owner = null)
    {
      var clone = new FieldDefinition
      {
        Attributes = this.Attributes.Clone(),
        Type = this.Type,
        Name = this.Name,
        Owner = owner,
      };
      return clone;
    }
  }
}
