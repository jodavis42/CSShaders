﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSShaders
{
  public enum OpType
  {
    Void, Bool, Int, Float, Vector, Matrix, Image, Sampler, SampledImage, Array, RuntimeArray, Struct, Opaque, Function, Pointer
  }

  public enum StorageClass
  {
    Unknown, Function, Generic, Private, Uniform, UniformConstant
  }


  public enum OpInstructionType
  {
    //Misc
    OpNop, OpUndef,
    // Debug
    OpSource, OpSourceContinued, OpSourceExtension, OpName, OpMemberName, OpString, OpLine, OpNoLine,
    // Annotations
    OpDecorate, OpMemberDecorate, OpDecorationGroup, OpGroupDecorate, OpGroupMemberDecorate,
    // Extensions
    OpExtension, OpExtInstImport, OpExtInst,
    // Mode Setting
    OpMemoryModel, OpEntryPoint, OpExecutionMode, OpCapability, 
    // Type Declarations
    OpTypeVoid, OpTypeBool, OpTypeInt, OpTypeFloat, OpTypeVector, OpTypeMatrix, OpTypeImage, OpTypeSampler, OpTypeSampledImage, OpTypeArray, OpTypeRuntimeArray, OpTypeStruct, OpTypeOpaque, OpTypeFunction,
    // Constant Creation
    OpConstantTrue, OpConstantFalse, OpConstant, OpConstantComposite, OpConstantSampler, OpConstantNull, OpSpecConstantTrue, OpSpecConstantFalse, OpSpecConstant, OpSpecConstantComposite, OpSpecConstantOp,
    // Memory
    OpVariable, OpLoad, OpStore, OpAccessChain, OpPtrAccessChain, OpArrayLength,
    // Functions
    OpFunction = 54, OpFunctionParameter, OpFunctionEnd, OpFunctionCall,
    // Image
    OpSampledImage = 86, OpImageSampleImplicitLod, OpImageSampleExplicitLod,
    // Conversion
    OpConvertFToU = 109,
    // Composite
    OpVectorExtractDynamic = 77, OpVectorInsertDynamic, OpVectorShuffle, OpCompositeConstruct, OpCompositeExtract, OpCompositeInsert, OpCopyObject, OpTranspose, 

    // Arithmetic
    OpSNegate = 126, OpFNegate, OpIAdd, OpFAdd, OpISub, OpFSub, OpIMul, OpFMul, OpUDiv, OpSDiv, OpFDiv, OpUMod, OpSRem, OpSMod, OpFRem, OpFMod,
    OpVectorTimesScalar, OpMatrixTimesScalar, OpVectorTimesMatrix, OpMatrixTimesVector, OpMatrixTimesMatrix, OpOuterProduct, OpDot, 
    // Bit
    OpShiftRightLogical, OpShiftRightArithmetic, OpShiftLeftLogical, OpBitwiseOr, OpBitwiseXor, OBitwiseAnd, OpNot, OpBitReverse, OpBitCount,
    // Relational
    OpAny, OpAll, OpIsNan, OpIsInf, OpOrdered, OpLessOrGreater, OpLogicalEqual, OpLogicalNotEqual, OpLogicalOr, OpLogicalAnd, OpLogicalNot, OpSelect,
    OpIEqual, OpINotEqual, OpUGreaterThan, OpSGreaterThan, OpUGreaterThanEqual, OpSGreaterThanEqual, OpULessThan, OpSLessThan, OpULessThanEqual, OpSLessThanEqual,
    OpFOrdEqual, OpFUnordEqual, OpFOrdNotEqual, OpFUnordNotEqual, OpFOrdLessThan, OpFUnordLessThan, OpFOrdGreaterThan, OpFUnordGreaterThan, OpFOrdLessThanEqual,
    OpFUnordLessThanEqual, OpFOrdGreaterThanEqual, OpFUnordGreaterThanEqual, 
    // Derivatives
    OpDPdx, OpDPdy, OpFwidth,
    // Control
    OpLoopMerge, OpSelectionMerge, OpLabel, OpBranch, OpBranchConditional, OpSwitch, OpKill, OpReturn, OpReturnValue, OpUnreachable,
  }
}
