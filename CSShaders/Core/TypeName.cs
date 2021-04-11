using Microsoft.CodeAnalysis;
using System;

namespace CSShaders
{
  public class TypeName
  {
    public string Namespace = "";
    public string Name = "";
    public string FullName => string.IsNullOrEmpty(Namespace) ? Name : $"{Namespace}.{Name}";

    public override string ToString()
    {
      throw new Exception();
    }
    public bool Equals(TypeName rhs)
    {
      if (rhs is null)
        return false;

      return this.Name == rhs.Name && this.Namespace == rhs.Namespace;
    }
    public override bool Equals(object obj) => this.Equals(obj as TypeName);

    public override int GetHashCode() => HashCode.Combine(Namespace, Name);

    public static bool operator ==(TypeName lhs, TypeName rhs)
    {
      if (lhs is null)
      { 
        if (rhs is null)
          return true;
        return false;
      }

      // Equals handles case of null on right side.
      return lhs.Equals(rhs);
    }

    public static bool operator !=(TypeName lhs, TypeName rhs) => !(lhs == rhs);

    public TypeName Clone()
    {
      return new TypeName { Name = this.Name, Namespace = this.Namespace };
    }

    public TypeName CloneAsAppended(string newName)
    {
      return new TypeName { Name = this.Name + newName, Namespace = this.Namespace };
    }
  }
}
