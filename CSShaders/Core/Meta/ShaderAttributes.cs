using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderAttributeParameter
  {
    public string Name = "";
    public object Value;
  }

  public class ShaderAttribute
  {
    public string Name = "";
    public List<ShaderAttributeParameter> Parameters = new List<ShaderAttributeParameter>();
  }

  public class ShaderAttributes
  {
    List<ShaderAttribute> Attributes = new List<ShaderAttribute>();

    public void Add(ShaderAttribute attribute)
    {
      Attributes.Add(attribute);
    }

    public void Add(string attributeName)
    {
      Add(new ShaderAttribute { Name = attributeName });
    }
    
    public bool Contains(string attributeName)
    {
      foreach(var attribute in Attributes)
      {
        if (attribute.Name == attributeName)
          return true;
      }
      return false;
    }
    
    public bool Contains(Type attributeType)
    {
      return Contains(attributeType.Name);
    }
  }
}
