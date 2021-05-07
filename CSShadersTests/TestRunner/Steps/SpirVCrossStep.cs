namespace CSShadersTests
{
  public class SpirVCrossStep : Step
  {
    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public const string DefaultOutPathKey = TestConfigStep.ActualGlslPathKey;
    public string SpvPathKey = DefaultSpvPathKey;
    public string OutPathKey = DefaultOutPathKey;

    public const string DefaultStageKey = "";
    public string StageKey = DefaultStageKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var spvPath = blackboard.Get<string>(SpvPathKey);
      var outPath = blackboard.Get<string>(OutPathKey);
      var stage = blackboard.Get<string>(StageKey);
      if (stage == null)
        stage = "";

      var tool = new SpirVCrossTool();
      tool.Run(spvPath, outPath, stage);

      var artifactList = blackboard.Get<ArtifactsList>();
      artifactList.Add(outPath);

      return StepResult.Success;
    }
  }
}
