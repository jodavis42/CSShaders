namespace CSShaders
{
  public class ShaderEntryPointInfo
  {
    public FragmentType mStageType = FragmentType.None;
    public ShaderBlock mExecutionModesBlock = new ShaderBlock();
    public ShaderFunction mEntryPointFunction = new ShaderFunction();
    public ShaderBlock mGlobalVariablesBlock = new ShaderBlock();
    public ShaderFunction mGlobalsInitializationFunction;
  }
}
