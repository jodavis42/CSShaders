using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Settings that describe the render targets that are available to the output of the pixel stage when building a composited shader.
  public class RenderTargetSettings
  {
    public class RenderTarget
    {
      public string Name;
      public ShaderType Type;
      public int Location;

      public string GetDebugString()
      {
        return $"{Type.GetPrettyName()} {Name}";
      }
    }

    public List<RenderTarget> RenderTargets = new List<RenderTarget>();
    public bool Locked { get; private set; }

    /// Adds a new render target with the given type and name. The location must be unique for all render targets.
    public void AddNamedRenderTarget(ShaderLibrary library, string name, int location)
    {
      var vector4Type = library.FindType(new TypeKey(typeof(Math.Vector4)));
      AddNamedRenderTarget(vector4Type, name, location);
    }

    /// Adds a new render target with the given type and name. The location must be unique for all render targets.
    public void AddNamedRenderTarget(ShaderType type, string name, int location)
    {
      RenderTargets.Add(new RenderTarget
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
      var usedLocations = new Dictionary<int, RenderTarget>();
      foreach (var target in RenderTargets)
      {
        var usedLocation = usedLocations.GetValueOrDefault(target.Location);
        if (usedLocation != null)
        {
          isValid = false;
          if (throwException)
            throw new System.Exception($"Attribute '{target.GetDebugString()}' uses location {target.Location} which is already used by '{usedLocation.GetDebugString()}'");
        }
        usedLocations.Add(target.Location, target);
      }
      return isValid;
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("RenderTarget settings are locked");
    }
  }
}
