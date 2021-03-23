namespace CSShadersTests
{
  class ValidatorDisassemblerMergerStep : Step
  {
    public const string DefaultKey = "ValidatorAndDisassembly";
    public string DisassemblyKey = SpirVDisassemblerStep.DefaultKey;
    public string ValidatorKey = SpirVValidatorStep.DefaultKey;
    public string ResultsKey = DefaultKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var disassemblyResults = blackboard.Get<string>(DisassemblyKey);
      if (disassemblyResults == null)
      {
        logger.Log($"Failed to find disassembly key ${DisassemblyKey}");
        return StepResult.Failed;
      }
      var validatorResults = blackboard.Get<string>(ValidatorKey);
      if (validatorResults == null)
      {
        logger.Log($"Failed to find validator key ${ValidatorKey}");
        return StepResult.Failed;
      }

      string mergedResults = validatorResults + disassemblyResults;
      blackboard.Add(ResultsKey, mergedResults, Blackboard.AddMode.Override);
      return StepResult.Success;
    }
  }
}
