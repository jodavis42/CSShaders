using System;
using System.Collections.Generic;

namespace CSShadersTests
{
  public class Blackboard
  {
    public enum AddMode { Error, Override, Ignore };
    public AddMode DefaultAddMode = AddMode.Error;
    Dictionary<string, object> Objects = new Dictionary<string, object>();

    public bool Add(string name, object value, AddMode addMode)
    {
      bool alreadyExists = Objects.ContainsKey(name);
      if (alreadyExists)
      {
        if (addMode == AddMode.Error)
          throw new Exception($"key {name} already exists");
        else if (addMode == AddMode.Override)
          Objects[name] = value;
      }
      else
        Objects[name] = value;
      return alreadyExists;
    }

    public bool Add(string name, object value)
    {
      return Add(name, value, DefaultAddMode);
    }

    public bool Add<T>(T value, AddMode addMode)
    {
      return Add(typeof(T).Name, value, addMode);
    }

    public bool Add<T>(T value)
    {
      return Add(value, DefaultAddMode);
    }

    public object Get(string name)
    {
      return Objects.GetValueOrDefault(name, null);
    }

    public T Get<T>(string name) where T : class
    {
      return Get(name) as T;
    }

    public T Get<T>() where T : class
    {
      return Get(typeof(T).Name) as T;
    }
  }
}
