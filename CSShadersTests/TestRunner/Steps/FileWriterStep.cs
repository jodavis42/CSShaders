using System.IO;

namespace CSShadersTests
{
  public class FileWriterStep : Step
  {
    string FileContentsKey;
    string OutFilePathKey;

    public FileWriterStep(string fileContentsKey, string outFilePathKey)
    {
      FileContentsKey = fileContentsKey;
      OutFilePathKey = outFilePathKey;
    }

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var fileContents = blackboard.Get<string>(FileContentsKey);
      var outFilePath = blackboard.Get<string>(OutFilePathKey);
      if (!string.IsNullOrEmpty(fileContents))
        File.WriteAllText(outFilePath, fileContents);
      return StepResult.Success;
    }
  }
}
