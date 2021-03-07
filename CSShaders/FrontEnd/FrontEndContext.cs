using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CSShaders
{
  public class FrontEndContext
  {
    public FrontEndTranslator mFrontEnd;
    public ShaderType mCurrentType;
    public ShaderFunction mCurrentFunction;
    public ShaderBlock mCurrentBlock;

    public Stack<IShaderIR> mOpStack = new Stack<IShaderIR>();
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
  }
}
