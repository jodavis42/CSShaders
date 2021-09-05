using CommandLine;
using CSShaders;
using System.Collections.Generic;
using System.IO;

namespace CSShadersTests
{
  public class Options
  {
    [Option("workingDir", Required = false)]
    public string WorkingDir { get; set; } = null;
    [Option("artifactsDir", Required = false)]
    public string ArtifactsDir { get; set; } = null;
    [Option("testPath", Required = false)]
    public string TestPath { get; set; } = null;
    [Option('v', "verbose", Required = false, HelpText = "Verbose logging.")]
    public bool Verbose { get; set; } = false;
    [Option("visualDiff", Required = false, HelpText = "Should a visual differ be used")]
    public bool VisualDiff { get; set; } = false;
    [Option("visualDiffTool", Required = false, HelpText = "Path to the visual diff tool to use")]
    public string VisualDiffTool { get; set; } = null;
    [Option("visualDiffToolArgs", Required = false, HelpText = "Arguments to use for the visual diff tool")]
    public string VisualDiffToolArgs { get; set; } = null;
  }

  public class Context
  {
    public bool Success = true;
    public ILogger Logger = new ConsoleLogger();
    public SimpleShaderGenerator SimpleGenerator = new SimpleShaderGenerator();
    public Pipeline BasicPipeline;
  }

  class Program
  {
    static int Main(string[] args)
    {
      var context = new Context();
      Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o => { ParseSucceeded(o, context); })
        .WithNotParsed<Options>(o => { ParseFailed(o, context); });
      return context.Success ? 0 : 1;
    }

    static void ParseFailed(IEnumerable<Error> errors, Context context)
    {
      context.Success = false;
      context.Logger.Log("Command Line Error:", ILogger.Verbosity.Error);
      foreach (var error in errors)
      {
        context.Logger.Log(error.ToString(), ILogger.Verbosity.Error);
      }
    }

    static void ParseSucceeded(Options options, Context context)
    {
      if (!string.IsNullOrWhiteSpace(options.ArtifactsDir))
        Directory.CreateDirectory(options.ArtifactsDir);

      var path = "Tests";
      if (!string.IsNullOrWhiteSpace(options.TestPath))
        path = options.TestPath;


      if (!string.IsNullOrEmpty(options.VisualDiffTool))
        ToolSettings.VisualDiffToolPath = options.VisualDiffTool;
      if (!string.IsNullOrEmpty(options.VisualDiffToolArgs))
        ToolSettings.VisualDiffToolArgs = options.VisualDiffToolArgs;

      var testRunner = new TestRunner()
      {
        ArtifactsDir = options.ArtifactsDir,
        VisualDiff = options.VisualDiff,
        Logger = context.Logger
      };
      if (options.Verbose)
        testRunner.Logger.MessageVerbosity = ILogger.Verbosity.Verbose;

      context.Success = testRunner.Run(path);
    }
  }
}
