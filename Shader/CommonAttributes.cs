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
    Input() { }
    Input(string name) { }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class Output : Attribute
  {
    Output() { }
    Output(string name) { }
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
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class SpecConstantInput : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class FragmentInput : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class StageInput : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class HardwareBuiltInInput : Attribute
  {
    public HardwareBuiltinType InputType;

    public HardwareBuiltInInput(HardwareBuiltinType inputType)
    {
      InputType = inputType;
    }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class StageOutput : Attribute
  {
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class HardwareBuiltInOutput : Attribute
  {
    public HardwareBuiltinType OutputType;

    public HardwareBuiltInOutput(HardwareBuiltinType outputType)
    {
      OutputType = outputType;
    }
  }

  [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class FragmentOutput : Attribute
  {
  }
}
