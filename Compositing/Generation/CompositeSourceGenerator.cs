using System;
using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// Generates the source code for a composite
  public class CompositeSourceGenerator
  {
    class Context
    {
      public ShaderCodeBuilder Builder = new ShaderCodeBuilder();
    }

    public GeneratedProject Generate(CompositeLibrary library)
    {
      var results = new GeneratedProject();
      foreach(var type in library.Types.Values)
      {
        var context = new Context();
        Generate(type, context);
        results.Files.Add(new GenerateShaderFile { FileName = type.ShortName, Code = context.Builder.ToString() });
      }

      return results;
    }

    void Generate(TypeDefinition type, Context context)
    {
      // If this is a stage definition then we have some extra generation to do
      if(type is StageDefinition stageDef)
      {
        Generate(stageDef, context);
        return;
      }

      // Otherwise just declare the type
      DeclareType(type, context);
    }

    bool Generate(StageDefinition stageDef, Context context)
    {
      // Skip the definition if it doesn't actually exist
      if (stageDef.TypeName == null || string.IsNullOrEmpty(stageDef.FullName))
        return false;

      var builder = context.Builder;

      // Write the fragment type attribute
      builder.AppendLine($"[Shader.{stageDef.StageType.GetFragmentType().ToString()}]");
      builder.AppendLine($"public struct {stageDef.ShortName}");
      builder.BeginScope();
      foreach(var fieldDef in stageDef.Fields)
      {
        builder.WriteIndentation();
        WriteFieldDefinition(fieldDef, context);
      }
      // Add a line after the fields to make it easier to read
      if(stageDef.Fields.Count != 0)
        builder.AppendLine("");

      // Declare the entry point function
      builder.AppendLineIndented($"[{TypeAliases.GetFullTypeName<Shader.EntryPoint>()}]");
      builder.AppendLineIndented("void Main()");
      builder.BeginScope();

      // Write out all of the fragment calls and links for the vertex attachment
      var vertexAttachment = stageDef.FindAttachment(StageAttachmentType.Vertex);
      foreach (var fragmentInstance in vertexAttachment.Variables)
      {
        // Declare the fragment instance
        builder.AppendLineIndented($"{fragmentInstance.Type.ShortName} {fragmentInstance.Name} = new {fragmentInstance.Type.ShortName}();");

        // Write out all the links for the instance (if it has any)
        var links = vertexAttachment.FindLinks(fragmentInstance);
        if(links != null)
          WriteLinks(links, context);

        // Call the main function now that the fragment is setup
        builder.AppendLineIndented($"{fragmentInstance.Name}.Main();");
        // Add a newline for readability
        builder.AppendLine("");
      }
      // Copy all links back to the stage output
      WriteLinks(vertexAttachment.StageLinks, context);

      builder.EndScope();
      builder.EndSemicolonScope();
      return true;
    }

    void DeclareType(TypeDefinition type, Context context)
    {
      var builder = context.Builder;
      WriteAttributes(type.Attributes, context);
      builder.AppendLine($"public struct {type.ShortName}");
      builder.BeginScope();
      foreach (var fieldDef in type.Fields)
      {
        builder.WriteIndentation();
        WriteFieldDefinition(fieldDef, context);
      }
      builder.EndSemicolonScope();
    }

    void WriteLinks(IEnumerable<VariableLink> links, Context context)
    {
      foreach (var link in links)
        WriteLink(link, context);
    }

    void WriteLink(VariableLink link, Context context)
    {
      WriteFieldAssignment(link.Source, link.Destination, context);
    }

    // A variable can be nested under many '.' of a local variable (e.g. var.Field.Field). This gets the fully scoped name.
    string GetFullVariablePath(VariableInstance variable)
    {
      var path = variable.Name;
      var owner = variable.Owner;
      // Walk up the owner chain, pre-pending the variable with the variable's instance name each time
      while (owner != null)
      {
        path = $"{owner.Name}.{path}";
        owner = owner.Owner;
      }
      return path;
    }

    void WriteFieldAssignment(VariableInstance source, VariableInstance dest, Context context)
    {
      var sourcePath = GetFullVariablePath(source);
      var destPath = GetFullVariablePath(dest);
      context.Builder.AppendLineIndented($"{destPath} = {sourcePath};");
    }

    void WriteFieldDefinition(FieldDefinition fieldDef, Context context)
    {
      var builder = context.Builder;
      WriteAttributes(fieldDef.Attributes, context);
      builder.AppendLine($" public {fieldDef.Type.FullName} {fieldDef.Name};");
    }

    void WriteAttributes(ShaderAttributes attributes, Context context)
    {
      foreach (var attribute in attributes)
        WriteAttribute(attribute, context);
    }

    void WriteAttribute(ShaderAttribute attribute, Context context)
    {
      var builder = context.Builder;
      builder.Append("[");
      builder.Append(attribute.Name.FullName);
      WriteAttributeParameters(attribute.Parameters, context);
      context.Builder.Append("]");
    }

    void WriteAttributeParameters(List<ShaderAttributeParameter> parameters, Context context)
    {
      var builder = context.Builder;
      if (parameters != null && parameters.Count != 0)
      {
        builder.Append("(");
        foreach (var param in parameters)
          WriteAttributeParameter(param, context);
        builder.Append(")");
      }
    }

    void WriteAttributeParameter(ShaderAttributeParameter param, Context context)
    {
      var builder = context.Builder;
      if (!string.IsNullOrEmpty(param.Name))
        builder.Append($"{param.Name} = ");

      var paramValue = param.Value.ToString();
      // If this is an enum, make sure to get the fully qualified name when declaring it
      // (e.g. emit `spv.BuiltIn.PerspectivePositon` not `PerspectivePosition`)
      if (param.Value is Enum enumValue)
      {
        var typeName = TypeAliases.GetFullTypeName(param.Value.GetType());
        paramValue = $"{typeName}.{paramValue}";
      }
      builder.Append(paramValue);
    }
  }
}
