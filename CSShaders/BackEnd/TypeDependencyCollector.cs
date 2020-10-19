using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace CSShaders
{
  public class TypeDependencyCollector
  {
    ShaderLibrary mOwningLibrary;
    public TypeDependencyCollector(ShaderLibrary owningLibrary)
    {
      mOwningLibrary = owningLibrary;
    }

    public void VisitLibrary()
    {
      foreach(var type in mOwningLibrary.GetTypes())
      {
        Visit(type);
      }
    }


    public void Visit(IEnumerable<ShaderType> types)
    {
      foreach (var type in types)
        Visit(type);
    }
    public void Visit(ShaderType type)
    {
      if (mProcessedTypes.Contains(type))
        return;

      mProcessedTypes.Add(type);

      // Always visit value types before pointers
      if (type.mBaseType == OpType.Pointer)
        Visit(type.mTypeStorage.mDereferenceType);

      Visit(type.mParameters);
      Visit(type.mFields);
      mReferencedTypes.Add(type);
      Visit(type.mTypeStorage.mPointerType);
      
      Visit(type.mFunctions);
    }

    public void Visit(IEnumerable<ShaderField> fields)
    {
      foreach (var field in fields)
        Visit(field);
    }

    public void Visit(ShaderField field)
    {
      Visit(field.mType);
    }

    public void Visit(IEnumerable<ShaderFunction> functions)
    {
      foreach (var function in functions)
        Visit(function);
    }

    public void Visit(ShaderFunction function)
    {
      Visit(function.mResultType);
      mReferencedFunctions.Add(function);
    }

    public void Visit(IEnumerable<IShaderIR> irs)
    {
      foreach (var ir in irs)
        Visit(ir);
    }

    public void Visit(IShaderIR ir)
    {
      if (ir is ShaderType shaderType)
        Visit(shaderType);
      else if (ir is ShaderConstantLiteral constantLiteral)
          Visit(constantLiteral);
      else
        throw new Exception();
    }

    public void Visit(ShaderConstantLiteral constantLiteral)
    {
      Visit(constantLiteral.mType);
    }

    public void Visit(ShaderEntryPointInfo entryPointInfo)
    {
      mEntryPoints.Add(entryPointInfo);
      Visit(entryPointInfo.mEntryPointFunction);
    }

    // Need a separate set to prevent infinite recursion but to not control the added order
    HashSet<ShaderType> mProcessedTypes = new HashSet<ShaderType>();
    public OrderedSet<ShaderType> mReferencedTypes = new OrderedSet<ShaderType>();
    public OrderedSet<ShaderFunction> mReferencedFunctions = new OrderedSet<ShaderFunction>();
    public OrderedSet<ShaderEntryPointInfo> mEntryPoints = new OrderedSet<ShaderEntryPointInfo>();
  }
}
