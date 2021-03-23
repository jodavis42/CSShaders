using System.Collections.Generic;

namespace CSShadersTests
{
  public enum StepResult
  {
    Success, Failed
  }

  public abstract class Step
  {
    public abstract StepResult Run(Blackboard blackboard);
  }

  public class Pipeline
  {
    public List<Step> Steps = new List<Step>();

    public StepResult Run(Blackboard blackboard)
    {
      StepResult result = StepResult.Success;
      foreach(var step in Steps)
      {
        if (step.Run(blackboard) == StepResult.Failed)
          result = StepResult.Failed;
      }
      return result;
    }
  }
}
