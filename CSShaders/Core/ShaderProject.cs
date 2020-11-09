using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;

namespace CSShaders
{
  /// <summary>
  /// A project is a collection of code entries that can be compiled into a library.
  /// </summary>
  public class ShaderProject : ShaderCompilationErrors
  {
    public class CodeEntry
    {
      public string Code;
      public string Location;
      public object UserData;
    }
    public List<CodeEntry> CodeEntries = new List<CodeEntry>();

    public void AddCodeFromString(string code, string location, object userData)
    {
      CodeEntries.Add(new CodeEntry { Code = code, Location = location, UserData = userData });
    }

    public bool AddCodeFromFile(string location, object userData)
    {
      if (!File.Exists(location))
        return false;

      var text = File.ReadAllText(location);
      AddCodeFromString(text, location, userData);
      return true;
    }

    public void Clear()
    {
      CodeEntries.Clear();
    }

    public ShaderLibrary CompileAndTranslate(ShaderModule dependencies, FrontEndTranslator translator)
    {
      var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, false);
      var trees = BuildSyntaxTrees();
      var references = BuildReferences(dependencies);
      var compilation = CSharpCompilation.Create("MyCompilation", trees, references, options);
      CheckDiagnostics(compilation);

      FrontEndTranslator frontEnd = new FrontEndTranslator();
      translator.mCurrentLibrary = new ShaderLibrary();
      translator.mCurrentLibrary.SourceCompilation = compilation;
      translator.mCurrentLibrary.mDependencies = dependencies;
      foreach(var tree in trees)
      {
        var semanticModel = compilation.GetSemanticModel(tree);
        translator.Translate(tree, semanticModel);
      }

      return translator.mCurrentLibrary;
    }

    private List<SyntaxTree> BuildSyntaxTrees()
    {
      var trees = new List<SyntaxTree>();
      foreach(var entry in CodeEntries)
      {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(entry.Code);
        trees.Add(tree);
      }
      return trees;
    }

    private List<MetadataReference> BuildReferences(ShaderModule dependencies)
    {
      // Walk all references of this library, making sure to not visit any dependency more than once
      var references = new List<MetadataReference>();
      var visitedReferences = new HashSet<ShaderLibrary>();
      var stack = new Stack<ShaderLibrary>();

      // Always forcibly add C#'s core library
      var coreLibrary = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
      references.Add(coreLibrary);

      // Get the first level of dependencies
      if (dependencies != null)
      {
        foreach (var dependency in dependencies)
          stack.Push(dependency);
      }

      // Visit every dependency if we haven't already
      while(stack.Count != 0)
      {
        var library = stack.Pop();
        if (visitedReferences.Contains(library))
          continue;

        // Get the C# reference if it exists
        if(library.SourceCompilation != null)
          references.Add(library.SourceCompilation.ToMetadataReference());

        // Add all sub-dependencies
        if(library.mDependencies != null)
        {
          foreach (var subDependency in library.mDependencies)
          {
            if (visitedReferences.Contains(library))
              continue;
            stack.Push(subDependency);
          }
        }
      }
      return references;
    }

    private void CheckDiagnostics(CSharpCompilation compilation)
    {
      var diagnostics = compilation.GetDiagnostics();
      if (diagnostics.IsEmpty)
        return;

      var ignoredErrors = new HashSet<string>();
      ignoredErrors.Add("CS0573");// Field initializers in structs
      foreach (var diagnostic in diagnostics)
      {
        if(diagnostic.Severity == DiagnosticSeverity.Error && !ignoredErrors.Contains(diagnostic.Id))
        {
          SendTranslationError(new ShaderCodeLocation(diagnostic.Location), diagnostic.ToString());
        }
      }
    }
  }
}
