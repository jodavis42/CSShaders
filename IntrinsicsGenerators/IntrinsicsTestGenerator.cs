using System.Collections.Generic;
using System.IO;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// Generates very simple tests for each intrinsic. The test simply calls each function with the required parameters to make sure they translate correctly.
  /// </summary>
  public class IntrinsicsTestGenerator
  {
    delegate void AddTypeNameDelegate(TypeName typeName, HashSet<TypeName> variableSet, List<TypeName> variableList);
    Dictionary<TypeName, string> LocalVariableNamesByType = new Dictionary<TypeName, string>();
    Dictionary<TypeName, string> StaticVariableTypes = new Dictionary<TypeName, string>();

    public void Generate(List<IntrinsicGroup> intrinsicGroups)
    {
      // Put each group in its own folder
      foreach (var group in intrinsicGroups)
      {
        var dirName = string.Format("{0} Intrinsics", group.GroupName);
        Directory.CreateDirectory(dirName);
        foreach (var intrinsic in group.Intrinsics)
        {
          if (intrinsic.Disabled)
            continue;

          // If the test has a name use that, otherwise use the intrinsic name
          var testName = intrinsic.TestName != null ? intrinsic.TestName : intrinsic.IntrinsicName;
          var fileName = string.Format("{0}_IntrinsicTest.csshader", testName);
          var filePathName = Path.Combine(dirName, fileName);

          var testWriter = new CodeWriter();
          Generate(testWriter, intrinsic);
          File.WriteAllText(filePathName, testWriter.ToString());
        }
      }
    }

    public void Generate(CodeWriter writer, IntrinsicDescription intrinsic)
    {
      LocalVariableNamesByType.Clear();
      StaticVariableTypes.Clear();

      GenerateLocalAndStaticVariables(intrinsic);

      writer.WriteIndentedLine("[Shader.UnitTest][Shader.Pixel]");
      writer.WriteIndentation();
      writer.Write("struct ");
      writer.Write(intrinsic.IntrinsicName);
      writer.WriteLine("Test");
      writer.BeginScope();
      DeclareStaticVariables(writer, intrinsic);
      writer.WriteIndentedLine("[Shader.EntryPoint]");
      writer.WriteIndentedLine("public void Main()");
      writer.BeginScope();
      DeclareLocalVariables(writer, intrinsic);
      foreach (var signature in intrinsic.Signatures)
        Generate(writer, intrinsic, signature);
      writer.EndScope();
      writer.EndScope();
    }

    public void Generate(CodeWriter writer, IntrinsicDescription intrinsic, IntrinsicSignature signature)
    {
      writer.WriteIndentation();
      writer.Write(GetVariableName(signature.ReturnTypeName));
      writer.Write(" = Shader.Intrinsics.");
      writer.Write(intrinsic.FunctionName);
      writer.Write("(");
      for(var i = 0; i < signature.Parameters.Count; ++i)
      {
        var param = signature.Parameters[i];
        writer.Write(GetVariableName(param.TypeName));
        if (i != signature.Parameters.Count - 1)
          writer.Write(", ");
      }
      writer.WriteLine(");");
    }

    void GenerateLocalAndStaticVariables(IntrinsicDescription intrinsic)
    {
      var variableList = new List<TypeName>();
      var variableSet = new HashSet<TypeName>();
      AddTypeNameDelegate AddTypeName = (TypeName typeName, HashSet<TypeName> variableSet, List<TypeName> variableList) =>
      {
        if (variableSet.Contains(typeName))
          return;
        variableList.Add(typeName);
        variableSet.Add(typeName);
      };

      foreach (var signature in intrinsic.Signatures)
      {
        AddTypeName(signature.ReturnTypeName, variableSet, variableList);
        foreach (var param in signature.Parameters)
          AddTypeName(param.TypeName, variableSet, variableList);
      }

      foreach (var varType in variableList)
      {
        var varName = GenerateLocalVariableName(varType);
        if (varType.ForcedStatic)
          StaticVariableTypes.Add(varType, varName);
        else
          LocalVariableNamesByType.Add(varType, varName);
      }
    }

    void DeclareStaticVariables(CodeWriter writer, IntrinsicDescription intrinsic)
    {
      foreach (var pair in StaticVariableTypes)
      {
        writer.WriteIndentation();
        writer.DeclareMemberVariable("public static", pair.Key, pair.Value, null);
      }
    }

    void DeclareLocalVariables(CodeWriter writer, IntrinsicDescription intrinsic)
    {
      foreach(var pair in LocalVariableNamesByType)
      {
        writer.WriteIndentation();
        writer.DeclareVariable(pair.Value, GenerateDefaultValue(pair.Key));
      }
    }

    string GetVariableName(TypeName typeName)
    {
      var varName = LocalVariableNamesByType.GetValueOrDefault(typeName);
      if (string.IsNullOrEmpty(varName))
        varName = StaticVariableTypes.GetValueOrDefault(typeName);
      return varName;
    }

    string GenerateLocalVariableName(TypeName typeName)
    {
      var unqualifiedTypeName = GetUnqualifiedTypeName(typeName);
      if (typeName.ForcedStatic)
      {
        return unqualifiedTypeName.Substring(0, 1).ToUpper() + unqualifiedTypeName.Substring(1) + "Val";
      }

      return unqualifiedTypeName.Substring(0, 1).ToLower() + unqualifiedTypeName.Substring(1) + "Val";
    }

    string GetUnqualifiedTypeName(TypeName typeName)
    {
      var typeNameStr = typeName.ToString();
      var lastSeparatorIndex = typeNameStr.LastIndexOf(".");
      if (lastSeparatorIndex == -1)
        return typeNameStr;
      return typeNameStr.Substring(lastSeparatorIndex + 1);
    }

    string GenerateDefaultValue(TypeName typeName)
    {
      if (typeName.Name == "bool")
        return "false";
      if (typeName.Name == "int")
        return "0";
      if (typeName.Name == "float")
        return "0.0f";
      return string.Format("new {0}()", typeName.ToString());
    }
  }
}
