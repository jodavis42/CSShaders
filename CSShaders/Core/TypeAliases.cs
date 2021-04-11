using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  public static class TypeAliases
  {
    static Dictionary<Type, TypeName> Aliases = new Dictionary<Type, TypeName>
    {
      { typeof(void), new TypeName{ Name = "void" } },
      { typeof(bool), new TypeName{ Name = "bool" } },
      { typeof(int), new TypeName{ Name = "int" } },
      { typeof(uint), new TypeName{ Name = "uint" } },
      { typeof(float), new TypeName{ Name = "float" } },
      { typeof(double), new TypeName{ Name = "double" } },
      { typeof(string), new TypeName{ Name = "string" } },
    };
    static Dictionary<string, TypeName> NameAliases = new Dictionary<string, TypeName>
    {
      { typeof(void).Name, new TypeName{ Name = "void" } },
      { typeof(bool).Name, new TypeName{ Name = "bool" } },
      { typeof(int).Name, new TypeName{ Name = "int" } },
      { typeof(uint).Name, new TypeName{ Name = "uint" } },
      { typeof(float).Name, new TypeName{ Name = "float" } },
      { typeof(double).Name, new TypeName{ Name = "double" } },
      { typeof(string).Name, new TypeName{ Name = "string" } },
    };

    public static TypeName GetTypeName(Type type)
    {
      var result = Aliases.GetValueOrDefault(type);
      if (result != null)
        return result;

      return new TypeName { Name = type.Name, Namespace = type.Namespace };
    }

    public static TypeName GetTypeName<T>()
    {
      return GetTypeName(typeof(T));
    }

    public static string GetShortTypeName(Type type)
    {
      return GetTypeName(type).Name;
    }

    public static string GetFullTypeName(Type type)
    {
      return GetTypeName(type).FullName;
    }

    public static string GetShortTypeName<T>()
    {
      return GetTypeName<T>().Name;
    }

    public static string GetFullTypeName<T>()
    {
      return GetTypeName<T>().FullName;
    }

    public static TypeName GetTypeName(ISymbol symbol)
    {
      var result = NameAliases.GetValueOrDefault(symbol.Name);
      if (result != null)
        return result;

      return new TypeName { Name = symbol.Name, Namespace = symbol.ContainingNamespace.ToString() };
    }

    public static string GetFullTypeName(ISymbol symbol)
    {
      return GetTypeName(symbol).FullName;
    }
  }
}
