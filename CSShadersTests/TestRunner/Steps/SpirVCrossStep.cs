namespace CSShadersTests
{
  public class SpirVCrossStep : Step
  {
    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public const string DefaultOutPathKey = TestConfigStep.ActualGlslPathKey;
    public string SpvPathKey = DefaultSpvPathKey;
    public string OutPathKey = DefaultOutPathKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var spvPath = blackboard.Get<string>(SpvPathKey);
      var outPath = blackboard.Get<string>(OutPathKey);
      var tool = new SpirVCrossTool();
      tool.Run(spvPath, outPath);
      return StepResult.Success;
    }
  }
}
