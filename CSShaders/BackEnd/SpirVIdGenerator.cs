using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class SpirvIdGenerator
  {
    UInt32 mId = 1;
    Dictionary<IShaderIR, UInt32> mIdMap = new Dictionary<IShaderIR, UInt32>();

    public int Count { get { return mIdMap.Count; } }
    public UInt32 GetId(IShaderIR ir)
    {
      if (!mIdMap.ContainsKey(ir))
      {
        mIdMap.Add(ir, mId);
        ++mId;
      }
      return mIdMap[ir];
    }

    public void GenerateIds(ShaderLibrary library, TypeDependencyCollector typeCollector)
    {
      foreach (var type in typeCollector.mReferencedTypes)
      {
        GetId(type);
        if (type.StorageClassCollection != null)
          GetId(type.FindPointerType(StorageClass.Function));
        foreach (var field in type.mFields)
          GetId(field.mType);
      }
      foreach (var constantOp in typeCollector.mReferencedConstants)
      {
        GenerateIds(constantOp);
      }
      foreach (var staticOp in typeCollector.mReferencedStatics)
      {
        GenerateIds(staticOp);
      }
      foreach (var function in typeCollector.mReferencedFunctions)
      {
        GenerateIds(function);
      }
    }

    void GenerateIds(ShaderFunction function)
    {
      GetId(function);
      GenerateIds(function.mParametersBlock);
      foreach (var block in function.mBlocks)
        GenerateIds(block);
    }

    void GenerateIds(ShaderBlock block)
    {
      GetId(block);
      foreach (var op in block.mLocalVariables)
        GenerateIds(op);
      foreach (var op in block.mOps)
        GenerateIds(op);
    }

    void GenerateIds(IShaderIR ir)
    {
      var op = ir as ShaderOp;
      if (op != null)
        GenerateIds(op);
    }

    void GenerateIds(ShaderOp op)
    {
      if (op.mResultType != null)
        GetId(op.mResultType);
      GetId(op);
      foreach (var arg in op.mParameters)
        GenerateIds(arg);
    }
  }
}
