using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// The definition of a composite to generate from a collection of fragments
  public class CompositeDefinition
  {
    public string Name;
    public List<ShaderType> Fragments = new List<ShaderType>();
  }
}
