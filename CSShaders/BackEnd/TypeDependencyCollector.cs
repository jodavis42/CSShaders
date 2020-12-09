using System;
using System.Collections.Generic;
using Utilities;

namespace CSShaders
{
  public class TypeDependencyCollector
  {
    ShaderLibrary mOwningLibrary;
    public TypeDependencyCollector(ShaderLibrary owningLibrary)
    {
      mOwningLibrary = owningLibrary;
    }

    public void VisitLibrary()
    {
      foreach (var constantOp in mOwningLibrary.mConstantOps.Values)
      {
        Visit(constantOp);
      }
      foreach (var staticField in mOwningLibrary.mStaticGlobals.Values)
      {
        mReferencedStatics.Add(staticField.InstanceOp);
        Visit(staticField.InstanceOp);
      }
      foreach (var type in mOwningLibrary.GetTypes())
      {
        Visit(type);
      }
    }

    public void Visit(IEnumerable<ShaderType> types)
    {
      foreach (var type in types)
        Visit(type);
    }

    public void Visit(ShaderType type)
    {
      // Don't double process a type
      if (type == null || mProcessedTypes.Contains(type))
        return;
      
      // If we're a pointer, visit the dereference type but don't yet add the pointer type to the processed types.
      // This is because visiting the dereference type can visit functions that use the pointer type.
      // This would cause the function type to then be declared before the pointer type here.
      if (type.mBaseType == OpType.Pointer)
      {
        Visit(type.GetDereferenceType());
        if (mProcessedTypes.Contains(type))
          return;
      }
      mProcessedTypes.Add(type);

      Visit(type.mParameters);
      Visit(type.mFields);
      // Only add this as a reference type once we finish walking fields/parameters. This is so we have guaranteed visit any nested types (or dereference types).
      mReferencedTypes.Add(type);

      // Now visit the body of the functions and all of their ops
      Visit(type.mFunctions);
      foreach(var entryPoint in type.mEntryPoints)
        Visit(entryPoint);
    }

    public void Visit(IEnumerable<ShaderField> fields)
    {
      foreach (var field in fields)
        Visit(field);
    }

    public void Visit(ShaderField field)
    {
      Visit(field.mType);
    }

    public void Visit(IEnumerable<ShaderFunction> functions)
    {
      foreach (var function in functions)
        Visit(function);
    }

    public void Visit(ShaderFunction function)
    {
      mReferencedFunctions.Add(function);
      Visit(function.mResultType);
      Visit(function.mParametersBlock);
      Visit(function.mBlocks);
    }

    public void Visit(IEnumerable<ShaderBlock> blocks)
    {
      foreach (var block in blocks)
        Visit(block);
    }

    public void Visit(ShaderBlock block)
    {
      Visit(block.mLocalVariables);
      Visit(block.mOps);
    }

    public void Visit(IEnumerable<IShaderIR> irs)
    {
      foreach (var ir in irs)
        Visit(ir);
    }

    public void Visit(IShaderIR ir)
    {
      if (ir is ShaderType shaderType)
        Visit(shaderType);
      else if (ir is ShaderFunction shaderFunction)
        Visit(shaderFunction);
      else if (ir is ShaderConstantLiteral constantLiteral)
        Visit(constantLiteral);
      else if (ir is ShaderOp shaderOp)
        Visit(shaderOp);
      else if (ir is ExtensionLibraryImportOp extLibraryImportOp)
        Visit(extLibraryImportOp);
      else
        throw new Exception();
    }

    public void Visit(ShaderConstantLiteral constantLiteral)
    {
      Visit(constantLiteral.mType);
    }

    public void Visit(ShaderOp shaderOp)
    {
      if (IsConstantOp(shaderOp))
        mReferencedConstants.Add(shaderOp);
      Visit(shaderOp.mResultType);
      foreach (var param in shaderOp.mParameters)
        Visit(param);
    }

    public void Visit(ExtensionLibraryImportOp extLibraryImportOp)
    {
      mReferencedExtensionLibraryImports.Add(extLibraryImportOp);
    }

    public void Visit(ShaderEntryPointInfo entryPointInfo)
    {
      mEntryPoints.Add(entryPointInfo);
      Visit(entryPointInfo.mEntryPointFunction);
    }

    private bool IsConstantOp(ShaderOp shaderOp)
    {
      return shaderOp.mOpType == OpInstructionType.OpConstant ||
        shaderOp.mOpType == OpInstructionType.OpConstantComposite ||
        shaderOp.mOpType == OpInstructionType.OpConstantTrue ||
        shaderOp.mOpType == OpInstructionType.OpConstantFalse ||
        shaderOp.mOpType == OpInstructionType.OpConstantSampler ||
        shaderOp.mOpType == OpInstructionType.OpConstantNull;
    }

    // Need a separate set to prevent infinite recursion but to not control the added order
    HashSet<ShaderType> mProcessedTypes = new HashSet<ShaderType>();
    public OrderedSet<ShaderType> mReferencedTypes = new OrderedSet<ShaderType>();
    public OrderedSet<ShaderFunction> mReferencedFunctions = new OrderedSet<ShaderFunction>();
    public OrderedSet<ShaderOp> mReferencedStatics = new OrderedSet<ShaderOp>();
    public OrderedSet<ShaderOp> mReferencedConstants = new OrderedSet<ShaderOp>();
    public OrderedSet<ExtensionLibraryImportOp> mReferencedExtensionLibraryImports = new OrderedSet<ExtensionLibraryImportOp>();
    public OrderedSet<ShaderEntryPointInfo> mEntryPoints = new OrderedSet<ShaderEntryPointInfo>();
  }
}
