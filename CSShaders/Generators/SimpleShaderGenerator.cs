using System;
using System.Collections.Generic;
using System.IO;

namespace CSShaders
{
  /// <summary>
  /// Simple wrapper around creating shaders and shader libraries with dependenices.
  /// </summary>
  public class SimpleShaderGenerator
  {
    public FrontEndTranslator FrontEnd = new FrontEndTranslator();
    public ShaderModule CoreDependencies = new ShaderModule();
    
    public ShaderProject FragmentProject;
    public ShaderLibrary FragmentLibrary;
    public List<ShaderProject.ErrorHandler> ErrorHandlers = new List<ShaderProject.ErrorHandler>();

    static void LoadProjectDirectory(string path, ShaderProject shaderProject, bool recursive = true)
    {
      var extensions = new HashSet<string>() { ".csshader" };
      shaderProject.LoadProjectDirectory(path, extensions, recursive);
    }

    static ShaderProject CreateProjectFromDirectory(string path, bool recursive = true)
    {
      var shaderProject = new ShaderProject();
      LoadProjectDirectory(path, shaderProject, recursive);
      return shaderProject;
    }

    public void LoadDependencies(string directory)
    {
      var core = new CoreShaderLibrary(this.FrontEnd);

      var utilitiesPath = Path.Combine(directory, "Utilities");
      var utilitiesModule = new ShaderModule() { core };
      ShaderProject shaderProject;
      var utilitiesLibrary = LoadAndCompileProject("Utilities", utilitiesPath, utilitiesModule, out shaderProject);

      this.CoreDependencies = new ShaderModule() { utilitiesLibrary };
    }

    public void LoadFragmentProject(string directory)
    {
      FragmentLibrary = LoadAndCompileProject("Fragments", directory, this.CoreDependencies, out this.FragmentProject);
    }

    public void LoadFragmentFile(string filePath)
    {
      FragmentProject.AddCodeFromFile(filePath, null);
    }

    public void CompileFragmentProject()
    {
      this.FragmentLibrary = this.FragmentProject.CompileAndTranslate("Fragments", this.CoreDependencies, this.FrontEnd);
    }

    public void ClearFragmentProject()
    {
      this.FragmentProject = new ShaderProject();
      this.FragmentLibrary = new ShaderLibrary();
      this.FragmentProject.ErrorHandlers.Add(this.OnTranslationError);
    }

    ShaderLibrary LoadAndCompileProject(string projectName, string directory, ShaderModule dependencies, out ShaderProject shaderProject)
    {
      shaderProject = CreateProjectFromDirectory(directory);
      shaderProject.ErrorHandlers.Add(this.OnTranslationError);

      var shaderLibrary = shaderProject.CompileAndTranslate(projectName, dependencies, this.FrontEnd);
      return shaderLibrary;
    }

    void OnTranslationError(ShaderTranslationError error)
    {
      foreach (var errorHandler in ErrorHandlers)
        errorHandler(error);
    }
  }
}
