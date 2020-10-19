namespace CSShaders
{
  /// <summary>
  /// Represents a constant literal of a certain type since instructions can only reference other instructions.
  /// </summary>
  public class ShaderConstantLiteral : IShaderIR
  {
    /// The type of this literal.
    public ShaderType mType;
    /// The value of this literal
    public object mValue = null;
  }
}
