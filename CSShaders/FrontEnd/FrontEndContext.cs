using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CSShaders
{
  public class FrontEndContext
  {
    public class MergePointScope
    {
      public ShaderBlock ContinueBlock;
      public ShaderBlock MergeBlock;
    }

    public FrontEndTranslator mFrontEnd;
    public FrontEndPass mCurrentPass;
    public ShaderType mCurrentType;
    public ShaderFunction mCurrentFunction;
    public ShaderBlock mCurrentBlock;

    public Stack<IShaderIR> mOpStack = new Stack<IShaderIR>();
    List<MergePointScope> MergePointStack = new List<MergePointScope>();
    public Dictionary<ISymbol, IShaderIR> mSymbolToIRMap = new Dictionary<ISymbol, IShaderIR>();
    public IShaderIR mThisOp = null;

    public void Push(IShaderIR op)
    {
      mOpStack.Push(op);
    }
    public IShaderIR Pop()
    {
      if (mOpStack.Count == 0)
        return null;
      return mOpStack.Pop();
    }

    public void PushMergePoint(ShaderBlock continueBlock, ShaderBlock mergeBlock)
    {
      MergePointStack.Add(new MergePointScope { ContinueBlock = continueBlock, MergeBlock = mergeBlock });
    }

    public ShaderBlock GetActiveMergeBlock()
    {
      for(var i = 0; i < MergePointStack.Count; ++i)
      {
        var index = MergePointStack.Count - 1 - i;
        if (MergePointStack[index].MergeBlock != null)
          return MergePointStack[index].MergeBlock;
      }
      return null;
    }

    public ShaderBlock GetActiveContinueBlock()
    {
      for (var i = 0; i < MergePointStack.Count; ++i)
      {
        var index = MergePointStack.Count - 1 - i;
        if (MergePointStack[index].ContinueBlock != null)
          return MergePointStack[index].ContinueBlock;
      }
      return null;
    }

    public void PopMergePoint()
    {
      MergePointStack.RemoveAt(MergePointStack.Count - 1);
    }
  }
}
