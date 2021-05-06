using System.Text;

namespace CSShaders
{
  /// Simple helper class to build CS Shader code
  public class ShaderCodeBuilder
  {
    StringBuilder Builder = new StringBuilder();
    int IndentationLevel = 0;

    public void WriteIndentation()
    {
      for (var i = 0; i < IndentationLevel; ++i)
        Append("  ");
    }

    public void Append(string text)
    {
      Builder.Append(text);
    }

    public void AppendIndented(string text)
    {
      WriteIndentation();
      Append(text);
    }

    public void AppendLine(string text)
    {
      Builder.AppendLine(text);
    }

    public void AppendLineIndented(string text)
    {
      WriteIndentation();
      AppendLine(text);
    }

    public void BeginScope()
    {
      AppendLineIndented("{");
      ++IndentationLevel;
    }

    public void EndScope()
    {
      --IndentationLevel;
      AppendLineIndented("}");
    }

    public void EndSemicolonScope()
    {
      --IndentationLevel;
      AppendLineIndented("};");
    }

    public override string ToString()
    {
      return Builder.ToString();
    }
  }
}
