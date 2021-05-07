using CSShaders;
using System.IO;

namespace CSShadersTests
{
  class ShaderLibraryBinaryWriterStep : Step
  {
    public const string DefaultLibraryKey = "Library";
    public string LibraryKey = DefaultLibraryKey;

    public const string DefaultTranslatorKey = "Translator";
    public string TranslatorKey = DefaultTranslatorKey;

    public const string DefaultSpvPathKey = TestConfigStep.SpvBinaryPathKey;
    public string SpvPathKey = DefaultSpvPathKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");

      var shaderLibrary = blackboard.Get<ShaderLibrary>(LibraryKey);
      if (shaderLibrary == null)
      {
        logger.Log($"Missing ShaderLibrary key {LibraryKey}", ILogger.Verbosity.Error);
        return StepResult.Failed;
      }

      var translator = blackboard.Get<FrontEndTranslator>(TranslatorKey);
      var spvPath = blackboard.Get<string>(SpvPathKey);

      var writer = new BinaryWriter(new FileStream(spvPath, FileMode.Create));
      var spirvWriter = new SpirVStreamWriter(writer);
      var spirVBinaryBackend = new ShaderToSpirVBinary();
      spirVBinaryBackend.Write(spirvWriter, shaderLibrary, translator);
      writer.Close();

      var artifactsList = blackboard.Get<ArtifactsList>();
      artifactsList.Add(spvPath);

      return StepResult.Success;
    }
  }
}
