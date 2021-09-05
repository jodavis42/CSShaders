using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// A project of files for after being generated during shader compositing.
  public class GeneratedProject
  {
    public List<GenerateShaderFile> Files = new List<GenerateShaderFile>();

    public ShaderProject BuildShaderProject()
    {
      var project = new ShaderProject();
      foreach (var file in Files)
        project.AddCodeFromString(file.Code, file.FileName, file);
      return project;
    }
  }
}
