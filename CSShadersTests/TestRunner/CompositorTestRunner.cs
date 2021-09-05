using CSShaders;
using System.Collections.Generic;
using System.IO;

namespace CSShadersTests
{
  internal class CompositorTestRunner
  {
    internal class CompositeFragmentData
    {
      internal FragmentType FragmentType = FragmentType.None;
      internal string SpirVCrossStage;
    }

    public ILogger Logger = new ConsoleLogger();
    public SimpleShaderGenerator SimpleGenerator = new SimpleShaderGenerator();
    public string ArtifactsDir = null;
    public bool VisualDiff = false;

    public Pipeline CompositeGenerationPipeline;

    public CompositorTestRunner(ILogger logger, SimpleShaderGenerator generator, string artifactsDir, bool visualDiff)
    {
      Logger = logger;
      SimpleGenerator = generator;
      ArtifactsDir = artifactsDir;
      VisualDiff = visualDiff;
      CreateCompositorPipeline();
    }

    public bool RunTest(string path)
    {
      return TestComposite(path);
    }

    void CreateCompositorPipeline()
    {
      CompositeGenerationPipeline = new Pipeline();
      CompositeGenerationPipeline.Steps.Add(new CompositeConfigStep());
      CompositeGenerationPipeline.Steps.Add(new DirectoryFragmentCompositionStep() { OutShaderLibraryKey = CompositeConfigStep.FragmentLibraryKey });
      CompositeGenerationPipeline.Steps.Add(new LoadCompositeDefStep());
      CompositeGenerationPipeline.Steps.Add(new CompositeGenerationStep { ShaderLibraryKey = CompositeConfigStep.FragmentLibraryKey, CompositeDefKey = LoadCompositeDefStep.DefaultCompositeDefKey, CompositeLibraryKey = CompositeConfigStep.CompositeLibraryKey });
      CompositeGenerationPipeline.Steps.Add(new CompositeDiffStep { CompositeDirectoryKey = CompositeConfigStep.CompositeLibraryKey, VisualDisplay = VisualDiff, DiffsGeneratedKey = CompositeConfigStep.TestFailedKey});
      CompositeGenerationPipeline.Steps.Add(new CompositeCompilationStep { LibraryOutKey = CompositeConfigStep.ShaderLibraryKey });
      CompositeGenerationPipeline.Steps.Add(new ShaderLibraryBinaryWriterStep { LibraryKey = CompositeConfigStep.ShaderLibraryKey, SpvPathKey = CompositeConfigStep.CompositeSpvBinaryPathKey });

      CompositeGenerationPipeline.Steps.Add(new SpirVValidatorStep() { SpvPathKey = CompositeConfigStep.CompositeSpvBinaryPathKey });
      CompositeGenerationPipeline.Steps.Add(new SpirVDisassemblerStep { SpvPathKey = CompositeConfigStep.CompositeSpvBinaryPathKey });
      CompositeGenerationPipeline.Steps.Add(new ValidatorDisassemblerMergerStep());
      CompositeGenerationPipeline.Steps.Add(new FileWriterStep(ValidatorDisassemblerMergerStep.DefaultKey, CompositeConfigStep.ActualCompositeDisassemblyPathKey));
      CompositeGenerationPipeline.Steps.Add(new ArtifactListFilePathAppendStep { FilePathKey = CompositeConfigStep.ActualCompositeDisassemblyPathKey });
    }

    CSShaders.Compositing.CompositorSettings BuildTestCompositorSettings()
    {
      var coreDependencies = SimpleGenerator.CoreDependencies;
      return CSShaders.Compositing.TestSettings.BuildTestCompositorSettings(coreDependencies);
    }

