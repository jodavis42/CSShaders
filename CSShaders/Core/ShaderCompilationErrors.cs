using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderCodeLocation
  {
    public ShaderCodeLocation() { }
    public ShaderCodeLocation(Location location)
    {
      Location = location;
    }

    Location Location;
  }

  public class ShaderCompilationErrors
  {
    public delegate void ErrorHandler(ShaderCodeLocation location, string message);
    public List<ErrorHandler> ErrorHandlers = new List<ErrorHandler>();

    public void SendTranslationError(string message)
    {
      SendTranslationError(null, message);
    }

    public void SendTranslationError(ShaderCodeLocation location, string message)
    {
      foreach(var handler in ErrorHandlers)
      {
        handler.Invoke(location, message);
      }
    }
  }
}
