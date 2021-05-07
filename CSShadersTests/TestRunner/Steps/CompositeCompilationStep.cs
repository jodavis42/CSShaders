using CSShaders;

namespace CSShadersTests
{
  class CompositeCompilationStep : Step
  {
    public const string DefaultGeneratedCompositeKey = "GeneratedComposite";
    public string GeneratedCompositeKey = DefaultGeneratedCompositeKey;

    public const string DefaultGeneratorKey = "Generator";
    public string GeneratorKey = DefaultGeneratorKey;

    public const string DefaultGeneratedCompositeLibraryKey = "GeneratedCompositeLibraryKey";
    public string GeneratedCompositeLibraryKey = DefaultGeneratedCompositeLibraryKey;

    public const string DefaultLibraryOutKey = "Library";
    public string LibraryOutKey = DefaultLibraryOutKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");
      var generator = blackboard.Get<SimpleShaderGenerator>(GeneratorKey);
      var generatedCompositeProject = blackboard.Get<CSShaders.Compositing.GeneratedProject>(GeneratedCompositeLibraryKey);
      if (generatedCompositeProject == null)
        return StepResult.Failed;

      generator.ClearShaderProject();
      foreach (var generatedShader in generatedCompositeProject.Files)
      {
        generator.ShaderProject.AddCodeFromString(generatedShader.Code, generatedShader.FileName, generatedShader.FileName);
      }
      generator.CompileShaderProject();
      if (generator.ShaderLibrary == null)
        logger.Log("Composite failed to compile", ILogger.Verbosity.Error);

      blackboard.Add(LibraryOutKey, generator.ShaderLibrary, Blackboard.AddMode.Override);

      return StepResult.Success;
    }
  }
}
