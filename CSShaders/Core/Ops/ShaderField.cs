namespace CSShaders
{
  /// <summary>
  /// Represents a field on a type.
  /// </summary>
  public class ShaderField : IShaderIR
  {
    /// The underlying type of this field
    public ShaderType mType;
    /// The meta information about this field from the original source language.
    public ShaderFieldMeta mMeta = null;
    public bool IsStatic = false;
  }
}
