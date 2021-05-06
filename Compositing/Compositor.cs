using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Describes a stage after compositing.
  public class ShaderStageDescription
  {
    public string ClassName;
    public StageType StageType;
    public ShaderReflection Reflection;
  }

  // The definition of a composited shader.
  public class ShaderDefinition
  {
    public string ShaderName;
    public List<ShaderType> Fragments = new List<ShaderType>();
    public List<ShaderStageDescription> Stages = new List<ShaderStageDescription>();
  }

  /// Composes a shader from a collection of fragments. Fragments are linked together via
  /// input/output attributes to form a full shader. Many shaders can be composited together
  /// at once into one library. This is especially true as many different shaders can
  /// declare the same constant buffers for use.
  public class Compositor
  {
    CompositeDefinitionGenerator DefinitionGenerator = new CompositeDefinitionGenerator();
    CompositeSourceGenerator SourceGenerator = new CompositeSourceGenerator();

    List<CompositeDefinition> Composites = new List<CompositeDefinition>();
    CompositeLibrary CompositeLibrary;
    public GeneratedProject GeneratedProject;

    public Dictionary<string, ShaderDefinition> Results = new Dictionary<string, ShaderDefinition>();

    /// Adds a new definition to be composited later
    public void AddDefinition(CompositeDefinition compositeDefinition)
    {
      Composites.Add(compositeDefinition);
    }

    /// Generate all definitions at once into a composite library (a collection of source files).
    /// This library can be converted into a ShaderProject via BuildProject.
    public void Generate(CompositorSettings settings, ShaderLibrary sourceLibrary)
    {
      CompositeLibrary = new CompositeLibrary();

      // Generate the definitions of each composite shader
      var resultDefinitions = new Dictionary<CompositeDefinition, GeneratedCompositeDefinition>();
      foreach(var compositeDef in Composites)
        resultDefinitions.Add(compositeDef, DefinitionGenerator.CompositeRender(settings, compositeDef, sourceLibrary, CompositeLibrary));

      // Build all the source files for the library of composites
      GeneratedProject = SourceGenerator.Generate(CompositeLibrary);

      // Extract and combine the defs and results together
      foreach(var pair in resultDefinitions)
      {
        var compositeDef = pair.Key;
        var resultDef = pair.Value;

        var shaderDef = new ShaderDefinition();
        shaderDef.Fragments = compositeDef.Fragments;
        shaderDef.ShaderName = compositeDef.Name;
        foreach (var stage in resultDef.Stages)
        {
          // Skip the CPU and GPU stages
          if (stage.StageType.GetFragmentType() == FragmentType.None)
            continue;

          var stageDesc = new ShaderStageDescription()
          {
            ClassName = stage.ShortName,
            StageType = stage.StageType,
            Reflection = stage.Reflection,
          };
          shaderDef.Stages.Add(stageDesc);
        }
      }
    }

    public ShaderProject BuildProject()
    {
      return GeneratedProject.BuildShaderProject();
    }

    public void Clear()
    {
      Composites = new List<CompositeDefinition>();
      CompositeLibrary = null;
      GeneratedProject = null;
    }
  }
}
