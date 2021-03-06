using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Interface for a uniform buffer. This is used to help declare uniform buffers.
  /// </summary>
  public class UniformBufferInterface
  {
    public string TypeName;
    public string InstanceName;
    public ShaderInterfaceSet Fields = new ShaderInterfaceSet();
    // The instance for the uniform buffer.
    public ShaderOp InterfaceInstance = null;

    public int DescriptorSet = 0;
    public int BindingId = 0;
  }

  /// <summary>
  /// A set of uniform buffers that are keyed off the descriptorSet and bindingId.
  /// </summary>
  public class UniformBufferSet
  {
    public Dictionary<Tuple<int, int>, UniformBufferInterface> Buffers = new Dictionary<Tuple<int, int>, UniformBufferInterface>();

    public UniformBufferInterface GetOrCreate(int descriptorSet, int bindingId)
    {
      var tuple = new Tuple<int, int>(descriptorSet, bindingId);
      var buffer = Buffers.GetValueOrDefault(tuple);
      // The buffer already exists, return it
      if (buffer != null)
        return buffer;

      // Create a new buffer
      buffer = new UniformBufferInterface();
      buffer.DescriptorSet = descriptorSet;
      buffer.BindingId = bindingId;
      Buffers.Add(tuple, buffer);
      return buffer;
    }

    public int Count { get { return Buffers.Count; } }
  }
}
