namespace CSShaders.Compositing
{
  /// A field instance represents the instance of a field on a varaible. The just allows the original field definition to be tracked.
  public class FieldInstance : VariableInstance
  {
    public FieldDefinition Field;

    public FieldInstance Clone(VariableInstance owner = null)
    {
      var clone = new FieldInstance()
      {
        Type = this.Type,
        Name = this.Name,
        Owner = owner,
        Field = this.Field.Clone(),
      };
      return clone;
    }
  }
}
