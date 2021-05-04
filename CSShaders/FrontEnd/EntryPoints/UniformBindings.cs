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

    public bool Equals(UniformBindings rhs)
    {
      if (rhs is null)
        return false;
      return rhs.DescriptorSet.Equals(DescriptorSet) & rhs.BindingId.Equals(BindingId);
    }
    public override bool Equals(object obj) => this.Equals(obj as UniformBindings);

    public static bool operator ==(UniformBindings lhs, UniformBindings rhs)
    {
      if (lhs is null)
      {
        if (rhs is null)
          return true;
        return false;
      }
      return lhs.Equals(rhs);
    }

    public static bool operator !=(UniformBindings lhs, UniformBindings rhs) => !(lhs == rhs);
  }
}