    bool TestComposite(string path)
    {
      var defPath = Path.Combine(path, "ShaderDefinition.json");

      var testData = new CompositeTestData();
      testData.ScriptFilePath = path;
      testData.CompositeSpvBinaryFilePath = Path.Combine(path, "Composite.spv");
      testData.CompositeDisassemblerPaths.GeneratedFilePath = Path.Combine(path, "Composite.generated.spvtxt");
      testData.CompositeDisassemblerPaths.ExpectedFilePath = Path.Combine(path, "Composite.expected.spvtxt");
      testData.CompositeDisassemblerPaths.DiffFilePath = Path.Combine(path, "Composite.spvtxt.diff");
      testData.CompositeGlslPaths.GeneratedFilePath = Path.Combine(path, "Composite.generated.glsl");
      testData.CompositeGlslPaths.ExpectedFilePath = Path.Combine(path, "Composite.expected.glsl");
      testData.CompositeGlslPaths.DiffFilePath = Path.Combine(path, "Composite.glsl.diff");

      var blackboard = new Blackboard();
      blackboard.Add("Logger", Logger);
      blackboard.Add(DirectoryFragmentCompositionStep.DefaultFragmentDirectoryKey, path);
      blackboard.Add(CompositeGenerationStep.DefaultCompositorSettingsKey, BuildTestCompositorSettings());
      blackboard.Add(CompositeDiffStep.DefaultCompositeDirectoryKey, path);
      blackboard.Add(LoadCompositeDefStep.DefaultDefinitionFilePathKey, defPath);
      blackboard.Add(LoadCompositeDefStep.DefaultGeneratorKey, SimpleGenerator);
      if (!string.IsNullOrEmpty(ArtifactsDir))
        blackboard.Add(CopyIfFileExistsStep.DefaultTargetPathKey, ArtifactsDir);
      blackboard.Add(CopyIfFileExistsStep.DefaultRootPathKey, Directory.GetCurrentDirectory());
      blackboard.Add(testData);
      blackboard.Add(CompositeConfigStep.CompositeLibraryKey, new CSShaders.Compositing.CompositeLibrary());
      blackboard.Add<ArtifactsList>(new ArtifactsList());

      bool success = true;
      var status = CompositeGenerationPipeline.Run(blackboard);
      if (status == StepResult.Failed)
        success = false;
      success &= TestCompositeResult(path, blackboard);

      var copyArtifactsStep = new CopyArtifactsStep { Key = TestConfigStep.TestFailedKey };
      copyArtifactsStep.Run(blackboard);

      success &= !blackboard.GetValue<bool>(CompositeConfigStep.TestFailedKey);
      return success;
    }

    // The current pipeline framework doesn't allow sub pipelines or pipelines generating new steps.
    // Composites need to run tests over each stage generated so this is the extra work to do that.
    bool TestCompositeResult(string path, Blackboard blackboard)
    {
      var fragDataMap = new Dictionary<FragmentType, CompositeFragmentData>
      {
        {FragmentType.Vertex, new CompositeFragmentData { FragmentType = FragmentType.Vertex, SpirVCrossStage = "vert"} },
        {FragmentType.Pixel, new CompositeFragmentData { FragmentType = FragmentType.Pixel, SpirVCrossStage = "frag"} },
      };

      var pipeline = new Pipeline();
      pipeline.Steps.Add(new SpirVCrossStep() { SpvPathKey = CompositeConfigStep.CompositeSpvBinaryPathKey, OutPathKey = CompositeConfigStep.ActualCompositeGlslPathKey});
      pipeline.Steps.Add(new FileDiffStep(CompositeConfigStep.ExpectedCompositeGlslPathKey, CompositeConfigStep.ActualCompositeGlslPathKey) { Key = CompositeConfigStep.DiffCompositeGlslPathKey, VisualDisplay = VisualDiff, DiffsGeneratedKey = CompositeConfigStep.TestFailedKey });

      bool success = true;
      var generatedShaderLibrary = blackboard.Get<CSShaders.ShaderLibrary>(CompositeConfigStep.ShaderLibraryKey);
      foreach (var shaderType in generatedShaderLibrary.mTypeMap.Values)
      {
        if (shaderType.mFragmentType == CSShaders.FragmentType.None)
          continue;

        var fragData = fragDataMap.GetValueOrDefault(shaderType.mFragmentType, null);
        if (fragData == null)
          continue;

        var fragString = shaderType.mFragmentType.ToString();
        var baseOutFilePath = Path.Combine(path, $"Composite_{fragString}");
        blackboard.DefaultAddMode = Blackboard.AddMode.Override;
        blackboard.Add(CompositeConfigStep.ActualCompositeGlslPathKey, baseOutFilePath + ".generated.glsl", Blackboard.AddMode.Override);
        blackboard.Add(CompositeConfigStep.ExpectedCompositeGlslPathKey, baseOutFilePath + ".expected.glsl", Blackboard.AddMode.Override);
        blackboard.Add(CompositeConfigStep.DiffCompositeGlslPathKey, baseOutFilePath + ".glsl.diff", Blackboard.AddMode.Override);
        blackboard.Add(SpirVCrossStep.DefaultStageKey, fragData.SpirVCrossStage);

        var result = pipeline.Run(blackboard);
        if(result == StepResult.Failed)
          success = false;
      }
      return success;
    }
  }
}
