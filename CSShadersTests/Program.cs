using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using CSShaders;

namespace CSShadersTests
{
  class Program
  {
    static ShaderModule mDependencies;
    static FrontEndTranslator mFrontEnd = new FrontEndTranslator();

    static void Main(string[] args)
    {
      mDependencies = new ShaderModule();
      mDependencies.Add(new CoreShaderLibrary(mFrontEnd));
      
      RunTests("Tests");
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
      if (Path.GetExtension(filePath) != ".csshader")
        return;

      SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(filePath));
      var compilation = CSharpCompilation.Create("MyCompilation", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
      var semanticModel = compilation.GetSemanticModel(tree);

      FrontEndTranslator frontEnd = new FrontEndTranslator();
      frontEnd.mCurrentLibrary = new ShaderLibrary();
      frontEnd.mCurrentLibrary.mDependencies = mDependencies;
      frontEnd.Translate(tree, semanticModel);

      var binaryOutPath = Path.ChangeExtension(filePath, ".generated.spv");
      var validatorOutPath = Path.ChangeExtension(filePath, ".generated.spvval");
      var generatedDisassemblerOutPath = Path.ChangeExtension(filePath, ".generated.spvtxt");
      var generatedGlslOutPath = Path.ChangeExtension(filePath, ".generated.glsl");
      var expectedDisassemblerOutPath = Path.ChangeExtension(filePath, ".expected.spvtxt");
      var expectedGlslOutPath = Path.ChangeExtension(filePath, ".expected.glsl");

      var writer = new BinaryWriter(new FileStream(binaryOutPath, FileMode.Create));
      var spirvWriter = new SpirVStreamWriter(writer);
      var spirVBinaryBackend = new ShaderToSpirVBinary();
      spirVBinaryBackend.Write(spirvWriter, frontEnd.mCurrentLibrary, frontEnd);
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

      TortoiseDiffTool diffTool = new TortoiseDiffTool();
      diffTool.mVisualDisplay = true;
      diffTool.Diff(expectedDisassemblerOutPath, generatedDisassemblerOutPath);
      diffTool.Diff(expectedGlslOutPath, generatedGlslOutPath);
    }
  }
}
