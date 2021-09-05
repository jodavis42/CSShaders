namespace CSShaders.Compositing
{
  /// Represents a variable instance (a variable name + type).
  /// This variable can be owned by another variable (e.g. a member of a local variable).
  public class VariableInstance
  {
    /// The type of this variable
    public TypeDefinition Type;
    /// The name of the variable
    public string Name;
    /// The variable that owns this one (e.g. the containing struct instance)
    public VariableInstance Owner;

    /// Finds or creates the name of a field instance from this variable
    public FieldInstance GetFieldInstance(string fieldName)
    {
      foreach (var field in Type.Fields)
      {
        if (field.Name == fieldName)
          return new FieldInstance { Type = field.Type, Name = fieldName, Owner = this, Field = field };
      }
      return null;
    }

    /// Creates a new local instance variable for a type with the given name
    public static VariableInstance CreateLocalInstance(TypeDefinition type, string name)
    {
      return new VariableInstance { Type = type, Name = name, Owner = null };
    }
  }
}
