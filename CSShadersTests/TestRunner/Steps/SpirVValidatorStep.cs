namespace CSShadersTests
{
  public class SpirVValidatorStep : Step
  {
    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public const string DefaultKey = "Validator";
    public string SpvPathKey = DefaultSpvPathKey;
    public string Key = DefaultKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var spirvBinaryPath = blackboard.Get<string>(SpvPathKey);
      var tool = new SpirVValidatorTool();
      var validatorResults = tool.Run(spirvBinaryPath);
      if(!string.IsNullOrWhiteSpace(validatorResults))
        logger.Log($"SpirV validator found issues for file '{spirvBinaryPath}'", ILogger.Verbosity.Error);

      blackboard.Add(Key, validatorResults);
      return StepResult.Success;
    }
  }
}
