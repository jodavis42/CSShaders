namespace CSShadersTests
{
  public class SpirVDisassemblerStep : Step
  {
    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public const string DefaultKey = "Disassembly";
    public string SpvPathKey = DefaultSpvPathKey;
    public string Key = DefaultKey;
    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var spirvBinaryPath = blackboard.Get<string>(SpvPathKey);
      var tool = new SpirVDisassemblerTool();
      var disassembly = tool.Run(spirvBinaryPath);
      blackboard.Add(Key, disassembly);
      return StepResult.Success;
    }
  }
}
