using System;

namespace Shader
{
  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
  public class UnitTest : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Method)]
  public class EntryPoint : Attribute
  {
  }

  public class IntrinsicFunction : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Method)]
  public class SimpleIntrinsicFunction : Attribute
  {
    public SimpleIntrinsicFunction(String opName) { }
  }

  [System.AttributeUsage(AttributeTargets.Method)]
  public class SimpleExtensionIntrinsic : Attribute
  {
    public string ExtensionName;
    public string IntrinsicName;

    public SimpleExtensionIntrinsic(string extensionName, string intrinsicName)
    {
      IntrinsicName = intrinsicName;
    }
  }

  /// <summary>
  /// Marks a type as requiring a certain shader capability (e.g. 16-bit floats).
  /// This is currently required by the user as opposed to being automatically added.
  /// </summary>
  [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  public class RequiresCapability : Attribute
  {
    public Capabilities Capability;

    public RequiresCapability(Capabilities capability)
    {
      Capability = capability;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
  public class Vertex : Attribute
  {
  }

  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
  public class Pixel : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class Input : Attribute
  {
    string Name;
    public Input() { }
    public Input(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class Output : Attribute
  {
    string Name;
    public Output() { }
    public Output(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class UniformBuffer : Attribute
  {
    public int DescriptorSet = 0;
    public int BindingId = 0;
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class UniformBufferField : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class UniformConstant : Attribute
  {
    public int DescriptorSet = 0;
    public int BindingId = 0;
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class AppBuiltInInput : Attribute
  {
    string Name;
    public AppBuiltInInput() { }
    public AppBuiltInInput(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class SpecConstantInput : Attribute
  {
    string Name;
    public SpecConstantInput() { }
    public SpecConstantInput(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class FragmentInput : Attribute
  {
    string Name;
    public FragmentInput() { }
    public FragmentInput(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class StageInput : Attribute
  {
    public string Name;
    public int Location = -1;
    public int Component = -1;

    public StageInput() { }
    public StageInput(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class PropertyInput : Attribute
  {
    public string PropertyName;
    public PropertyInput() { }
    public PropertyInput(string propertyName)
    {
      PropertyName = propertyName;
    }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class HardwareBuiltInInput : Attribute
  {
    public HardwareBuiltinType InputType;

    public HardwareBuiltInInput()
    {
    }

    public HardwareBuiltInInput(HardwareBuiltinType inputType)
    {
      InputType = inputType;
    }

    public HardwareBuiltInInput(string inputName)
    {
    }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class StageOutput : Attribute
  {
    public string Name;
    public int Location = -1;
    public int Component = -1;

    public StageOutput() { }
    public StageOutput(string name) { Name = name; }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class HardwareBuiltInOutput : Attribute
  {
    public HardwareBuiltinType OutputType;

    public HardwareBuiltInOutput()
    {
    }

    public HardwareBuiltInOutput(HardwareBuiltinType outputType)
    {
      OutputType = outputType;
    }

    public HardwareBuiltInOutput(string inputName)
    {
    }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class FragmentOutput : Attribute
  {
    string Name;
    public FragmentOutput() { }
    public FragmentOutput(string name) { Name = name; }
  }
}
