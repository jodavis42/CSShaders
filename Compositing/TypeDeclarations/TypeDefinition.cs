using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Represents the definition of a type that may or may not yet exist as a shader type (we could be generating it now).
  public class TypeDefinition
  {
    /// Is this field discovered from a shader type or was it generated? This helps with emitting code.
    public bool Generated = true;

    public ShaderAttributes Attributes = new ShaderAttributes();
    /// The name of this type
    public TypeName TypeName;
    /// The shortname is the name of the type with no namespace qualifiers
    public string ShortName => TypeName?.Name;
    /// The fully qualified name (namespace)
    public string FullName => TypeName?.FullName;
    /// The list of fields on this type
    public List<FieldDefinition> Fields = new List<FieldDefinition>();
    /// If this was created from a shader type, this will point to that type
    public ShaderType ShaderType;

    public FieldDefinition AddField(TypeDefinition type, string name)
    {
      return AddField(null, type, name);
    }

    public FieldDefinition AddField(ShaderField shaderField, TypeDefinition type, string name)
    {
      var field = new FieldDefinition()
      {
        Type = type,
        Name = name,
        Owner = this,
        ShaderField = shaderField,
      };
      Fields.Add(field);
      return field;
    }
  }
}
