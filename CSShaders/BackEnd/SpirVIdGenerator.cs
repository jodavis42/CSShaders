using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public class SpirvIdGenerator
  {
    UInt32 mId = 1;
    Dictionary<IShaderIR, UInt32> mIdMap = new Dictionary<IShaderIR, UInt32>();
    Dictionary<String, UInt32> mPrimitiveDedupeMap = new Dictionary<String, UInt32>();

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
      foreach (var extLibraryImport in typeCollector.mReferencedExtensionLibraryImports)
      {
        GenerateIds(extLibraryImport);
      }
      foreach (var ir in typeCollector.mReferencedTypesConstantsAndGlobals)
      {
        GenerateIds(ir);
      }
      foreach (var function in typeCollector.mReferencedFunctions)
      {
        GenerateIds(function);
      }
    }

    void GenerateIds(ShaderType shaderType)
    {
      if (mIdMap.ContainsKey(shaderType))
        return;

      // SpirV only allows one instance of a primitive type, but for convenience we allow multiple
      // distinct types that share the same underlying primitive type (e.g. Vector4, Quaternion).
      // To handle this, we dedupe the types to the same underlying id.
      if (shaderType.IsPrimitiveType())
      {
        if (TryDedupePrimitiveType(shaderType))
          return;
      }

      GetId(shaderType);
      if (shaderType.StorageClassCollection != null)
        GetId(shaderType.FindPointerType(StorageClass.Function));
      foreach (var param in shaderType.mParameters)
        GenerateIds(param);
      foreach (var field in shaderType.mFields)
        GetId(field.mType);
    }

    bool TryDedupePrimitiveType(ShaderType shaderType)
    {
      var builder = new StringBuilder();
      BuildTypeIdentifier(shaderType, ref builder);
      var idStr = builder.ToString();

      // If we already contain this type then mark the duplicate and return
      if (mPrimitiveDedupeMap.ContainsKey(idStr))
      {
        mIdMap.Add(shaderType, mPrimitiveDedupeMap.GetValueOrDefault(idStr));
        return true;
      }
      //Otherwise, register this as a potential duplicate for later
      var id = GetId(shaderType);
      mPrimitiveDedupeMap.Add(idStr, id);
      return false;
    }

    void BuildTypeIdentifier(ShaderType shaderType, ref StringBuilder builder)
    {
      builder.Append(shaderType.mBaseType.ToString());
      builder.Append(" ");
      foreach (var parameter in shaderType.mParameters)
      {
        if (parameter is ShaderConstantLiteral literal)
        {
          builder.Append((UInt32)literal.mValue);
          builder.Append(" ");
        }
        else if (parameter is ShaderType subType)
          BuildTypeIdentifier(subType, ref builder);
        else
          throw new Exception();
      }
    }

    void GenerateIds(ShaderFunction function)
    {
      GenerateIds(function.mResultType);
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
      if (ir is ShaderOp op)
        GenerateIds(op);
      else if (ir is ExtensionLibraryImportOp extImportOp)
        GetId(extImportOp);
      else if (ir is ShaderType shaderType)
        GenerateIds(shaderType);
      else if (ir is ShaderConstantLiteral constantLiteral)
      {
        // No op
      }
      else if (ir is ShaderBlock shaderBlock)
      {
        // Id is already assigned from the owning function
      }
      else
        throw new Exception();
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
