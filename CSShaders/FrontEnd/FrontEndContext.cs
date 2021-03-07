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
    public ShaderType mCurrentType;
    public ShaderFunction mCurrentFunction;
    public ShaderBlock mCurrentBlock;

    public Stack<IShaderIR> mOpStack = new Stack<IShaderIR>();
    public Stack<MergePointScope> MergePointStack = new Stack<MergePointScope>();
    public Dictionary<ISymbol, IShaderIR> mSymbolToIRMap = new Dictionary<ISymbol, IShaderIR>();
    public IShaderIR mThisOp = null;

    public void Push(IShaderIR op)
    {
      mOpStack.Push(op);
    }
    public IShaderIR Pop()
    {
      return mOpStack.Pop();
    }

    public void PushMergePoint(ShaderBlock continueBlock, ShaderBlock mergeBlock)
    {
      MergePointStack.Push(new MergePointScope { ContinueBlock = continueBlock, MergeBlock = mergeBlock });
    }

    public void PopMergePoint()
    {
      MergePointStack.Pop();
    }
  }
}
