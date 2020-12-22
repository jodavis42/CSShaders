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
      WriteComments(intrinsic, signature);

      Writer.WriteIndentation();
      // If this intrinsic is disabled, comment it out
      if (intrinsic.Disabled)
        Writer.Write("//");

      WriteAttributes(intrinsic, signature);

      Writer.Write(signature.Qualifiers);
      Writer.Write(" ");
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

    void WriteComments(IntrinsicDescription intrinsic, IntrinsicSignature signature)
    {
      List<string> comments = null;
      if (signature.Comments != null)
        comments = signature.Comments;
      else
        comments = intrinsic.Comments;

      if(comments != null)
      {
        foreach (var comment in comments)
        {
          Writer.WriteIndentation();
          Writer.Write("// ");
          Writer.WriteLine(comment);
        }
      }
    }

    void WriteAttributes(IntrinsicDescription intrinsic, IntrinsicSignature signature)
    {
      string attributes = null;
      if (signature.Attributes != null)
        attributes = signature.Attributes.GetAttributes();
      else if (intrinsic.Attributes != null)
        attributes = intrinsic.Attributes.GetAttributes();

      if (!string.IsNullOrEmpty(attributes))
      {
        Writer.Write(attributes);
        Writer.Write(" ");
      }
    }

    public override string ToString()
    {
      return Writer.ToString();
    }
  }
}
