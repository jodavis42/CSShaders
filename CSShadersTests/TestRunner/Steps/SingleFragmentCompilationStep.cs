using CSShaders;

namespace CSShadersTests
{
  class SingleFragmentCompilationStep : Step
  {
    public const string DefaultGeneratorKey = "Generator";
    public const string DefaultScriptFilePathKey = TestConfigStep.ScriptPathKey;
    public string GeneratorKey = DefaultGeneratorKey;
    public string ScriptFilePathKey = DefaultScriptFilePathKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var generator = blackboard.Get<SimpleShaderGenerator>(GeneratorKey);
      var scriptFilePath = blackboard.Get<string>(ScriptFilePathKey);

      logger.Log($"Testing fragment file {scriptFilePath}", ILogger.Verbosity.Message);
      generator.ClearFragmentProject();
      generator.LoadFragmentFile(scriptFilePath);
      generator.CompileFragmentProject();

      return StepResult.Success;
    }
  }
}
