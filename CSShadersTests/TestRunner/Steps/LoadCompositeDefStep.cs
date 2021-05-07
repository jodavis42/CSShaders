using CSShaders;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CSShadersTests
{
  public class CompositeTestDefinition
  {
    public string Name { get; set; }
    public List<string> Fragments { get; set; } = new List<string>();
  }

  class LoadCompositeDefStep : Step
  {
    public const string DefaultDefinitionFilePathKey = "CompositeDefFilePath";
    public string DefinitionFilePathKey = DefaultDefinitionFilePathKey;

    public const string DefaultCompositeDefKey = "CompositeDef";
    public string CompositeDefKey = DefaultCompositeDefKey;

    public const string DefaultGeneratorKey = "Generator";
    public string GeneratorKey = DefaultGeneratorKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var generator = blackboard.Get<SimpleShaderGenerator>(GeneratorKey);
      var definitionFilePath = blackboard.Get<string>(DefinitionFilePathKey);
      if (!File.Exists(definitionFilePath))
        return StepResult.Failed;

      var compositeTestDefText = File.ReadAllText(definitionFilePath);
      var compositeTestDef = JsonSerializer.Deserialize<CompositeTestDefinition>(compositeTestDefText);
      if (compositeTestDef == null)
        return StepResult.Failed;

      var compositeDef = new CSShaders.Compositing.CompositeDefinition();
      compositeDef.Name = compositeTestDef.Name;
      foreach (var fragmentName in compositeTestDef.Fragments)
      {
        var fragmentType = generator.FragmentLibrary.FindType(new TypeKey(fragmentName));
        compositeDef.Fragments.Add(fragmentType);
      }

      var artifactsList = blackboard.Get<ArtifactsList>();
      artifactsList.Add(definitionFilePath);

      blackboard.Add(CompositeDefKey, compositeDef);

      return StepResult.Success;
    }
  }
}
