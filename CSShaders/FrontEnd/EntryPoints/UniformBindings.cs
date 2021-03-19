using System;

namespace CSShaders
{
  public class UniformBindings
  {
    public int DescriptorSet = 0;
    public int BindingId = 0;

    public override int GetHashCode()
    {
      return Tuple.Create(DescriptorSet, BindingId).GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is UniformBindings uniformBindings)
        return uniformBindings.DescriptorSet.Equals(DescriptorSet) & uniformBindings.BindingId.Equals(BindingId);
      return false;
    }
  }
}
