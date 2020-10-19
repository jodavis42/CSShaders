using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class ShaderModule : List<ShaderLibrary>
  {
    public ShaderType FindType(TypeKey key, bool checkDependencies = true)
    {
      foreach(var library in this)
      {
        var shaderType = library.FindType(key, checkDependencies);
        if (shaderType != null)
          return shaderType;
      }
      return null;
    }

    public ShaderOp FindConstant(ConstantOpKey key, bool checkDependencies = true)
    {
      foreach (var library in this)
      {
        var result = library.FindConstant(key, checkDependencies);
        if (result != null)
          return result;
      }
      return null;
    }

    public ShaderFunction FindFunction(FunctionKey key, bool checkDependencies = true)
    {
      foreach (var library in this)
      {
        var result = library.FindFunction(key, checkDependencies);
        if (result != null)
          return result;
      }
      return null;
    }
  }
}
