using CSShaders;
using System;
using System.IO;

namespace CSShadersTests
{
  class Program
  {
    static SimpleShaderGenerator SimpleGenerator = new SimpleShaderGenerator();

    static void Main(string[] args)
    {
      SimpleGenerator.ErrorHandlers.Add(OnTranslationErrorLog);
      //SimpleGenerator.ErrorHandlers.Add(OnTranslationErrorThrowException);
      LoadCoreLibraries();

      var path = "Tests";
      if (args.Length != 0)
        path = args[0];

      if (Directory.Exists(path))
        RunTests(path);
      else
        RunTest(path);
    }

    static void LoadCoreLibraries()
    {
      var path = Path.Combine("..", "CSShaders", "Libraries");
      SimpleGenerator.LoadDependencies(path);
    }
    
    static void RunTests(string path)
    {
      foreach(var file in Directory.EnumerateFiles(path))
      {
        RunTest(file);
      }

      foreach (var subDir in Directory.EnumerateDirectories(path))
      {
        RunTests(subDir);
      }
    }

    static void RunTest(string filePath)
    {
      if (!File.Exists(filePath))
        return;

      if (Path.GetExtension(filePath) != ".csshader")
        return;

      SimpleGenerator.ClearFragmentProject();
      SimpleGenerator.LoadFragmentFile(filePath);
      SimpleGenerator.CompileFragmentProject();


      var binaryOutPath = Path.ChangeExtension(filePath, ".generated.spv");
      var validatorOutPath = Path.ChangeExtension(filePath, ".generated.spvval");
      var generatedDisassemblerOutPath = Path.ChangeExtension(filePath, ".generated.spvtxt");
      var generatedGlslOutPath = Path.ChangeExtension(filePath, ".generated.glsl");
      var expectedDisassemblerOutPath = Path.ChangeExtension(filePath, ".expected.spvtxt");
      var expectedGlslOutPath = Path.ChangeExtension(filePath, ".expected.glsl");

      var writer = new BinaryWriter(new FileStream(binaryOutPath, FileMode.Create));
      var spirvWriter = new SpirVStreamWriter(writer);
      var spirVBinaryBackend = new ShaderToSpirVBinary();
      spirVBinaryBackend.Write(spirvWriter, SimpleGenerator.FragmentLibrary, SimpleGenerator.FrontEnd);
      writer.Close();

      var validatorTool = new SpirVValidatorTool();
      var validatorResults = validatorTool.Run(binaryOutPath);
      File.WriteAllText(validatorOutPath, validatorResults);
      
      var disassemblerTool = new SpirVDisassemblerTool();
      var disassembly = disassemblerTool.Run(binaryOutPath);
      if (validatorResults.Length != 0)
        disassembly = validatorResults + disassembly;
      File.WriteAllText(generatedDisassemblerOutPath, disassembly);

      var glslTool = new SpirVCrossTool();
      glslTool.Run(binaryOutPath, generatedGlslOutPath);

      VisualDiffTool diffTool = new VisualDiffTool();
      diffTool.VisualDisplay = true;
      diffTool.Diff(expectedDisassemblerOutPath, generatedDisassemblerOutPath);
      diffTool.Diff(expectedGlslOutPath, generatedGlslOutPath);
    }

    static void OnTranslationErrorLog(ShaderTranslationError error)
    {
      Console.WriteLine(string.Format("{0}: {1}", error.Location.ToString(), error.Message));
    }

    static void OnTranslationErrorThrowException(ShaderTranslationError error)
    {
      throw new Exception(string.Format("{0}: {1}", error.Location.ToString(), error.Message));
    }
  }
}
