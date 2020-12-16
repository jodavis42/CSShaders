using System.Text;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// Helper class to build code. This contains simple helpers for managing scope, indendation and a few helpers for common operations.
  /// </summary>
  public class CodeWriter
  {
    public StringBuilder Builder = new StringBuilder();
    public int IndentationLevel = 0;

    public void Write(string str)
    {
      Builder.Append(str);
    }

    public void WriteLine(string str)
    {
      Builder.AppendLine(str);
    }

    public void WriteIndentedLine(string str)
    {
      WriteIndentation();
      Builder.AppendLine(str);
    }

    public void WriteLine()
    {
      Builder.AppendLine();
    }

    public void WriteIndentation()
    {
      for (var i = 0; i < IndentationLevel; ++i)
        Builder.Append("  ");
    }

    public void BeginScope()
    {
      WriteIndentation();
      Builder.Append("{\n");
      ++IndentationLevel;
    }

    public void EndScope()
    {
      --IndentationLevel;
      WriteIndentation();
      Builder.Append("}\n");
    }

    /// <summary>
    /// Declares a variable using the 'var VarName = initValue' syntax.
    /// </summary>
    public void DeclareVariable(string varName, string initialValue)
    {
      Write("var ");
      Write(varName);

      if (!string.IsNullOrEmpty(initialValue))
      {
        Write(" = ");
        Write(initialValue);
      }
      WriteLine(";");
    }

    /// <summary>
    /// Declares a variable using the 'TypeName VarName = initValue' syntax. This is required on member variables
    /// </summary>
    public void DeclareVariable(TypeName typeName, string varName, string initialValue)
    {
      Write(typeName.ToString());
      Write(" ");
      Write(varName);

      if (!string.IsNullOrEmpty(initialValue))
      {
        Write(" = ");
        Write(initialValue);
      }
      WriteLine(";");
    }

    /// <summary>
    /// Declares a member variable with any additional qualifiers.
    /// </summary>
    /// <param name="qualifiers">Any qualifiers to this function, such as public, static, etc...</param>
    /// <param name="typeName"></param>
    /// <param name="varName"></param>
    /// <param name="initialValue"></param>
    public void DeclareMemberVariable(string qualifiers, TypeName typeName, string varName, string initialValue)
    {
      Write(qualifiers);
      Write(" ");
      DeclareVariable(typeName, varName, initialValue);
    }

    public override string ToString()
    {
      return Builder.ToString();
    }
  }
}
