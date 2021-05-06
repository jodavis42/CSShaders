using System;
using System.Collections.Generic;

namespace CSShaders.Compositing
{
  using UniformBindings = CSShaders.UniformBindings;
  public class UniformBufferField
  {
    public ShaderType FieldType { get; internal set; }
    public string FieldName { get; internal set; }
    public UniformBufferDescription Owner { get; internal set; }
  }

  /// The description of one uniform buffer. Fields are expected to be laid out in order.
  /// Currently no location bindings are allowed, the layout is auto determined from the size and order of the fields.
  public class UniformBufferDescription
  {
    /// Once locked, a buffer should not be changed. This allows caching and other optimizations.
    public bool Locked { get; private set; }

    internal string BufferName { get; private set; }

    internal UniformBindings Bindings = new UniformBindings();
    internal List<UniformBufferField> Fields = new List<UniformBufferField>();

    public UniformBufferDescription(string bufferName)
    {
      BufferName = bufferName;
    }

    public void SetBindings(UniformBindings bindings)
    {
      VerifyNotLocked();
      Bindings = bindings;
    }

    public void Add(UniformBufferField field)
    {
      VerifyNotLocked();
      if (field.Owner != null)
        throw new Exception("Cannot add field to multiple buffers");
      field.Owner = this;
      Fields.Add(field);
    }

    public void Add(ShaderType fieldType, string fieldName)
    {
      Add(new UniformBufferField { FieldType = fieldType, FieldName = fieldName });
    }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("Cannot add a field to a locked buffer");
    }
  }

  /// The collection of descriptions about uniform buffers that the user wants.
  /// These control how the [HardwareBuiltIn] attribute is grouped together into actual uniform buffers.
  public class UniformDescriptionSettings
  {
    /// Once locked, a buffer should not be changed. This allows caching and other optimizations.
    public bool Locked { get; private set; }
    public List<UniformBufferDescription> BufferDescriptions { get; private set; } = new List<UniformBufferDescription>();

    /// Create a new buffer with given C# name and the specified bindings
    public UniformBufferDescription CreateBuffer(string bufferName, UniformBindings bindings)
    {
      var existingBuffer = FindBuffer(bindings);
      if (existingBuffer != null)
        throw new Exception($"Buffer binding BindingId:{bindings.BindingId} DescriptorSet:{bindings.DescriptorSet} already exists. Cannot bind twice");

      var desc = new UniformBufferDescription(bufferName);
      desc.SetBindings(bindings);
      BufferDescriptions.Add(desc);
      return desc;
    }

    public UniformBufferDescription FindBuffer(UniformBindings bindings)
    {
      foreach(var bufferDescription in BufferDescriptions)
      {
        if (bufferDescription.Bindings == bindings)
          return bufferDescription;
      }
      return null;
    }

    public void Lock()
    {
      VerifyNotLocked();
      Locked = true;
      foreach (var buffer in BufferDescriptions)
        buffer.Lock();
    }

    void VerifyNotLocked()
    {
      if (Locked)
        throw new System.Exception("Cannot add a field to a locked description");
    }
  }
}
