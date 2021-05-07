using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Settings that describe the attributes that are available to the vertex stage when building a composited shader.
  public class AttributeSettings
  {
    public class VertexAttribute
    {
      public string Name;
      public ShaderType Type;
      public int Location;

      public string GetDebugString()
      {
        return $"{Type.GetPrettyName()} {Name}";
      }
    }

    public bool Locked { get; private set; }
    public List<VertexAttribute> VertexAttributes = new List<VertexAttribute>();

    /// Adds a new attribute with the given type and name. The attribute location must be unique for all attributes.
    public void AddAttribute(ShaderType type, string name, int location)
    {
      VertexAttributes.Add(new VertexAttribute
      {
        Type = type,
        Name = name,
        Location = location
      });
    }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
      Validate(true);
    }

    public bool Validate(bool throwException)
    {
      bool isValid = true;
      var usedLocations = new Dictionary<int, VertexAttribute>();
      foreach (var attribute in VertexAttributes)
      {
        var usedLocation = usedLocations.GetValueOrDefault(attribute.Location);
        if (usedLocation != null)
        {
          isValid = false;
          if(throwException)
            throw new System.Exception($"Attribute '{attribute.GetDebugString()}' uses location {attribute.Location} which is already used by '{usedLocation.GetDebugString()}'");
        }
        usedLocations.Add(attribute.Location, attribute);
      }
      return isValid;
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("Attribute settings are locked");
    }
  }
}
