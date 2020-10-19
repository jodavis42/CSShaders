using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  class ShaderToSimpleDisassembly
  {
    //StringBuilder mBuilder = new StringBuilder();
    //Dictionary<IShaderIR, int> mIdMap = new Dictionary<IShaderIR, int>();

    //public string DumpLibrary(ShaderLibrary library)
    //{
    //  var builder = new StringBuilder();
    //  mBuilder = builder;

    //  foreach (var type in library.mTypeNameMap.Values)
    //  {
    //    builder.AppendFormat("%{0} = {1}", GetId(type), type.mBaseType.ToString());
    //    builder.AppendLine();
    //    if (type.mTypeStorage != null)
    //    {
    //      builder.AppendFormat("%{0} = {1} %{2}", GetId(type.mTypeStorage.mPointerType), type.mTypeStorage.mPointerType.mBaseType.ToString(), GetId(type));
    //      builder.AppendLine();
    //    }
    //  }
    //  foreach(var constantLiteral in library.mConstantLiterals.Values)
    //  {
    //    builder.AppendFormat("%{0} = %{1} %{2}", GetId(constantLiteral), GetId(constantLiteral.mType), constantLiteral.mValue.ToString());
    //    builder.AppendLine();
    //  }
    //  foreach (var type in library.mTypeNameMap.Values)
    //  {
    //    foreach(var function in type.mFunctions)
    //    {
    //      builder.AppendFormat("%{0} = {1}", function.mMeta.mName, OpType.Function.ToString());
    //      builder.AppendLine();

    //      WriteBlock(function.mParametersBlock);
    //      foreach(var block in function.mBlocks)
    //      {
    //        WriteBlock(block);
    //      }
          
    //    }
    //  }


    //  return builder.ToString();
    //}

    //void WriteBlock(ShaderBlock block)
    //{
    //  foreach (var ir in block.mLocalVariables)
    //  {
    //    var blockOp = ir as ShaderOp;
    //    WriteOp(blockOp);
    //  }
    //  foreach (var ir in block.mOps)
    //  {
    //    var blockOp = ir as ShaderOp;
    //    WriteOp(blockOp);
    //  }
    //}

    //void WriteOp(ShaderOp op)
    //{
    //  mBuilder.AppendFormat("%{0} = {1}", GetId(op), op.mOpType.ToString());
    //  foreach (var arg in op.mParameters)
    //    mBuilder.AppendFormat(" %{0}", GetId(arg));
    //  if(!string.IsNullOrEmpty(op.mDebugName))
    //    mBuilder.AppendFormat(" //{0}", op.mDebugName);
    //  mBuilder.AppendLine();
    //}

    //int GetId(IShaderIR ir)
    //{
    //  if(!mIdMap.ContainsKey(ir))
    //  {
    //    mIdMap.Add(ir, mIdMap.Count);
    //  }
    //  return mIdMap[ir];
    //}

  }
}
