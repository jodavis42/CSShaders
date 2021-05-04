using System.Collections.Generic;
using System.IO;

namespace CSShadersTests
{
  class CopyIfFileExistsStep : Step
  {
    public const string DefaultRootPathKey = "RootPathKey";
    public const string DefaultTargetPathKey = "TargetPathKey";

    public string RootPathKey = DefaultRootPathKey;
    public string TargetPathKey = DefaultTargetPathKey;

    public string ConditionalFileKey;
    public List<string> FilePathKeysToCopy;

    public override StepResult Run(Blackboard blackboard)
    {
      var conditionalFile = blackboard.Get<string>(ConditionalFileKey);
      if(!File.Exists(conditionalFile))
        return StepResult.Success;

      var rootPath = blackboard.Get<string>(RootPathKey);
      var targetPath = blackboard.Get<string>(TargetPathKey);
      if(string.IsNullOrEmpty(targetPath))
        return StepResult.Success;

      var fullResultDirectory = Path.GetDirectoryName(Path.Combine(targetPath, Path.GetRelativePath(rootPath, conditionalFile)));
      Directory.CreateDirectory(fullResultDirectory);
      foreach(var filePathKey in FilePathKeysToCopy)
      {
        var filePath = blackboard.Get<string>(filePathKey);
        File.Copy(filePath, Path.Combine(fullResultDirectory, Path.GetFileName(filePath)), true);
      }

      return StepResult.Success;
    }
  }
}
