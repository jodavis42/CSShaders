using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// The definition of a composite after being generated. This is the collection of all the
  /// information needed for every stage of a shader for how to generate the actual code for the final shader.
  public class GeneratedCompositeDefinition
  {
    public string Name;

    public List<StageDefinition> Stages { get; } = new List<StageDefinition>();

    public GeneratedCompositeDefinition()
    {
      for (var stageType = StageType.Start; stageType < StageType.Count; ++stageType)
      {
        Stages.Add(new StageDefinition(stageType));
      }
    }

    public StageDefinition FindStage(StageType stageType)
    {
      if (stageType < StageType.Start || stageType >= StageType.Count)
        return null;
      return Stages[(int)stageType];
    }
  }
}
