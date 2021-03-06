using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  /// <summary>
  /// A field for a shader's interface. Used to aid in entry point generation.
  /// </summary>
  public class ShaderInterfaceField
  {
    // The field to create an interface parameter for.
    public ShaderField ShaderField;

    public delegate ShaderOp InstanceAccessDelegate(FrontEndTranslator translator, ShaderInterfaceField interfaceField, FrontEndContext context);
    // A delegate to get the instance to this field. Fields can be declared in several different ways,
    // including grouped in a struct or as separate fields. This helps to abstract this from the user.
    public InstanceAccessDelegate GetInstance = null;
    // If this is a built-in, this is the type for the built in
    public Spv.BuiltIn BuiltInType;
  }

  /// <summary>
  /// A set of interface fields. This could be an input, output, uniform. Sets might for a struct or be floating fields, depending on the shader stage's requirements.
  /// </summary>
  public class ShaderInterfaceSet
  {
    public String Name;
    public List<ShaderInterfaceField> Fields = new List<ShaderInterfaceField>();

    public void Add(ShaderInterfaceField interfaceField)
    {
      Fields.Add(interfaceField);
    }

    public IEnumerator<ShaderInterfaceField> GetEnumerator()
    {
      return Fields.GetEnumerator();
    }

    public ShaderOp GetFieldInstance(FrontEndTranslator translator, ShaderInterfaceField interfaceField, FrontEndContext context)
    {
      if (interfaceField.GetInstance == null)
        return null;
      return interfaceField.GetInstance(translator, interfaceField, context);
    }

    public int Count { get { return Fields.Count; } }
  }
}
