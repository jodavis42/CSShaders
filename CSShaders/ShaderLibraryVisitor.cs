using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class ShaderLibraryVisitor
  {
    public virtual void Visit(ShaderLibrary library)
    {
      foreach(var type in library.GetTypes())
        Visit(type);
    }

    public virtual void Visit(ShaderType shaderType)
    {
      foreach (var function in shaderType.mFunctions)
        Visit(function);
    }

    public virtual void Visit(ShaderFunction shaderFunction)
    {
      Visit(shaderFunction.mParametersBlock);
      foreach (var block in shaderFunction.mBlocks)
        Visit(block);
    }
    
    public virtual void Visit(ShaderBlock shaderBlock)
    {
      foreach (var ir in shaderBlock.mOps)
        Visit(ir);
    }

    public virtual void Visit(IShaderIR shaderIR)
    {

    }

    public virtual void Visit<T>(List<T> args)
    {
      foreach (var ir in args)
        Visit(ir);
    }

    public virtual void Visit<T>(T args)
    {
      
    }
  }
  public class ShaderIdGenerator : ShaderLibraryVisitor
  {
    UInt32 mId = 1;
    Dictionary<IShaderIR, UInt32> mIdMap = new Dictionary<IShaderIR, UInt32>();
    public override void Visit(ShaderType shaderType)
    {
      GetId(shaderType);
      base.Visit(shaderType);
    }

    public override void Visit(ShaderFunction shaderFunction)
    {
      GetId(shaderFunction);
      base.Visit(shaderFunction);
    }

    public override void Visit(ShaderBlock shaderblock)
    {
      GetId(shaderblock);
      base.Visit(shaderblock);
    }

    public override void Visit(IShaderIR shaderIR)
    {
      GetId(shaderIR);
      base.Visit(shaderIR);
    }

    UInt32 GetId(IShaderIR ir)
    {
      if (!mIdMap.ContainsKey(ir))
      {
        mIdMap.Add(ir, mId);
        ++mId;
      }
      return mIdMap[ir];
    }
  }
}
