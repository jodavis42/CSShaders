using System.IO;

namespace CSShadersTests
{
  public class CompositeDiffStep : Step
  {
    public const string DefaultKey = "Diff";
    public string Key = DefaultKey;

    public const string DefaultCompositeDirectoryKey = "CompositeDirectoryKey";
    public string CompositeDirectoryKey = DefaultCompositeDirectoryKey;

    public const string DefaultGeneratedCompositeLibraryKey = "GeneratedCompositeLibraryKey";
    public string GeneratedCompositeLibraryKey = DefaultGeneratedCompositeLibraryKey;

    public const string DefaultDiffsGeneratedKey = "DiffsGeneratedKey";
    public string DiffsGeneratedKey = DefaultDiffsGeneratedKey;

    public bool VisualDisplay = false;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var artifacts = blackboard.Get<ArtifactsList>();

      var generatedProject = blackboard.Get<CSShaders.Compositing.GeneratedProject>(GeneratedCompositeLibraryKey);
      if (generatedProject == null)
      {
        logger.Log($"Failed to find expected file path key ${GeneratedCompositeLibraryKey}");
        return StepResult.Failed;
      }

      bool foundDiffs = blackboard.GetValue<bool>(DefaultDiffsGeneratedKey);
      var compositeDirectory = blackboard.Get<string>("CompositeDirectoryKey");
      foreach (var composite in generatedProject.Files)
      {
        var generatedText = composite.Code;
        var basePath = Path.Combine(compositeDirectory, composite.FileName);
        var generatedOutPath = Path.ChangeExtension(basePath, ".csshader.generated.txt");
        var expectedGlslOutPath = Path.ChangeExtension(basePath, ".csshader.expected.txt");
        artifacts.Add(generatedOutPath);
        artifacts.Add(expectedGlslOutPath);

        File.WriteAllText(generatedOutPath, generatedText);

        var tool = new VisualDiffTool();
        tool.VisualDisplay = VisualDisplay;
        bool result = tool.Diff(expectedGlslOutPath, generatedOutPath);
        if (!result)
        {
          logger.Log($"Expected file '{expectedGlslOutPath}' and actual file '{generatedOutPath}' had differences", ILogger.Verbosity.Error);
          foundDiffs = true;
        }
      }

      blackboard.Add(DiffsGeneratedKey, foundDiffs, Blackboard.AddMode.Override);

      return StepResult.Success;
    }
  }
}
