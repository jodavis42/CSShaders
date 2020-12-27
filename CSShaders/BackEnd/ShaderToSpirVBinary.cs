using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderToSpirVBinary
  {
    const UInt32 MagicNumber = 0x07230203;

    SpirVStreamWriter mWriter;
    ShaderLibrary mLibrary;
    FrontEndTranslator mFrontEnd;
    TypeDependencyCollector mTypeCollector;
    SpirvIdGenerator IdGenerator = new SpirvIdGenerator();
    Dictionary<OpInstructionType, Spv.Op> SimpleInstructions = new Dictionary<OpInstructionType, Spv.Op>();

    public ShaderToSpirVBinary()
    {
      SimpleInstructions.Add(OpInstructionType.OpExtInst, Spv.Op.OpExtInst);

      SimpleInstructions.Add(OpInstructionType.OpConvertFToS, Spv.Op.OpConvertFToS);
      SimpleInstructions.Add(OpInstructionType.OpConvertSToF, Spv.Op.OpConvertSToF);
      SimpleInstructions.Add(OpInstructionType.OpBitcast, Spv.Op.OpBitcast);
      SimpleInstructions.Add(OpInstructionType.OpTranspose, Spv.Op.OpTranspose);
      SimpleInstructions.Add(OpInstructionType.OpSNegate, Spv.Op.OpSNegate);
      SimpleInstructions.Add(OpInstructionType.OpFNegate, Spv.Op.OpFNegate);
      SimpleInstructions.Add(OpInstructionType.OpIAdd, Spv.Op.OpIAdd);
      SimpleInstructions.Add(OpInstructionType.OpFAdd, Spv.Op.OpFAdd);
      SimpleInstructions.Add(OpInstructionType.OpISub, Spv.Op.OpISub);
      SimpleInstructions.Add(OpInstructionType.OpFSub, Spv.Op.OpFSub);
      SimpleInstructions.Add(OpInstructionType.OpIMul, Spv.Op.OpIMul);
      SimpleInstructions.Add(OpInstructionType.OpFMul, Spv.Op.OpFMul);
      SimpleInstructions.Add(OpInstructionType.OpSDiv, Spv.Op.OpSDiv);
      SimpleInstructions.Add(OpInstructionType.OpFDiv, Spv.Op.OpFDiv);
      SimpleInstructions.Add(OpInstructionType.OpSRem, Spv.Op.OpSRem);
      SimpleInstructions.Add(OpInstructionType.OpSMod, Spv.Op.OpSMod);
      SimpleInstructions.Add(OpInstructionType.OpFRem, Spv.Op.OpFRem);
      SimpleInstructions.Add(OpInstructionType.OpFMod, Spv.Op.OpFMod);
      SimpleInstructions.Add(OpInstructionType.OpVectorTimesScalar, Spv.Op.OpVectorTimesScalar);
      SimpleInstructions.Add(OpInstructionType.OpMatrixTimesScalar, Spv.Op.OpMatrixTimesScalar);
      SimpleInstructions.Add(OpInstructionType.OpVectorTimesMatrix, Spv.Op.OpVectorTimesMatrix);
      SimpleInstructions.Add(OpInstructionType.OpMatrixTimesVector, Spv.Op.OpMatrixTimesVector);
      SimpleInstructions.Add(OpInstructionType.OpMatrixTimesMatrix, Spv.Op.OpMatrixTimesMatrix);
      SimpleInstructions.Add(OpInstructionType.OpOuterProduct, Spv.Op.OpOuterProduct);
      SimpleInstructions.Add(OpInstructionType.OpDot, Spv.Op.OpDot);
      SimpleInstructions.Add(OpInstructionType.OpShiftRightLogical, Spv.Op.OpShiftRightLogical);
      SimpleInstructions.Add(OpInstructionType.OpShiftRightArithmetic, Spv.Op.OpShiftRightArithmetic);
      SimpleInstructions.Add(OpInstructionType.OpShiftLeftLogical, Spv.Op.OpShiftLeftLogical);
      SimpleInstructions.Add(OpInstructionType.OpBitwiseOr, Spv.Op.OpBitwiseOr);
      SimpleInstructions.Add(OpInstructionType.OpBitwiseXor, Spv.Op.OpBitwiseXor);
      SimpleInstructions.Add(OpInstructionType.OpBitwiseAnd, Spv.Op.OpBitwiseAnd);
      SimpleInstructions.Add(OpInstructionType.OpNot, Spv.Op.OpNot);
      SimpleInstructions.Add(OpInstructionType.OpBitReverse, Spv.Op.OpBitReverse);
      SimpleInstructions.Add(OpInstructionType.OpBitCount, Spv.Op.OpBitCount);
      SimpleInstructions.Add(OpInstructionType.OpSignBitSet, Spv.Op.OpSignBitSet);
      SimpleInstructions.Add(OpInstructionType.OpSelect, Spv.Op.OpSelect);
      SimpleInstructions.Add(OpInstructionType.OpDPdx, Spv.Op.OpDPdx);
      SimpleInstructions.Add(OpInstructionType.OpDPdy, Spv.Op.OpDPdy);
      SimpleInstructions.Add(OpInstructionType.OpFwidth, Spv.Op.OpFwidth);

      SimpleInstructions.Add(OpInstructionType.OpVectorShuffle, Spv.Op.OpVectorShuffle);
      SimpleInstructions.Add(OpInstructionType.OpCompositeConstruct, Spv.Op.OpCompositeConstruct);
      SimpleInstructions.Add(OpInstructionType.OpCompositeExtract, Spv.Op.OpCompositeExtract);
      SimpleInstructions.Add(OpInstructionType.OpCompositeInsert, Spv.Op.OpCompositeInsert);

      SimpleInstructions.Add(OpInstructionType.OpAny, Spv.Op.OpAny);
      SimpleInstructions.Add(OpInstructionType.OpAll, Spv.Op.OpAll);
      SimpleInstructions.Add(OpInstructionType.OpOrdered, Spv.Op.OpOrdered);
      SimpleInstructions.Add(OpInstructionType.OpLessOrGreater, Spv.Op.OpLessOrGreater);
      SimpleInstructions.Add(OpInstructionType.OpLogicalEqual, Spv.Op.OpLogicalEqual);
      SimpleInstructions.Add(OpInstructionType.OpLogicalNotEqual, Spv.Op.OpLogicalNotEqual);
      SimpleInstructions.Add(OpInstructionType.OpLogicalOr, Spv.Op.OpLogicalOr);
      SimpleInstructions.Add(OpInstructionType.OpLogicalAnd, Spv.Op.OpLogicalAnd);
      SimpleInstructions.Add(OpInstructionType.OpLogicalNot, Spv.Op.OpLogicalNot);
      SimpleInstructions.Add(OpInstructionType.OpIEqual, Spv.Op.OpIEqual);
      SimpleInstructions.Add(OpInstructionType.OpINotEqual, Spv.Op.OpINotEqual);
      SimpleInstructions.Add(OpInstructionType.OpUGreaterThan, Spv.Op.OpUGreaterThan);
      SimpleInstructions.Add(OpInstructionType.OpSGreaterThan, Spv.Op.OpSGreaterThan);
      SimpleInstructions.Add(OpInstructionType.OpUGreaterThanEqual, Spv.Op.OpUGreaterThanEqual);
      SimpleInstructions.Add(OpInstructionType.OpSGreaterThanEqual, Spv.Op.OpSGreaterThanEqual);
      SimpleInstructions.Add(OpInstructionType.OpULessThan, Spv.Op.OpULessThan);
      SimpleInstructions.Add(OpInstructionType.OpSLessThan, Spv.Op.OpSLessThan);
      SimpleInstructions.Add(OpInstructionType.OpULessThanEqual, Spv.Op.OpULessThanEqual);
      SimpleInstructions.Add(OpInstructionType.OpSLessThanEqual, Spv.Op.OpSLessThanEqual);
      SimpleInstructions.Add(OpInstructionType.OpFOrdEqual, Spv.Op.OpFOrdEqual);
      SimpleInstructions.Add(OpInstructionType.OpFOrdNotEqual, Spv.Op.OpFOrdNotEqual);
      SimpleInstructions.Add(OpInstructionType.OpFOrdLessThan, Spv.Op.OpFOrdLessThan);
      SimpleInstructions.Add(OpInstructionType.OpFOrdLessThanEqual, Spv.Op.OpFOrdLessThanEqual);
      SimpleInstructions.Add(OpInstructionType.OpFOrdGreaterThan, Spv.Op.OpFOrdGreaterThan);
      SimpleInstructions.Add(OpInstructionType.OpFOrdGreaterThanEqual, Spv.Op.OpFOrdGreaterThanEqual);

      SimpleInstructions.Add(OpInstructionType.OpAccessChain, Spv.Op.OpAccessChain);
      SimpleInstructions.Add(OpInstructionType.OpLoad, Spv.Op.OpLoad);
      SimpleInstructions.Add(OpInstructionType.OpFunctionParameter, Spv.Op.OpFunctionParameter);
      SimpleInstructions.Add(OpInstructionType.OpConstantTrue, Spv.Op.OpConstantTrue);
      SimpleInstructions.Add(OpInstructionType.OpConstantFalse, Spv.Op.OpConstantFalse);

      SimpleInstructions.Add(OpInstructionType.OpSampledImage, Spv.Op.OpSampledImage);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleImplicitLod, Spv.Op.OpImageSampleImplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleExplicitLod, Spv.Op.OpImageSampleExplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleDrefImplicitLod, Spv.Op.OpImageSampleDrefImplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleDrefExplicitLod, Spv.Op.OpImageSampleDrefExplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleProjImplicitLod, Spv.Op.OpImageSampleProjImplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleProjExplicitLod, Spv.Op.OpImageSampleProjExplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleProjDrefImplicitLod, Spv.Op.OpImageSampleProjDrefImplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageSampleProjDrefExplicitLod, Spv.Op.OpImageSampleProjDrefExplicitLod);
      SimpleInstructions.Add(OpInstructionType.OpImageFetch, Spv.Op.OpImageFetch);
      SimpleInstructions.Add(OpInstructionType.OpImageGather, Spv.Op.OpImageGather);
      SimpleInstructions.Add(OpInstructionType.OpImageDrefGather, Spv.Op.OpImageDrefGather);
      SimpleInstructions.Add(OpInstructionType.OpImageRead, Spv.Op.OpImageRead);
      SimpleInstructions.Add(OpInstructionType.OpImageWrite, Spv.Op.OpImageWrite);
      SimpleInstructions.Add(OpInstructionType.OpImage, Spv.Op.OpImage);
      SimpleInstructions.Add(OpInstructionType.OpImageQueryFormat, Spv.Op.OpImageQueryFormat);
      SimpleInstructions.Add(OpInstructionType.OpImageQueryOrder, Spv.Op.OpImageQueryOrder);
      SimpleInstructions.Add(OpInstructionType.OpImageQuerySizeLod, Spv.Op.OpImageQuerySizeLod);
      SimpleInstructions.Add(OpInstructionType.OpImageQuerySize, Spv.Op.OpImageQuerySize);
      SimpleInstructions.Add(OpInstructionType.OpImageQueryLod, Spv.Op.OpImageQueryLod);
      SimpleInstructions.Add(OpInstructionType.OpImageQueryLevels, Spv.Op.OpImageQueryLevels);
      SimpleInstructions.Add(OpInstructionType.OpImageQuerySamples, Spv.Op.OpImageQuerySamples);
    }

    public void Write(SpirVStreamWriter writer, ShaderLibrary library, FrontEndTranslator frontEnd)
    {
      mWriter = writer;
      mLibrary = library;
      mFrontEnd = frontEnd;

      mTypeCollector = new TypeDependencyCollector(mLibrary);
      mTypeCollector.VisitLibrary();

      CreateDummyEntryPoint();
      IdGenerator.GenerateIds(mLibrary, mTypeCollector);
      WriteHeader();
      WriteDebugInstructions();
      WriteTypeDeclarations();
      WriteConstants();
      WriteGlobalStatics();
      WriteTypeFunctions();
    }

    UInt32 GetId(IShaderIR ir)
    {
      return IdGenerator.GetId(ir);
    }

    void CreateDummyEntryPoint()
    {
      var dummyGenerator = new SpirVDummyEntryPointGenerator();
      dummyGenerator.CreateDummyEntryPoint(mTypeCollector, mLibrary, mFrontEnd);
    }

    void WriteHeader()
    {
      mWriter.Write(MagicNumber);
      mWriter.Write(0, 1, 4, 0);
      mWriter.Write(0);
      mWriter.Write(IdGenerator.Count + 1);
      mWriter.Write(0);

      // Capabilities
      mWriter.WriteInstruction(2, Spv.Op.OpCapability, (UInt32)Spv.Capability.CapabilityShader);
      mWriter.WriteInstruction(2, Spv.Op.OpCapability, (UInt32)Spv.Capability.CapabilityLinkage);
      // Extension Instance Imports
      foreach(var extLibraryImport in mTypeCollector.mReferencedExtensionLibraryImports)
      {
        var byteCount = mWriter.GetPaddedByteCount(extLibraryImport.ExtensionLibraryName);
        var wordCount = byteCount / 4;
        UInt16 totalSize = (UInt16)(2 + wordCount);
        mWriter.WriteInstruction(totalSize, Spv.Op.OpExtInstImport, GetId(extLibraryImport));
        mWriter.Write(extLibraryImport.ExtensionLibraryName);
      }
      // Memory Models
      mWriter.WriteInstruction(3, Spv.Op.OpMemoryModel, (UInt32)Spv.AddressingModel.AddressingModelLogical, (UInt32)Spv.MemoryModel.MemoryModelGLSL450);
      // EntryPoints
      foreach(var entryPoint in mTypeCollector.mEntryPoints)
      {
        var entryPointFn = entryPoint.mEntryPointFunction;
        var entryPointId = GetId(entryPointFn);
        var entryPointName = entryPointFn.DebugInfo.Name;
        var byteCount = mWriter.GetPaddedByteCount(entryPointName);
        var wordCount = byteCount / 4;
        var interfaceSize = entryPoint.mGlobalVariablesBlock.mLocalVariables.Count;
        UInt16 totalSize = (UInt16)(3 + wordCount + interfaceSize);

        Spv.ExecutionModel executionModel = Spv.ExecutionModel.ExecutionModelFragment;
        if (entryPoint.mStageType == FragmentType.Vertex)
          executionModel = Spv.ExecutionModel.ExecutionModelVertex;
        else if(entryPoint.mStageType == FragmentType.Pixel)
          executionModel = Spv.ExecutionModel.ExecutionModelFragment;

        mWriter.WriteInstruction(totalSize, Spv.Op.OpEntryPoint, (UInt32)executionModel, entryPointId);
        mWriter.Write(entryPointFn.DebugInfo.Name);
        foreach(var interfaceVar in entryPoint.mGlobalVariablesBlock.mLocalVariables)
        {
          mWriter.Write(GetId(interfaceVar));
        }
      }
      // ExecutionMode (per entry point)
      foreach(var entryPoint in mTypeCollector.mEntryPoints)
      {
        WriteBlockInstructions(entryPoint.mExecutionModesBlock, entryPoint.mExecutionModesBlock.mOps);
      }

      mWriter.WriteInstruction(3, Spv.Op.OpSource, 0, 100);

      var capabilities = new HashSet<Spv.Capability>();
      //foreach(var entryPoint in mTypeCollector.mEntryPoints)
      //{
      //  foreach(var capability in entryPoint.m)
      //}
    }

    void WriteDebugInstructions()
    {
      foreach (var type in mTypeCollector.mReferencedTypes)
      {
        WriteDebugName(type, type.DebugInfo.Name);
        for(var i = 0; i < type.mFields.Count; ++i)
          WriteFieldDebugName(type, i, type.mFields[i], type.mFields[i].DebugInfo.Name);
      }
      foreach (var function in mTypeCollector.mReferencedFunctions)
      {
        WriteDebugInstructions(function);
      }
      foreach (var globalStatic in mTypeCollector.mReferencedStatics)
      {
        WriteDebugName(globalStatic, globalStatic.DebugInfo.Name);
      }
      foreach (var entryPoint in mTypeCollector.mEntryPoints)
      {
        WriteDebugInstructions(entryPoint.mEntryPointFunction);
      }
    }

    void WriteDebugInstructions(ShaderFunction function)
    {
      WriteDebugName(function, function.DebugInfo.Name);
      WriteDebugInstructions(function.mParametersBlock);
      foreach (var block in function.mBlocks)
        WriteDebugInstructions(block);
    }

    void WriteDebugInstructions(ShaderBlock block)
    {
      foreach (var op in block.mLocalVariables)
        WriteDebugName(op, op.DebugInfo.Name);
      foreach (var op in block.mOps)
        WriteDebugName(op, op.DebugInfo.Name);
    }

    void WriteDebugName(IShaderIR ir, string debugName)
    {
      if (string.IsNullOrEmpty(debugName))
        return;

      var id = GetId(ir);
      var wordCount = mWriter.GetPaddedWordCount(debugName);
      var instructionSize = (UInt16)(2 + wordCount);
      mWriter.WriteInstruction(instructionSize, Spv.Op.OpName);
      mWriter.Write(id);
      mWriter.Write(debugName);
    }

    void WriteFieldDebugName(ShaderType owningType, int memberIndex, ShaderField field, string debugName)
    {
      if (string.IsNullOrEmpty(debugName))
        return;

      var wordCount = mWriter.GetPaddedWordCount(debugName);
      var instructionSize = (UInt16)(3 + wordCount);
      mWriter.WriteInstruction(instructionSize, Spv.Op.OpMemberName);
      mWriter.Write(GetId(owningType));
      mWriter.Write(memberIndex);
      mWriter.Write(debugName);
    }

    Spv.StorageClass ConvertStorageClass(StorageClass storageClass)
    {
      if (storageClass == StorageClass.Function)
        return Spv.StorageClass.StorageClassFunction;
      else if (storageClass == StorageClass.Private)
        return Spv.StorageClass.StorageClassPrivate;
      else if (storageClass == StorageClass.Generic)
        return Spv.StorageClass.StorageClassGeneric;
      else if (storageClass == StorageClass.Uniform)
        return Spv.StorageClass.StorageClassUniform;
      else if (storageClass == StorageClass.UniformConstant)
        return Spv.StorageClass.StorageClassUniformConstant;
      return Spv.StorageClass.StorageClassFunction;
    }

    void WriteTypeDeclarations()
    {
      var visitedTypeIds = new HashSet<UInt32>();
      foreach (var type in mTypeCollector.mReferencedTypes)
      {
        // Handle types existing more than once (for primitive deduping)
        var id = GetId(type);
        if (visitedTypeIds.Contains(id))
          continue;
        visitedTypeIds.Add(id);

        if (type.mBaseType == OpType.Void)
        {
          mWriter.WriteInstruction(2, Spv.Op.OpTypeVoid, GetId(type));
        }
        else if (type.mBaseType == OpType.Bool)
        {
          mWriter.WriteInstruction(2, Spv.Op.OpTypeBool, GetId(type));
        }
        else if (type.mBaseType == OpType.Int)
        {
          mWriter.WriteInstruction(4, Spv.Op.OpTypeInt, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.Float)
        {
          mWriter.WriteInstruction(3, Spv.Op.OpTypeFloat, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.Vector)
        {
          mWriter.WriteInstruction(4, Spv.Op.OpTypeVector, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.Matrix)
        {
          mWriter.WriteInstruction(4, Spv.Op.OpTypeMatrix, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.Pointer)
        {
          var dereferenceType = type.GetDereferenceType();
          if (dereferenceType.mBaseType != OpType.Function)
          {
            var storageClass = ConvertStorageClass(type.mStorageClass);
            mWriter.WriteInstruction(4, Spv.Op.OpTypePointer, GetId(type), (UInt32)storageClass, GetId(dereferenceType));
          }
        }
        else if (type.mBaseType == OpType.Struct)
        {
          UInt16 instructionSize = (UInt16)(2 + type.mFields.Count);
          mWriter.WriteInstruction(instructionSize, Spv.Op.OpTypeStruct, GetId(type));
          foreach (var field in type.mFields)
          {
            mWriter.Write(GetId(field.mType));
          }
        }
        else if (type.mBaseType == OpType.Sampler)
        {
          UInt16 instructionSize = (UInt16)2;
          mWriter.WriteInstruction(instructionSize, Spv.Op.OpTypeSampler, GetId(type));
        }
        else if (type.mBaseType == OpType.Image)
        {
          UInt16 instructionSize = (UInt16)9;
          mWriter.WriteInstruction(instructionSize, Spv.Op.OpTypeImage, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.SampledImage)
        {
          UInt16 instructionSize = (UInt16)3;
          mWriter.WriteInstruction(instructionSize, Spv.Op.OpTypeSampledImage, GetId(type));
          WriteArgs(type.mParameters);
        }
        else if (type.mBaseType == OpType.Function)
        {
          var instructionSize = 2 + type.mParameters.Count;
          mWriter.WriteInstruction((UInt16)(instructionSize), Spv.Op.OpTypeFunction, GetId(type));
          WriteArgs(type.mParameters);
        }
        else
          throw new Exception();
      }
    }

    void WriteConstants()
    {
      foreach (var constantOp in mTypeCollector.mReferencedConstants)
      {
        WriteOp(constantOp);
      }
    }

    void WriteGlobalStatics()
    {
      foreach (var constantOp in mTypeCollector.mReferencedStatics)
      {
        mWriter.WriteInstruction((UInt16)(4), Spv.Op.OpVariable);
        mWriter.Write(GetId(constantOp.mResultType));
        mWriter.Write(GetId(constantOp));
        var spirvStorageClass = ConvertStorageClass(constantOp.mResultType.mStorageClass);
        mWriter.Write((UInt32)spirvStorageClass);
      }
    }

    void WriteTypeFunctions()
    {
      foreach (var function in mTypeCollector.mReferencedFunctions)
      {
        var functionId = GetId(function);
        UInt32 returnTypeId = GetId(function.mParameters[0]);
        UInt32 functionTypeId = GetId(function.mResultType);
        mWriter.WriteInstruction(5, Spv.Op.OpFunction, new List<UInt32> { returnTypeId, functionId, (UInt32)Spv.FunctionControlMask.FunctionControlMaskNone, functionTypeId });

        foreach (var paramIR in function.mParametersBlock.mOps)
        {
          var paramOp = paramIR as ShaderOp;
          WriteOp(paramOp);
        }
        foreach (var block in function.mBlocks)
          WriteBlock(block);
        mWriter.WriteInstruction(1, Spv.Op.OpFunctionEnd);
      }
    }

    void WriteBlock(ShaderBlock block)
    {
      mWriter.WriteInstruction(2, Spv.Op.OpLabel, GetId(block));
      WriteBlockInstructions(block, block.mLocalVariables);
      WriteBlockInstructions(block, block.mOps);
    }

    void WriteBlockInstructions(ShaderBlock block, List<IShaderIR> instructions)
    {
      foreach (var ir in instructions)
      {
        WriteIR(ir);
      }
    }

    void WriteIR(IShaderIR ir)
    {
      if (ir is ShaderOp op)
        WriteOp(op);
      else if (ir is ShaderConstantLiteral constantLiteral)
        WriteConstantLiteral(constantLiteral);
      else
        throw new Exception();
    }

    void WriteOp(ShaderOp op)
    {
      switch (op.mOpType)
      {
        case OpInstructionType.OpStore:
          WriteBasicOpNoResultId(op, Spv.Op.OpStore);
          break;
        case OpInstructionType.OpVariable:
          mWriter.WriteInstruction((UInt16)(4 + op.mParameters.Count), Spv.Op.OpVariable);
          mWriter.Write(GetId(op.mResultType));
          mWriter.Write(GetId(op));
          mWriter.Write((UInt32)Spv.StorageClass.StorageClassFunction);
          break;
        case OpInstructionType.OpReturn:
          mWriter.WriteInstruction(1, Spv.Op.OpReturn);
          break;
        case OpInstructionType.OpReturnValue:
          mWriter.WriteInstruction(2, Spv.Op.OpReturnValue, GetId(op.mParameters[0]));
          break;
        case OpInstructionType.OpExecutionMode:
          WriteBasicOpNoResultId(op, Spv.Op.OpExecutionMode);
          break;
        case OpInstructionType.OpConstant:
          mWriter.WriteInstruction(4, Spv.Op.OpConstant);
          var constantLiteral = op.mParameters[0] as ShaderConstantLiteral;
          mWriter.Write(GetId(op.mResultType));
          mWriter.Write(GetId(op));
          if (constantLiteral.mValue is bool boolVal)
            mWriter.Write(boolVal ? 1 : 0);
          else if (constantLiteral.mValue is int intVal)
            mWriter.Write(intVal);
          else if (constantLiteral.mValue is uint uintVal)
            mWriter.Write(uintVal);
          else if (constantLiteral.mValue is float floatVal)
          {
            byte[] raw = BitConverter.GetBytes(floatVal);
            int floatAsInt = BitConverter.ToInt32(raw, 0);
            mWriter.Write(floatAsInt);
          }
          else
            throw new Exception();
            
          break;
        case OpInstructionType.OpFunctionCall:
          UInt16 instructionCount = (UInt16)(3 + op.mParameters.Count);
          mWriter.WriteInstruction(instructionCount, Spv.Op.OpFunctionCall);
          mWriter.Write(GetId(op.mResultType));
          mWriter.Write(GetId(op));
          WriteArgs(op.mParameters);
          break;
        default:
          Spv.Op spvOp;
          if(!SimpleInstructions.TryGetValue(op.mOpType, out spvOp))
            throw new Exception();
          WriteBasicOp(op, spvOp);
          break;
      }
    }

    void WriteBasicOp(ShaderOp op, Spv.Op spvOpType)
    {
      UInt16 totalSize = (UInt16)(2 + op.mParameters.Count);
      if (op.mResultType != null)
        ++totalSize;
      mWriter.WriteInstruction(totalSize, spvOpType);
      if (op.mResultType != null)
        mWriter.Write(GetId(op.mResultType));
      mWriter.Write(GetId(op));
      WriteArgs(op.mParameters);
    }

    void WriteBasicOpNoResultId(ShaderOp op, Spv.Op spvOpType)
    {
      UInt16 totalSize = (UInt16)(1 + op.mParameters.Count);
      if (op.mResultType != null)
        ++totalSize;
      mWriter.WriteInstruction(totalSize, spvOpType);
      if (op.mResultType != null)
        mWriter.Write(GetId(op.mResultType));
      WriteArgs(op.mParameters);
    }

    void WriteArgs(List<IShaderIR> args)
    {
      foreach (var arg in args)
      {
        var constantLiteral = arg as ShaderConstantLiteral;
        if (constantLiteral != null)
        {
          WriteConstantLiteral(constantLiteral);
        }
        else
        {
          var id = GetId(arg);
          mWriter.Write(id);
        }
      }
    }

    void WriteConstantLiteral(ShaderConstantLiteral constantLiteral)
    {
      if (constantLiteral.mType.mBaseType == OpType.Int)
      {
        if (constantLiteral.mValue is int intValue)
          mWriter.Write(intValue);
        else if (constantLiteral.mValue is uint uintValue)
          mWriter.Write(uintValue);
        else
          throw new Exception();
      }
      else
        throw new Exception();
    }
  }
}
