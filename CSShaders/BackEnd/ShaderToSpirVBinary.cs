using System;
using System.Collections.Generic;

namespace CSShaders
{
  public class ShaderToSpirVBinary
  {

    UInt32 MagicNumber = 0x07230203;
    ShaderLibrary mLibrary;
    SpirVStreamWriter mWriter;
    FrontEndTranslator mFrontEnd;
    TypeDependencyCollector mTypeCollector;


    UInt32 mId = 1;
    Dictionary<IShaderIR, UInt32> mIdMap = new Dictionary<IShaderIR, UInt32>();
    
    
    public void Write(SpirVStreamWriter writer, ShaderLibrary library, FrontEndTranslator frontEnd)
    {
      mWriter = writer;
      mLibrary = library;
      mFrontEnd = frontEnd;

      mTypeCollector = new TypeDependencyCollector(mLibrary);
      mTypeCollector.VisitLibrary();
      CreateDummyEntryPoint();
      GenerateIds();
      WriteHeader();
      WriteDebugInstructions();
      WriteTypeDeclarations();
      WriteConstants();
      WriteTypeFunctions();
    }

    void CreateDummyEntryPoint()
    {
      var unitTestTypes = new List<ShaderType>();
      foreach(var type in mTypeCollector.mReferencedTypes)
      {
        if (type.mMeta.mAttributes.Contains("UnitTest"))
          unitTestTypes.Add(type);
      }
      foreach (var type in unitTestTypes)
        CreateDummyEntryPoint(mTypeCollector, type);
      
    }

    void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderType shaderType)
    {
      
      var voidType = mLibrary.FindType(new TypeKey(typeof(void)));
      var dummyMain = mFrontEnd.CreateFunction(shaderType, voidType, "dummyMain", null, null);
      dummyMain.mResultType = mFrontEnd.CreateFunctionType(voidType, null, null);
      dummyMain.DebugInfo.Name = "dummyMain";
      dummyMain.mBlocks.Add(new ShaderBlock());
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;
      context.mCurrentFunction = dummyMain;
      context.mCurrentBlock = dummyMain.mBlocks[0];
      var selfOp = mFrontEnd.CreateOpVariable(shaderType, context);
      selfOp.DebugInfo.Name = "self";
      if(shaderType.mImplicitConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mImplicitConstructor);
        fnParamOps.Add(selfOp);
        mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }
      else if(shaderType.mPreConstructor != null)
      {
        var fnParamOps = new List<IShaderIR>();
        fnParamOps.Add(shaderType.mPreConstructor);
        fnParamOps.Add(selfOp);
        mFrontEnd.CreateOp(context.mCurrentBlock, OpInstructionType.OpFunctionCall, voidType, fnParamOps);
      }

      mFrontEnd.FixupBlockTerminators(dummyMain);
      typeCollector.Visit(dummyMain);

