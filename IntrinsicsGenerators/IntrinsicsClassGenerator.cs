using System.Collections.Generic;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// Generates the Intrinsics class for use in the shader library.
  /// </summary>
  public class IntrinsicsClassGenerator
  {
    public CodeWriter Writer = new CodeWriter();
    public void Initialize()
    {
      Writer.WriteLine("namespace Shader");
      Writer.BeginScope();
      Writer.WriteIndentation();
      Writer.WriteLine("public struct Intrinsics");
      Writer.BeginScope();
    }

    public void Finish()
    {
      Writer.EndScope();
      Writer.EndScope();
    }

    public void Generate(List<IntrinsicGroup> groups)
    {
      foreach (var group in groups)
        Generate(group);
    }

    public void Generate(IntrinsicGroup group)
    {
      Writer.WriteIndentation();
      Writer.WriteLine(group.GroupComment);
      foreach (var intrinsic in group.Intrinsics)
        Generate(intrinsic);
    }

    public void Generate(IntrinsicDescription intrinsic)
    {
      foreach (var signature in intrinsic.Signatures)
        Generate(intrinsic, signature);
      Writer.WriteLine();
    }

    public void Generate(IntrinsicDescription intrinsic, IntrinsicSignature signature)
    {
      foreach(var comment in intrinsic.Comments)
      {
        Writer.WriteIndentation();
        Writer.Write("// ");
        Writer.WriteLine(comment);
      }

      Writer.WriteIndentation();
      // If this intrinsic is disabled, comment it out
      if (intrinsic.Disabled)
        Writer.Write("//");

      if (!string.IsNullOrEmpty(intrinsic.Attributes))
      {
        Writer.Write(intrinsic.Attributes);
        Writer.Write(" ");
      }

      Writer.Write("public extern static ");
      Writer.Write(signature.ReturnTypeName.ToString());
      Writer.Write(" ");
      Writer.Write(intrinsic.FunctionName);
      Writer.Write("(");
      
      for (var i = 0; i < signature.Parameters.Count; ++i)
      {
        var param = signature.Parameters[i];
        Writer.Write(param.GetDeclaration());

        if (i != signature.Parameters.Count - 1)
          Writer.Write(", ");
      }
      Writer.Write(");\n");
    }

    public override string ToString()
    {
      return Writer.ToString();
    }
  }
}
