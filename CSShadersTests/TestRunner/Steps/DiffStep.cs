namespace CSShadersTests
{
  public class FileDiffStep : Step
  {
    public const string DefaultKey = "Diff";
    public string Key = DefaultKey;
    public string ExpectedFilePathKey;
    public string ActualFilePathKey;
    public bool VisualDisplay = false;

    public FileDiffStep(string expectedFilePathKey, string actualFilePathKey)
    {
      ExpectedFilePathKey = expectedFilePathKey;
      ActualFilePathKey = actualFilePathKey;
    }

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");

      var expectedFilePath = blackboard.Get<string>(ExpectedFilePathKey);
      if (expectedFilePath == null)
      {
        logger.Log($"Failed to find expected file path key ${ExpectedFilePathKey}");
        return StepResult.Failed;
      }
      var actualFilePath = blackboard.Get<string>(ActualFilePathKey);
      if (actualFilePath == null)
      {
        logger.Log($"Failed to find expected file path key ${ActualFilePathKey}");
        return StepResult.Failed;
      }

      var tool = new VisualDiffTool();
      tool.VisualDisplay = VisualDisplay;
      bool result = tool.Diff(expectedFilePath, actualFilePath);
      if(!result)
      {
        logger.Log($"Expected file '{expectedFilePath}' and actual file '{actualFilePath}' had differences", ILogger.Verbosity.Error);
        if (!string.IsNullOrWhiteSpace(tool.mDiffText))
          blackboard.Add(Key, tool.mDiffText);
      }
      
      return StepResult.Success;
    }
  }
}
