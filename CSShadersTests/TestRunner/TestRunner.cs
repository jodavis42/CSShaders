﻿using CSShaders;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSShadersTests
{
  public class TestRunner
  {
    public bool Success = true;
    public ILogger Logger = new ConsoleLogger();
    public SimpleShaderGenerator SimpleGenerator = new SimpleShaderGenerator();
    public Pipeline BasicPipeline;
    public string ArtifactsDir = null;
    public bool VisualDiff = false;
    
    public bool Run(string path)
    {
      Success = true;

      CreateBasicPipeline();
      SimpleGenerator.ErrorHandlers.Add(OnTranslationErrorLog);
      //SimpleGenerator.ErrorHandlers.Add(OnTranslationErrorThrowException);
      LoadCoreLibraries();

      if (Directory.Exists(path))
        RunTests(path);
      else
        RunTest(path);

      return Success;
    }

    void CreateBasicPipeline()
    {
      BasicPipeline = new Pipeline();
      BasicPipeline.Steps.Add(new TestConfigStep());
      BasicPipeline.Steps.Add(new SingleFragmentCompilationStep());
      BasicPipeline.Steps.Add(new FragmentLibraryWriterStep());
      BasicPipeline.Steps.Add(new SpirVValidatorStep() { SpvPathKey = TestConfigStep.SpvBinaryPathKey });
      BasicPipeline.Steps.Add(new SpirVDisassemblerStep() { SpvPathKey = TestConfigStep.SpvBinaryPathKey });
      BasicPipeline.Steps.Add(new ValidatorDisassemblerMergerStep());
      BasicPipeline.Steps.Add(new FileWriterStep(ValidatorDisassemblerMergerStep.DefaultKey, TestConfigStep.ActualDisassemblyPathKey));
      BasicPipeline.Steps.Add(new SpirVCrossStep() { SpvPathKey = TestConfigStep.SpvBinaryPathKey, OutPathKey = TestConfigStep.ActualGlslPathKey });
      BasicPipeline.Steps.Add(new FileDiffStep(TestConfigStep.ExpectedDisassemblyPathKey, TestConfigStep.ActualDisassemblyPathKey) { Key = TestConfigStep.DiffDisassemblyKey, VisualDisplay = VisualDiff });
      BasicPipeline.Steps.Add(new FileWriterStep(TestConfigStep.DiffDisassemblyKey, TestConfigStep.DiffDisassemblyPathKey));
      BasicPipeline.Steps.Add(new FileDiffStep(TestConfigStep.ExpectedGlslPathKey, TestConfigStep.ActualGlslPathKey) { Key = TestConfigStep.DiffGlslKey, VisualDisplay = VisualDiff });
      BasicPipeline.Steps.Add(new FileWriterStep(TestConfigStep.DiffGlslKey, TestConfigStep.DiffGlslPathKey));
      if(!string.IsNullOrEmpty(ArtifactsDir))
      {
        BasicPipeline.Steps.Add(new CopyIfFileExistsStep
        {
          ConditionalFileKey = TestConfigStep.DiffDisassemblyPathKey,
          FilePathKeysToCopy = new List<string>
        {
          TestConfigStep.DiffDisassemblyPathKey,
          TestConfigStep.ExpectedDisassemblyPathKey,
          TestConfigStep.ActualDisassemblyPathKey
        }
        });
        BasicPipeline.Steps.Add(new CopyIfFileExistsStep
        {
          ConditionalFileKey = TestConfigStep.DiffGlslPathKey,
          FilePathKeysToCopy = new List<string>
        {
          TestConfigStep.DiffGlslPathKey,
          TestConfigStep.ExpectedGlslPathKey,
          TestConfigStep.ActualGlslPathKey
        }
        });
      }
    }

    void LoadCoreLibraries()
    {
      var path = Path.Combine("..", "CSShaders", "Libraries");
      SimpleGenerator.LoadDependencies(path);
    }

    void RunTests(string path)
    {
      foreach (var file in Directory.EnumerateFiles(path))
      {
        RunTest(file);
      }

      foreach (var subDir in Directory.EnumerateDirectories(path))
      {
        RunTests(subDir);
      }
    }

    void RunTest(string filePath)
    {
      if (!File.Exists(filePath))
        return;

      if (Path.GetExtension(filePath) != ".csshader")
        return;

      Success &= RunShaderTest(filePath);
    }

    bool RunShaderTest(string filePath)
    {
      var testData = new TestData();
      testData.ScriptFilePath = filePath;
      testData.SpvBinaryFilePath = Path.ChangeExtension(filePath, ".generated.spv");
      testData.DisassemblerTest.ExpectedFilePath = Path.ChangeExtension(filePath, ".expected.spvtxt");
      testData.DisassemblerTest.GeneratedFilePath = Path.ChangeExtension(filePath, ".generated.spvtxt");
      testData.DisassemblerTest.DiffFilePath = Path.ChangeExtension(filePath, ".spvtxt.diff");
      testData.GlslTest.ExpectedFilePath = Path.ChangeExtension(filePath, ".expected.glsl");
      testData.GlslTest.GeneratedFilePath = Path.ChangeExtension(filePath, ".generated.glsl");
      testData.GlslTest.DiffFilePath = Path.ChangeExtension(filePath, ".glsl.diff");

      var blackboard = new Blackboard();
      blackboard.Add("Logger", Logger);
      blackboard.Add(testData);
      blackboard.Add(FragmentLibraryWriterStep.DefaultGeneratorKey, SimpleGenerator);
      if(!string.IsNullOrEmpty(ArtifactsDir))
        blackboard.Add(CopyIfFileExistsStep.DefaultTargetPathKey, ArtifactsDir);
      blackboard.Add(CopyIfFileExistsStep.DefaultRootPathKey, Directory.GetCurrentDirectory());

      var status = BasicPipeline.Run(blackboard);
      if (status == StepResult.Failed || File.Exists(testData.DisassemblerTest.DiffFilePath) || File.Exists(testData.GlslTest.DiffFilePath))
        return false;
      return true;
    }

    void OnTranslationErrorLog(ShaderTranslationError error)
    {
      Logger.Log(string.Format("{0}: {1}", error.Location.ToString(), error.Message));
    }

    void OnTranslationErrorThrowException(ShaderTranslationError error)
    {
      throw new Exception(string.Format("{0}: {1}", error.Location.ToString(), error.Message));
    }
  }
}