      var dummyEntryPoint = new ShaderEntryPointInfo();
      dummyEntryPoint.mEntryPointFunction = dummyMain;
      dummyEntryPoint.mStageType = shaderType.mFragmentType;
      var executionModeLiteral = mFrontEnd.CreateConstantLiteral(mLibrary.FindType(new TypeKey(typeof(int))), ((int)(Spv.ExecutionMode.ExecutionModeOriginUpperLeft)).ToString());
      var executionModeOp = mFrontEnd.CreateOp(OpInstructionType.OpExecutionMode, null, new List<IShaderIR> { dummyMain, executionModeLiteral });
      dummyEntryPoint.mExecutionModesBlock.mOps.Add(executionModeOp);
      typeCollector.Visit(dummyEntryPoint);
    }

    void WriteHeader()
    {
      mWriter.Write(MagicNumber);
      mWriter.Write(0, 1, 4, 0);
      mWriter.Write(0);
      mWriter.Write(mIdMap.Count + 1);
      mWriter.Write(0);

      mWriter.WriteInstruction(2, Spv.Op.OpCapability, (UInt32)Spv.Capability.CapabilityShader);
      mWriter.WriteInstruction(2, Spv.Op.OpCapability, (UInt32)Spv.Capability.CapabilityLinkage);
      mWriter.WriteInstruction(3, Spv.Op.OpMemoryModel, (UInt32)Spv.AddressingModel.AddressingModelLogical, (UInt32)Spv.MemoryModel.MemoryModelGLSL450);
      // EntryPoints
      foreach(var entryPoint in mTypeCollector.mEntryPoints)
      {
        var entryPointFn = entryPoint.mEntryPointFunction;
        var entryPointId = GetId(entryPointFn);
        var entryPointName = entryPointFn.DebugInfo.Name;
        var byteCount = mWriter.GetPaddedByteCount(entryPointName);
        var wordCount = byteCount / 4;
        UInt16 totalSize = (UInt16)(3 + wordCount);//+interfaceSize

        Spv.ExecutionModel executionModel = Spv.ExecutionModel.ExecutionModelFragment;
        if (entryPoint.mStageType == FragmentType.Vertex)
          executionModel = Spv.ExecutionModel.ExecutionModelVertex;
        else if(entryPoint.mStageType == FragmentType.Pixel)
          executionModel = Spv.ExecutionModel.ExecutionModelFragment;

        mWriter.WriteInstruction(totalSize, Spv.Op.OpEntryPoint, (UInt32)executionModel, entryPointId);
        mWriter.Write(entryPointFn.DebugInfo.Name);
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

    void GenerateIds()
    {
      foreach (var type in mTypeCollector.mReferencedTypes)
      {
        GetId(type);
        if (type.mTypeStorage != null)
          GetId(type.mTypeStorage.mPointerType);
        foreach (var field in type.mFields)
          GetId(field.mType);
      }
      foreach (var constantLiteral in mLibrary.mConstantLiterals.Values)
      {
        //builder.AppendFormat("%{0} = %{1} %{2}", GetId(constantLiteral), GetId(constantLiteral.mType), constantLiteral.mValue.ToString());
        //builder.AppendLine();
      }
      foreach (var constantOp in mLibrary.mConstantOps.Values)
      {
        GenerateIds(constantOp);
        //builder.AppendFormat("%{0} = %{1} %{2}", GetId(constantLiteral), GetId(constantLiteral.mType), constantLiteral.mValue.ToString());
        //builder.AppendLine();
      }
      foreach (var function in mTypeCollector.mReferencedFunctions)
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
      if(op.mResultType != null)
        GetId(op.mResultType);
      GetId(op);
      foreach (var arg in op.mParameters)
        GenerateIds(arg);
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

    void WriteTypeDeclarations()
    {
      foreach (var type in mTypeCollector.mReferencedTypes)
      {
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
        else if (type.mBaseType == OpType.Pointer)
        {
          if(type.mTypeStorage.mDereferenceType.mBaseType != OpType.Function)
            mWriter.WriteInstruction(4, Spv.Op.OpTypePointer, GetId(type), (UInt32)Spv.StorageClass.StorageClassFunction, GetId(type.mTypeStorage.mDereferenceType));
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
        else if (type.mBaseType == OpType.Function)
        {
          var instructionSize = 2 + type.mParameters.Count;
          mWriter.WriteInstruction((UInt16)(instructionSize), Spv.Op.OpTypeFunction, GetId(type));
          WriteArgs(type.mParameters);
        }
      }
    }

    void WriteConstants()
    {
      foreach (var constantOp in mLibrary.mConstantOps.Values)
      {
        WriteOp(constantOp);
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
        var blockOp = ir as ShaderOp;
        WriteOp(blockOp);
      }
    }

    void WriteIR(IShaderIR ir)
    {
      var op = ir as ShaderOp;
      if (op != null)
        WriteOp(op);
      else
      {
        var constantLiteral = ir as ShaderConstantLiteral;
        WriteConstantLiteral(constantLiteral);
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

    void WriteOp(ShaderOp op)
    {
      switch (op.mOpType)
      {
        case OpInstructionType.OpStore:
          WriteBasicOpNoResultId(op, Spv.Op.OpStore);
          break;
        case OpInstructionType.OpLoad:
          WriteBasicOp(op, Spv.Op.OpLoad);
          break;
        case OpInstructionType.OpVariable:
          mWriter.WriteInstruction((UInt16)(4 + op.mParameters.Count), Spv.Op.OpVariable);
          mWriter.Write(GetId(op.mResultType));
          mWriter.Write(GetId(op));
          mWriter.Write((UInt32)Spv.StorageClass.StorageClassFunction);
          break;
        case OpInstructionType.OpFunctionParameter:
          WriteBasicOp(op, Spv.Op.OpFunctionParameter);
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
        case OpInstructionType.OpConstantTrue:
          WriteBasicOp(op, Spv.Op.OpConstantTrue);
          break;
        case OpInstructionType.OpConstantFalse:
          WriteBasicOp(op, Spv.Op.OpConstantFalse);
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
            mWriter.Write((int)floatVal);
          break;
        case OpInstructionType.OpAccessChain:
          WriteBasicOp(op, Spv.Op.OpAccessChain);
          break;
        case OpInstructionType.OpFunctionCall:
          UInt16 instructionCount = (UInt16)(3 + op.mParameters.Count);
          mWriter.WriteInstruction(instructionCount, Spv.Op.OpFunctionCall);
          mWriter.Write(GetId(op.mResultType));
          mWriter.Write(GetId(op));
          WriteArgs(op.mParameters);

          break;
        default:
          throw new Exception();
      }
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
      }
      else
        throw new Exception();
    }

    UInt32 GetId(IShaderIR ir)
    {
      if (!mIdMap.ContainsKey(ir))
      {
        mIdMap.Add(ir, mId);
        ++mId;
      }
      return mIdMap[ir];
    }





  }
}
