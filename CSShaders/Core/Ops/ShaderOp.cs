using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderOp : IShaderIR
  {
    public OpInstructionType mOpType = OpInstructionType.OpUndef;
    public List<IShaderIR> mParameters = new List<IShaderIR>();
    public ShaderType mResultType;
  }
}
