using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// For a fragment field, how did the compositor choose to resolve an input.
  public enum FieldResolutionType
  {
    None,
    Fragment,
    Stage,
    AppBuiltIn,
    HardwareBuiltIn,
    SpecConstant,
    Property,
  }

  public class FieldReference
  {
    public ShaderField Field { get; internal set; }
    public ShaderType Owner { get; internal set; }
    public string Name => Field?.mMeta?.mName;
    public string OwnerName => Owner?.mMeta?.mName;

    public FieldReference() { }
    public FieldReference(FieldDefinition fieldDef)
    {
      Field = fieldDef.ShaderField;
      Owner = fieldDef.Owner?.ShaderType;
    }

    public FieldReference(ShaderType owner, ShaderField field)
    {
      Field = field;
      Owner = owner;
    }
  }

  /// The reflection for a field of a fragment.
  public class FieldReflection
  {
    /// How was this field resovled as an input
    public FieldResolutionType ResolutionType { get; } = FieldResolutionType.None;
    /// Information about where this field came from (name, type, owner, etc...)
    public FieldReference Field { get; }
    /// If this field was resolved as [FragmentInput], this is the field that this matches to
    public FieldReference SourceField { get; }

    public FieldReflection() { }
    public FieldReflection(FieldResolutionType resolutionType, FieldDefinition field, FieldDefinition sourceField)
    {
      ResolutionType = resolutionType;
      Field = (field != null) ? new FieldReference(field) : null;
      SourceField = (sourceField != null) ? new FieldReference(sourceField) : null;
    }
  }

  /// Reflection information about a fragment
  public class FragmentReflection
  {
    public ShaderType ShaderType;
    public List<FieldReflection> Fields = new List<FieldReflection>();
  }

  /// Reflection info about one shader. This represents a generated or composited shader (i.e. the result of one shader stage)
  public class ShaderReflection
  {
    public List<FragmentReflection> Fragments = new List<FragmentReflection>();
  }
}
