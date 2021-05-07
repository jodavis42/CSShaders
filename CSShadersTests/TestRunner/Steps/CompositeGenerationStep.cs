namespace CSShadersTests
{
  class CompositeGenerationStep : Step
  {
    public const string DefaultCompositeDefKey = "CompositeDef";
    public string CompositeDefKey = DefaultCompositeDefKey;

    public const string DefaultShaderLibraryKey = "ShaderLibraryKey";
    public string ShaderLibraryKey = DefaultShaderLibraryKey;

    public const string DefaultCompositeLibraryKey = "CompositeLibraryKey";
    public string CompositeLibraryKey = DefaultCompositeLibraryKey;

    public const string DefaultGeneratedCompositeLibraryKey = "GeneratedCompositeLibraryKey";
    public string GeneratedCompositeLibraryKey = DefaultGeneratedCompositeLibraryKey;

    public const string DefaultCompositorSettingsKey = "CompositorSettings";
    public string CompositorSettingsKey = DefaultCompositorSettingsKey;

    public override StepResult Run(Blackboard blackboard)
    {
      var logger = blackboard.Get<ILogger>("Logger");

      var shaderLibrary = blackboard.Get<CSShaders.ShaderLibrary>(ShaderLibraryKey);
      if (shaderLibrary == null)
        return StepResult.Failed;

      var compositeDef = blackboard.Get<CSShaders.Compositing.CompositeDefinition>(CompositeDefKey);
      if (compositeDef == null)
        return StepResult.Failed;

      var compositeSettings = blackboard.Get<CSShaders.Compositing.CompositorSettings>(CompositorSettingsKey);
      if (compositeSettings == null)
        return StepResult.Failed;

      var compositeLibrary = blackboard.Get<CSShaders.Compositing.CompositeLibrary>(CompositeLibraryKey);
      if (compositeLibrary == null)
        return StepResult.Failed;

      var compositor = new CSShaders.Compositing.Compositor();
      compositor.AddDefinition(compositeDef);
      compositor.Generate(compositeSettings, shaderLibrary);

      blackboard.Add(GeneratedCompositeLibraryKey, compositor.GeneratedProject);

      return StepResult.Success;
    }
  }
}
