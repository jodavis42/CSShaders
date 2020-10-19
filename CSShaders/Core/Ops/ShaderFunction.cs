using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// A function on a type. The function arguments are stored as parameters to the function.
  /// </summary>
  public class ShaderFunction : ShaderOp
  {
    /// The meta information about this function from the original source language.
    public ShaderFunctionMeta mMeta = new ShaderFunctionMeta();
    /// The block of instructions containing all parameters declared in this function.
    /// Some languages (SpirV) require parameters to be declared up front. Putting them in this block makes that easier.
    public ShaderBlock mParametersBlock = new ShaderBlock();
    /// All blocks of code contained in this function.
    public List<ShaderBlock> mBlocks = new List<ShaderBlock>();

    public ShaderFunction()
    {
      mOpType = OpInstructionType.OpFunction;
    }

    public bool IsStatic()
    {
      return false;
    }

    public ShaderType GetFunctionType()
    {
      return mResultType;
    }
  }
}
