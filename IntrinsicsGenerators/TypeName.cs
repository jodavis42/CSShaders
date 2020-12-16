namespace IntrinsicsGenerators
{
  /// <summary>
  /// Represents the name of a type. Used to help proper function overloading so types are distinct types compared to names.
  /// </summary>
  public class TypeName
  {
    /// <summary>
    /// The name of this type.
    /// </summary>
    public string Name;
    /// <summary>
    /// Currently, some types are forced as statics (images). This is used so generation knows what types are special
    /// </summary>
    public bool ForcedStatic = false;

    public TypeName(string typeName, bool forcedStatic = false)
    {
      Name = typeName;
      ForcedStatic = forcedStatic;
    }

    public override string ToString()
    {
      return Name;
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is TypeName typeName)
        return Name == typeName.Name;
      return false;
    }
  }
}
