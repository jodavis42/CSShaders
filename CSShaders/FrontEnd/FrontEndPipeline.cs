using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace CSShaders
{
  public class FrontEndPipeline
  {
    public List<FrontEndPass> mPasses = new List<FrontEndPass>()
    {
      // Collect all types
      new FrontEndPrimitiveTypeCollector(),
      new FrontEndTypeCollector(),
      // Visit Intrinsics separately (e.g. Vector2)
      
      new FrontEndPrimitiveDeclarationCollector(),
      // Collect all functions/vars definitions
      new FrontEndDeclarationCollector(),
      new FrontEndPreconstructorPass(),
      // Collect all function contents, var definitions
      new FrontEndDefinitionCollector()
    };

    public void Translate(FrontEndTranslator translator, CSharpCompilation compilation, List<SyntaxTree> trees)
    {
      FrontEndContext context = new FrontEndContext();
      foreach (var pass in mPasses)
      {
        pass.Visit(translator, compilation, trees, context);
      }

      // Validation passes
    }
  }
}
