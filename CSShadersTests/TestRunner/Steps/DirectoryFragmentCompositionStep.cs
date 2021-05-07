using CSShaders;

namespace CSShadersTests
{
  public class DirectoryFragmentCompositionStep : Step
  {
    public const string DefaultGeneratorKey = "Generator";
    public string GeneratorKey = DefaultGeneratorKey;

    public const string DefaultFragmentDirectoryKey = "FragmentDirectory";
    public string FragmentDirectoryKey = DefaultFragmentDirectoryKey;

    public const string DefaultOutShaderLibraryKey = "ShaderLibraryKey";
    public string OutShaderLibraryKey = DefaultOutShaderLibraryKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var generator = blackboard.Get<SimpleShaderGenerator>(GeneratorKey);
      var fragmentDirectory = blackboard.Get<string>(FragmentDirectoryKey);

      logger.Log($"Testing fragment directory {fragmentDirectory}", ILogger.Verbosity.Message);
      generator.ClearFragmentProject();
      generator.LoadFragmentProject(fragmentDirectory);
      generator.CompileFragmentProject();

      blackboard.Add(OutShaderLibraryKey, generator.FragmentLibrary, Blackboard.AddMode.Override);

      var artifactList = blackboard.Get<ArtifactsList>();
      foreach(var entry in generator.FragmentProject.CodeEntries)
        artifactList.Add(entry.Location);

      return StepResult.Success;
    }
  }
}