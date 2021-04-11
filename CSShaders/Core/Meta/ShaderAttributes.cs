using Microsoft.CodeAnalysis;
using System;
using System.Collections;
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
    public TypeName Name;
    public List<ShaderAttributeParameter> Parameters = new List<ShaderAttributeParameter>();
    public AttributeData Attribute;

    public ShaderAttribute Clone()
    {
      var result = new ShaderAttribute();
      result.Name = Name.Clone();
      result.Attribute = Attribute;
      foreach (var param in Parameters)
        result.Parameters.Add(new ShaderAttributeParameter { Name = param.Name, Value = param.Value });
      return result;
    }
  }

  public class ShaderAttributes : IEnumerable<ShaderAttribute>
  {
    public delegate void AttributeCallback(ShaderAttribute attribute);
    List<ShaderAttribute> Attributes = new List<ShaderAttribute>();

    public ShaderAttribute Add(Type attributeType)
    {
      return Add(new ShaderAttribute { Name = TypeAliases.GetTypeName(attributeType) });
    }

    public ShaderAttribute Add(ShaderAttribute attribute)
    {
      Attributes.Add(attribute);
      return attribute;
    }

    public ShaderAttribute Add(string attributeName)
    {
      return Add(new ShaderAttribute { Name = new TypeName { Name = attributeName } });
    }
    
    public bool Contains(string attributeName)
    {
      foreach(var attribute in Attributes)
      {
        if (attribute.Name.Name == attributeName)
          return true;
      }
      return false;
    }

    public void Clear()
    {
      Attributes.Clear();
    }
    
    public bool Contains(Type attributeType)
    {
      return Contains(attributeType.Name);
    }

    public void ForeachAttribute(TypeName attributeName, AttributeCallback callback)
    {
      foreach (var attribute in Attributes)
      {
        if (attribute.Name == attributeName)
          callback(attribute);
      }
    }

    public void ForeachAttribute(Type attributeType, AttributeCallback callback)
    {
      ForeachAttribute(TypeAliases.GetTypeName(attributeType), callback);
    }

    public IEnumerator<ShaderAttribute> GetEnumerator()
    {
      return Attributes.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public ShaderAttributes Clone()
    {
      var result = new ShaderAttributes();
      foreach (var attribute in Attributes)
        result.Add(attribute.Clone());
      return result;
    }
  }
}
