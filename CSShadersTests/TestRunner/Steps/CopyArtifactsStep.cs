using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSShadersTests
{
  class CopyArtifactsStep : Step
  {
    public const string DefaultKey = "CopyArtifacts";
    public string Key = DefaultKey;

    public const string DefaultRootPathKey = "RootPathKey";
    public string RootPathKey = DefaultRootPathKey;

    public const string DefaultTargetPathKey = "TargetPathKey";
    public string TargetPathKey = DefaultTargetPathKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var copyArtifacts = blackboard.GetValue<bool>(Key);
      if (!copyArtifacts)
        return StepResult.Success;

      var artifactList = blackboard.Get<ArtifactsList>();
      if (artifactList == null)
        return StepResult.Success;

      var rootPath = blackboard.Get<string>(RootPathKey);
      var targetPath = blackboard.Get<string>(TargetPathKey);
      if (string.IsNullOrEmpty(targetPath))
        return StepResult.Success;

      foreach (var filePath in artifactList)
      {
        var relativePath = Path.GetRelativePath(rootPath, filePath);
        var targetFilePath = Path.GetDirectoryName(Path.Combine(targetPath, relativePath));
        Directory.CreateDirectory(targetFilePath);
        File.Copy(filePath, Path.Combine(targetFilePath, Path.GetFileName(filePath)), true);
      }

      return StepResult.Success;
    }
  }
}
