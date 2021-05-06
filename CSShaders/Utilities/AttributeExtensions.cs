namespace CSShaders
{
  public static class AttributeExtensions
  {
    public static Shader.StageInput ParseStageInput(ShaderAttribute attribute)
    {
      var parsedAttribute = new Shader.StageInput() { Location = -1, Component = -1 };
      // Parse from the C# attribute if it exists.
      if (attribute.Attribute != null)
      {
        foreach (var namedArg in attribute.Attribute.NamedArguments)
        {
          if (namedArg.Key == nameof(Shader.StageInput.Name))
            parsedAttribute.Name = namedArg.Value.ToString();
          else if (namedArg.Key == nameof(Shader.StageInput.Location))
            parsedAttribute.Location = (int)namedArg.Value.Value;
          else if (namedArg.Key == nameof(Shader.StageInput.Component))
            parsedAttribute.Location = (int)namedArg.Value.Value;
        }
      }
      // Also always check pre-parsed attributes
      foreach (var namedArg in attribute.Parameters)
      {
        if (namedArg.Name == nameof(Shader.StageInput.Name))
          parsedAttribute.Name = namedArg.Value.ToString();
        else if (namedArg.Name == nameof(Shader.StageInput.Location))
          parsedAttribute.Location = (int)namedArg.Value;
        else if (namedArg.Name == nameof(Shader.StageInput.Component))
          parsedAttribute.Location = (int)namedArg.Value;
      }

      return parsedAttribute;
    }

    public static Shader.StageOutput ParseStageOutput(ShaderAttribute attribute)
    {
      var parsedAttribute = new Shader.StageOutput() { Location = -1, Component = -1 };
      // Parse from the C# attribute if it exists.
      if (attribute.Attribute != null)
      {
        foreach (var namedArg in attribute.Attribute.NamedArguments)
        {
          if (namedArg.Key == nameof(Shader.StageInput.Name))
            parsedAttribute.Name = namedArg.Value.ToString();
          else if (namedArg.Key == nameof(Shader.StageOutput.Location))
            parsedAttribute.Location = (int)namedArg.Value.Value;
          else if (namedArg.Key == nameof(Shader.StageOutput.Component))
            parsedAttribute.Location = (int)namedArg.Value.Value;
        }
      }
      // Also always check pre-parsed attributes
      foreach (var namedArg in attribute.Parameters)
      {
        if (namedArg.Name == nameof(Shader.StageInput.Name))
          parsedAttribute.Name = namedArg.Value.ToString();
        else if (namedArg.Name == nameof(Shader.StageOutput.Location))
          parsedAttribute.Location = (int)namedArg.Value;
        else if (namedArg.Name == nameof(Shader.StageOutput.Component))
          parsedAttribute.Location = (int)namedArg.Value;
      }

      return parsedAttribute;
    }
  }
}
