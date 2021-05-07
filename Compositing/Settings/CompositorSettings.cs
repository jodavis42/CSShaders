namespace CSShaders.Compositing
{
  /// A collection of settings for the compositor to use when linking together fragments and generating code.
  public class CompositorSettings
  {
    public string ResultNamespace;
    public UniformDescriptionSettings UniformSettings = new UniformDescriptionSettings();
    public HardwareBuiltInSettings HardwareBuiltInSettings = new HardwareBuiltInSettings();
    public AttributeSettings AttributeSettings = new AttributeSettings();
    public RenderTargetSettings RenderTargetSettings = new RenderTargetSettings();

    public bool Locked { get; private set; }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
      UniformSettings.Lock();
      HardwareBuiltInSettings.Lock();
      AttributeSettings.Lock();
      RenderTargetSettings.Lock();
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("CompositorSettings settings are locked");
    }
  }
}
