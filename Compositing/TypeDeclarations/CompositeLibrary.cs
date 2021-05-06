using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Represents a library of types created during compositing. These types may or may not exist as shader types depending on if they're discovered or generated.
  public class CompositeLibrary
  {
    public Dictionary<TypeName, TypeDefinition> Types = new Dictionary<TypeName, TypeDefinition>();
  }
}
