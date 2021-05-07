using CSShaders;
using System.IO;

namespace CSShadersTests
{
  class FragmentLibraryWriterStep : Step
  {
    public const string DefaultGeneratorKey = "Generator";
    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public string GeneratorKey = DefaultGeneratorKey;
    public string SpvPathKey = DefaultSpvPathKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var generator = blackboard.Get<SimpleShaderGenerator>(GeneratorKey);
      var spvPath = blackboard.Get<string>(SpvPathKey);

      var writer = new BinaryWriter(new FileStream(spvPath, FileMode.Create));
      var spirvWriter = new SpirVStreamWriter(writer);
      var spirVBinaryBackend = new ShaderToSpirVBinary();
      spirVBinaryBackend.Write(spirvWriter, generator.FragmentLibrary, generator.FrontEnd);
      writer.Close();

      var artifactList = blackboard.Get<ArtifactsList>();
      artifactList.Add(spvPath);

      return StepResult.Success;
    }
  }
}
