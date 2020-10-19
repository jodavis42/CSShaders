using Microsoft.CodeAnalysis;

namespace CSShaders
{
  /// <summary>
  /// Debug information about the original source file.
  /// </summary>
  public class ShaderDebugInfo
  {
    /// The location in the original file that this objects came from.
    public Location Location;
    /// The name to use for debugging purposes in the final shader byte code
    public string Name;
  }
}
