using System.Collections.Generic;

namespace CSShaders
{
  public enum BlockType
  {
    // A simple block with no flow control change
    Direct,
    // A block belonging to an if statement
    Selection,
    // A block belonging to a loop instruction
    Loop
  }

  /// <summary>
  /// A block is a collection of instructions that also forms a lable. Most functions are composed of many blocks,
  /// deliniated when new scopes happen, primarily from conditions and loops.
  /// </summary>
  public class ShaderBlock : IShaderIR
  {
    public BlockType mBlockType = BlockType.Direct;
    /// The local variables declared in this block. Local variables have to be the first thing declared so separating this out makes things easier.
    public List<IShaderIR> mLocalVariables = new List<IShaderIR>();
    /// All instructions in this block.
    public List<IShaderIR> mOps = new List<IShaderIR>();
    /// If this is a non-direct flow control block, then the merge point is where direct control flow will resume to.
    public ShaderBlock mMergePoint = null;
    /// If this is a non-direct flow control block, then the continue point is where this block will jump to when finished.
    public ShaderBlock mContinuePoint = null;
    /// The last termination instruction in this block if it exists.
    public IShaderIR mTerminatorOp = null;
  }
}
