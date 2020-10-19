using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;

namespace CSShaders
{
  class Program
  {
   
    static void Main(string[] args)
    {
      FrontEndTranslator frontEnd = new FrontEndTranslator();
      frontEnd.mCurrentLibrary = new ShaderLibrary();
      foreach (var shaderPath in Directory.EnumerateFiles("Shaders"))
      {
        if (Path.GetExtension(shaderPath) != ".csshader")
          continue;
        SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(shaderPath));
        var compilation = CSharpCompilation.Create("MyCompilation", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        
        var semanticModel = compilation.GetSemanticModel(tree);
        frontEnd.Translate(tree, semanticModel);
        var dissassembler = new ShaderToSimpleDisassembly();
        //var disassembly = dissassembler.DumpLibrary(frontEnd.mCurrentLibrary);
        //Console.WriteLine(disassembly);
        //var simpleOutPath = GenerateFilePath(shaderPath, ".dis");
        //File.WriteAllText(simpleOutPath, disassembly);

        var outPath = GenerateFilePath(shaderPath, ".spv");
        var writer = new BinaryWriter(new FileStream(outPath, FileMode.Create));
        
        var spirvWriter = new SpirVStreamWriter(writer);
        var spirVBinaryBackend = new ShaderToSpirVBinary();
        spirVBinaryBackend.Write(spirvWriter, frontEnd.mCurrentLibrary, frontEnd);
        writer.Close();
      }
    }

    static private string GenerateFilePath(string filePath, string newExtension, bool emitToTempDir = false)
    {
      if (emitToTempDir)
      {
        string newPath = Path.ChangeExtension(filePath, newExtension);
        return Path.Combine(Path.GetTempPath(), Path.GetFileName(newPath));
      }
      else
        return Path.ChangeExtension(filePath, newExtension);
    }
  }
}
