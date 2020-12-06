namespace CSShaders
{
  /// <summary>
  /// Represents a field on a type.
  /// </summary>
  public class ShaderField
  {
    /// The underlying type of this field
    public ShaderType mType;
    /// The meta information about this field from the original source language.
    public ShaderFieldMeta mMeta = null;
    public bool IsStatic = false;

    /// Debug information about this instruction
    public ShaderDebugInfo DebugInfo = new ShaderDebugInfo();
  }

  /// <summary>
  /// Global/Static fields might belong to a type, but they can't be actually declared or
  /// initialized in a type. This gives a location to store that extra information.
  /// </summary>
  public class GlobalShaderField : ShaderField
  {
    public ShaderOp InstanceOp;
    public IShaderIR InitialValue;
  }
}
