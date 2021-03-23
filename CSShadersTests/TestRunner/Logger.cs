using System;

namespace CSShadersTests
{
  public class ILogger
  {
    public enum Verbosity { None, Error, Warning, Message, Verbose };
    public Verbosity MessageVerbosity = Verbosity.Error;

    public virtual void Log(string message, Verbosity verbosity = Verbosity.Error)
    {

    }
  }

  public class ConsoleLogger : ILogger
  {
    public override void Log(string message, Verbosity verbosity = Verbosity.Error)
    {
      if (verbosity > MessageVerbosity)
        return;

      Console.WriteLine($"[{verbosity.ToString()}]: {message}");
    }
  }
}
