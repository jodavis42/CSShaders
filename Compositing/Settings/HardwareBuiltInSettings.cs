using System.Collections.Generic;

namespace CSShaders.Compositing
{
  public class HardwareBuiltInField
  {
    public ShaderType FieldType { get; internal set; }
    public string FieldName { get; internal set; }
    public Shader.HardwareBuiltinType BuiltInType { get; internal set; }
    public HardwareBuiltInDescriptions Owner { get; internal set; }
  }

  /// The description of a hardware builtin per stage.
  /// EVer field specifies a Name/Type pair to register to a built-in in spirv. No type validation is currently performed.
  public class HardwareBuiltInDescriptions
  {
    /// Once locked, a buffer should not be changed. This allows caching and other optimizations.
    public bool Locked { get; private set; }
    public List<HardwareBuiltInField> Inputs { get; } = new List<HardwareBuiltInField>();
    public List<HardwareBuiltInField> Outputs { get; } = new List<HardwareBuiltInField>();

    public void AddInput(ShaderType fieldType, string fieldName, Shader.HardwareBuiltinType builtInType)
    {
      Add(Inputs, new HardwareBuiltInField { FieldType = fieldType, FieldName = fieldName, BuiltInType = builtInType });
    }

    public void AddOutput(ShaderType fieldType, string fieldName, Shader.HardwareBuiltinType builtInType)
    {
      Add(Outputs, new HardwareBuiltInField { FieldType = fieldType, FieldName = fieldName, BuiltInType = builtInType });
    }

    private void Add(List<HardwareBuiltInField> fields, HardwareBuiltInField field)
    {
      VerifyNotLocked();
      field.Owner = this;
      fields.Add(field);
    }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("Cannot add a field to a locked description");
    }
  }

  public class HardwareBuiltInSettings
  {
    /// Once locked, a buffer should not be changed. This allows caching and other optimizations.
    public bool Locked { get; private set; }
    Dictionary<FragmentType, HardwareBuiltInDescriptions> StageDescriptions { get; } = new Dictionary<FragmentType, HardwareBuiltInDescriptions>();

    public HardwareBuiltInDescriptions GetFragmentDesciptions(FragmentType fragmentType)
    {
      return StageDescriptions.GetValueOrDefault(fragmentType);
    }

    public HardwareBuiltInDescriptions GetOrCreateFragmentDesciptions(FragmentType fragmentType)
    {
      VerifyNotLocked();

      var result = GetFragmentDesciptions(fragmentType);
      if (result == null)
      {
        result = new HardwareBuiltInDescriptions();
        StageDescriptions.Add(fragmentType, result);
      }
      return result;
    }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
      foreach (var pair in StageDescriptions)
        pair.Value.Lock();
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("Cannot add a field to a locked description");
    }
  }
}
