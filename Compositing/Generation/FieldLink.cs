using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Represents an assignment between two variables.
  /// This allows the compositor to mark two variables (fields) as transfering data.
  public class VariableLink
  {
    public VariableInstance Source;
    public VariableInstance Destination;
  }
  
  public class VariableLinkList : List<VariableLink>
  {

  }
}
