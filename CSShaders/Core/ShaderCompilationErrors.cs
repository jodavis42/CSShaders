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

    public override string ToString()
    {
      var lineSpan = Location.GetMappedLineSpan();
      return string.Format("{0}({1},{2})", lineSpan.Path, lineSpan.StartLinePosition, lineSpan.EndLinePosition);
    }

    Location Location;
  }

  public class ShaderTranslationError
  {
    public ShaderCodeLocation Location;
    public string Message = "";
  }


  public class ShaderCompilationErrors
  {
    public delegate void ErrorHandler(ShaderTranslationError error);
    public List<ErrorHandler> ErrorHandlers = new List<ErrorHandler>();

    public void SendTranslationError(string message)
    {
      SendTranslationError(null, message);
    }

    public void SendTranslationError(ShaderCodeLocation location, string message)
    {
      var error = new ShaderTranslationError { Location = location, Message = message };
      SendTranslationError(error);
    }

    public void SendTranslationError(ShaderTranslationError error)
    {
      foreach (var handler in ErrorHandlers)
      {
        handler.Invoke(error);
      }
    }
  }
}
