using System.IO;

namespace CSShadersTests
{
  public class ArtifactListFilePathAppendStep : Step
  {
    public string FilePathKey = "";

    public override StepResult Run(Blackboard blackboard)
    {
      var filePath = blackboard.Get<string>(FilePathKey);
      if (filePath == null)
        return StepResult.Success;

      var artifactList = blackboard.Get<ArtifactsList>();
      if(artifactList == null)
      {
        var logger = blackboard.Get<ILogger>("Logger");
        logger.Log("Failed to get artifact list", ILogger.Verbosity.Error);
        return StepResult.Failed;
      }
      if(File.Exists(filePath))
        artifactList.Add(filePath);

      return StepResult.Success;
    }
  }
}
