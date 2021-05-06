namespace CSShaders.Compositing
{
  /// The source code for one shader file after composite generation. This file might be the source
  /// to a shader or it could be a file for some common shared data (such as a uniform buffer declaration).
  public class GenerateShaderFile
  {
    public string FileName;
    public string Code;
  }
}
