using CSShaders;

namespace CSShadersTests
{
  public class CompositeTestData
  {
    public string ScriptFilePath;
    public string CompositeSpvBinaryFilePath;
    public TestFilePaths CompositeDisassemblerPaths = new TestFilePaths();
    public TestFilePaths CompositeGlslPaths = new TestFilePaths();
  }

  public class CompositeConfigStep : Step
  {
    public const string GeneratorKey = "Generator";
    public const string FragmentProjectKey = "FragmentProject";
    public const string FragmentLibraryKey = "FragmentLibrary";
    public const string ShaderProjectKey = "ShaderProject";
    public const string ShaderLibraryKey = "ShaderLibrary";
    public const string CompositeLibraryKey = "CompositeLibraryKey";
    public const string GeneratedCompositeLibraryKey = "GeneratedCompositeLibraryKey";

    public const string ScriptPathKey = "ScriptPath";
    public const string CompositeSpvBinaryPathKey = "CompositeSpvBinaryPath";


    public const string ExpectedCompositeDisassemblyPathKey = "EpectedCompositeDisassemblyPath";
    public const string ActualCompositeDisassemblyPathKey = "ActualCompositeDisassemblyPath";
    public const string DiffCompositeDisassemblyPathKey = "DiffCompositeDisassemblyPath";
    public const string DiffCompositeDisassemblyKey = "DiffCompositeDisassembly";

    public const string ExpectedCompositeGlslPathKey = "ExpectedCompositeGlslPath";
    public const string ActualCompositeGlslPathKey = "ActualCompositeGlslPath";
    public const string DiffCompositeGlslPathKey = "DiffCompositeGlslPath";
    public const string DiffCompositeGlslKey = "DiffCompositeGlsl";

    public const string TestFailedKey = "TestFailed";

    public override StepResult Run(Blackboard blackboard)
    {
      var testData = blackboard.Get<CompositeTestData>();

      var generator = blackboard.Get< SimpleShaderGenerator>(GeneratorKey);
      blackboard.Add(FragmentProjectKey, generator.FragmentProject);
      blackboard.Add(FragmentLibraryKey, generator.FragmentLibrary);
      blackboard.Add(ShaderProjectKey, generator.ShaderProject);
      blackboard.Add(ShaderLibraryKey, generator.ShaderLibrary);

      blackboard.Add(ScriptPathKey, testData.ScriptFilePath);
      blackboard.Add(CompositeSpvBinaryPathKey, testData.CompositeSpvBinaryFilePath);

      blackboard.Add(ExpectedCompositeDisassemblyPathKey, testData.CompositeDisassemblerPaths.ExpectedFilePath);
      blackboard.Add(ActualCompositeDisassemblyPathKey, testData.CompositeDisassemblerPaths.GeneratedFilePath);
      blackboard.Add(DiffCompositeDisassemblyPathKey, testData.CompositeDisassemblerPaths.DiffFilePath);

      blackboard.Add(ExpectedCompositeGlslPathKey, testData.CompositeGlslPaths.ExpectedFilePath);
      blackboard.Add(ActualCompositeGlslPathKey, testData.CompositeGlslPaths.GeneratedFilePath);
      blackboard.Add(DiffCompositeGlslPathKey, testData.CompositeGlslPaths.DiffFilePath);

      return StepResult.Success;
    }
  }
}
