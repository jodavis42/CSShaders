namespace CSShadersTests
{
  public class TestFilePaths
  {
    public string GeneratedFilePath;
    public string ExpectedFilePath;
    public string DiffFilePath;
  }
  public class TestData
  {
    public string ScriptFilePath;
    public string SpvBinaryFilePath;
    public TestFilePaths DisassemblerTest = new TestFilePaths();
    public TestFilePaths GlslTest = new TestFilePaths();
  }

  public class TestConfigStep : Step
  {
    public const string ScriptPathKey = "ScriptPath";
    public const string SpvBinaryPathKey = "SpvBinaryPath";
    public const string ExpectedGlslPathKey = "EpectedGlslPath";
    public const string ActualGlslPathKey = "ActualGlslPath";
    public const string DiffGlslPathKey = "DiffGlslPath";
    public const string DiffGlslKey = "DiffGlsl";
    public const string ExpectedDisassemblyPathKey = "EpectedDisassemblyPath";
    public const string ActualDisassemblyPathKey = "ActualDisassemblyPath";
    public const string DiffDisassemblyPathKey = "DiffDisassemblyPath";
    public const string DiffDisassemblyKey = "DiffDisassembly";
    public const string TestFailedKey = "TestFailed";

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var testData = blackboard.Get<TestData>();
      blackboard.Add(ScriptPathKey, testData.ScriptFilePath);
      blackboard.Add(SpvBinaryPathKey, testData.SpvBinaryFilePath);
      blackboard.Add(ExpectedDisassemblyPathKey, testData.DisassemblerTest.ExpectedFilePath);
      blackboard.Add(ActualDisassemblyPathKey, testData.DisassemblerTest.GeneratedFilePath);
      blackboard.Add(DiffDisassemblyPathKey, testData.DisassemblerTest.DiffFilePath);
      blackboard.Add(ExpectedGlslPathKey, testData.GlslTest.ExpectedFilePath);
      blackboard.Add(ActualGlslPathKey, testData.GlslTest.GeneratedFilePath);
      blackboard.Add(DiffGlslPathKey, testData.GlslTest.DiffFilePath);
      return StepResult.Success;
    }
  }
}
