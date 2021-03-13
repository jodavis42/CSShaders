using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSShaders
{
  public class CoreShaderLibrary : ShaderLibrary
  {
    public CoreShaderLibrary(FrontEndTranslator translator)
    {
      translator.mCurrentLibrary = this;

      // Create core primitive times first
      translator.CreatePrimitive(typeof(void), OpType.Void);
      translator.CreatePrimitive(typeof(bool), OpType.Bool);
      // Unsigned int must be first
      CreateIntType(translator, typeof(uint), 32, 0);
      CreateIntType(translator, typeof(int), 32, 1);
      var floatType = CreateFloatType(translator, typeof(float), 32);
      // @JoshD: Double requires special capabilities, re-add later.
      //CreateFloatType(frontEnd, typeof(double), 64);

      // Add some custom intrinsics for built-int types that 
      AddPrimitiveIntrinsics(translator);
      AddPrimitiveOps(translator);
      AddCastIntrinsics(translator);

      // Compile the custom data types and functions we're adding
      // @JoshD: Fix path lookup
      var path = Path.Combine("..", "Shader");
      var extensions = new HashSet<string> { ".cs" };
      var project = new ShaderProject();
      project.LoadProjectDirectory(path, extensions);
      var trees = new List<SyntaxTree>();
      SourceCompilation = project.Compile("Core", null, translator, out trees);
      project.Translate(SourceCompilation, trees, null, translator, this);
    }

    ShaderType CreateIntType(FrontEndTranslator translator, Type type, uint width, uint signedness)
    {
      var floatType = translator.CreatePrimitive(type, OpType.Int);
      floatType.mParameters.Add(translator.CreateConstantLiteral(width));
      floatType.mParameters.Add(translator.CreateConstantLiteral(signedness));
      return floatType;
    }

    ShaderType CreateFloatType(FrontEndTranslator translator, Type type, uint width)
    {
      var floatType = translator.CreatePrimitive(type, OpType.Float);
      floatType.mParameters.Add(translator.CreateConstantLiteral(width));
      return floatType;
    }

    void AddPrimitiveIntrinsics(FrontEndTranslator translator)
    {
      var floatType = translator.FindType(typeof(float));
      SpecialResolvers.CreateSimpleIntrinsicFunction(translator, new FunctionKey("float.operator +(float, float)"), OpInstructionType.OpFAdd, floatType);
    }

    void AddPrimitiveOps(FrontEndTranslator translator)
    {
      var boolType = FindType(new TypeKey(typeof(bool)));
      var intType = FindType(new TypeKey(typeof(int)));
      var uintType = FindType(new TypeKey(typeof(uint)));
      var floatType = FindType(new TypeKey(typeof(float)));

      AddSimpleValueTypeUnaryOp(translator, boolType, "!", boolType, OpInstructionType.OpLogicalNot);
      AddSimpleValueTypeBinaryOp(translator, boolType, boolType, "|", boolType, OpInstructionType.OpLogicalOr);
      AddSimpleValueTypeBinaryOp(translator, boolType, boolType, "&", boolType, OpInstructionType.OpLogicalAnd);
      AddSimpleValueTypeBinaryOp(translator, boolType, boolType, "^", boolType, OpInstructionType.OpLogicalNotEqual);

      IncDecOp(translator, intType, "++", intType, OpInstructionType.OpIAdd);
      IncDecOp(translator, intType, "--", intType, OpInstructionType.OpISub);
      AddSimpleValueTypeUnaryOp(translator, intType, "~", intType, OpInstructionType.OpNot);
      AddSimpleValueTypeUnaryOp(translator, intType, "-", intType, OpInstructionType.OpSNegate);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "+", intType, OpInstructionType.OpIAdd);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "-", intType, OpInstructionType.OpISub);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "*", intType, OpInstructionType.OpIMul);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "/", intType, OpInstructionType.OpSDiv);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "%", intType, OpInstructionType.OpSMod);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "|", intType, OpInstructionType.OpBitwiseOr);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "&", intType, OpInstructionType.OpBitwiseAnd);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "^", intType, OpInstructionType.OpBitwiseXor);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, "<<", intType, OpInstructionType.OpShiftLeftLogical);
      AddSimpleValueTypeBinaryOp(translator, intType, intType, ">>", intType, OpInstructionType.OpShiftRightArithmetic);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, "<", intType, OpInstructionType.OpSLessThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, "<=", intType, OpInstructionType.OpSLessThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, ">", intType, OpInstructionType.OpSGreaterThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, ">=", intType, OpInstructionType.OpSGreaterThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, "==", intType, OpInstructionType.OpIEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, intType, "!=", intType, OpInstructionType.OpINotEqual);
      LogicOrAndOp(translator, boolType, boolType, "||", boolType, OpInstructionType.OpLogicalOr, true);
      LogicOrAndOp(translator, boolType, boolType, "&&", boolType, OpInstructionType.OpLogicalAnd, false);

      IncDecOp(translator, uintType, "++", uintType, OpInstructionType.OpIAdd);
      IncDecOp(translator, uintType, "--", uintType, OpInstructionType.OpISub);
      AddSimpleValueTypeUnaryOp(translator, uintType, "~", uintType, OpInstructionType.OpNot);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "+", uintType, OpInstructionType.OpIAdd);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "-", uintType, OpInstructionType.OpISub);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "*", uintType, OpInstructionType.OpIMul);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "/", uintType, OpInstructionType.OpUDiv);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "%", uintType, OpInstructionType.OpUMod);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "|", uintType, OpInstructionType.OpBitwiseOr);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "&", uintType, OpInstructionType.OpBitwiseAnd);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "^", uintType, OpInstructionType.OpBitwiseXor);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, "<<", intType, OpInstructionType.OpShiftLeftLogical);
      AddSimpleValueTypeBinaryOp(translator, uintType, uintType, ">>", intType, OpInstructionType.OpShiftRightArithmetic);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, "<", uintType, OpInstructionType.OpULessThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, "<=", uintType, OpInstructionType.OpULessThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, ">", uintType, OpInstructionType.OpUGreaterThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, ">=", uintType, OpInstructionType.OpUGreaterThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, "==", uintType, OpInstructionType.OpIEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, uintType, "!=", uintType, OpInstructionType.OpINotEqual);

      IncDecOp(translator, floatType, "++", floatType, OpInstructionType.OpFAdd);
      IncDecOp(translator, floatType, "--", floatType, OpInstructionType.OpFSub);
      AddSimpleValueTypeUnaryOp(translator, floatType, "-", floatType, OpInstructionType.OpFNegate);
      AddSimpleValueTypeBinaryOp(translator, floatType, floatType, "+", floatType, OpInstructionType.OpFAdd);
      AddSimpleValueTypeBinaryOp(translator, floatType, floatType, "-", floatType, OpInstructionType.OpFSub);
      AddSimpleValueTypeBinaryOp(translator, floatType, floatType, "*", floatType, OpInstructionType.OpFMul);
      AddSimpleValueTypeBinaryOp(translator, floatType, floatType, "/", floatType, OpInstructionType.OpFDiv);
      AddSimpleValueTypeBinaryOp(translator, floatType, floatType, "%", floatType, OpInstructionType.OpFMod);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, "<", floatType, OpInstructionType.OpFOrdLessThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, "<=", floatType, OpInstructionType.OpFOrdLessThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, ">", floatType, OpInstructionType.OpFOrdGreaterThan);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, ">=", floatType, OpInstructionType.OpFOrdGreaterThanEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, "==", floatType, OpInstructionType.OpFOrdEqual);
      AddSimpleValueTypeBinaryOp(translator, boolType, floatType, "!=", floatType, OpInstructionType.OpFOrdNotEqual);
    }

    void AddSimpleValueTypeUnaryOp(FrontEndTranslator translator, ShaderType resultType, string opToken, ShaderType operandType, OpInstructionType instructionType)
    {
      CreateUnaryOpIntrinsic(new UnaryOpKey(opToken, operandType), (IShaderIR operandIR, FrontEndContext context) =>
      {
        var operandValueOp = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, operandIR);
        return translator.CreateOp(context.mCurrentBlock, instructionType, resultType, new List<IShaderIR> { operandValueOp });
      });
    }

    void AddSimpleValueTypeBinaryOp(FrontEndTranslator translator, ShaderType resultType, ShaderType lhsType, string opToken, ShaderType rhsType, OpInstructionType instructionType)
    {
      CreateBinaryOpIntrinsic(new BinaryOpKey(lhsType, opToken, rhsType), (IShaderIR lhsExpression, IShaderIR rhsExpression, FrontEndContext context) =>
      {
        var lhsValueOp = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, lhsExpression);
        var rhsValueOp = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, rhsExpression);
        return translator.CreateOp(context.mCurrentBlock, instructionType, resultType, new List<IShaderIR> { lhsValueOp, rhsValueOp });
      });
    }

    void LogicalOrAnd(SyntaxNode lhsExpression, SyntaxNode rhsExpression, FrontEndContext context, bool isOr)
    {
      var translator = context.mFrontEnd;
      var boolType = translator.FindType(typeof(bool));

      // Walk the left hand operator (this can change the current block)
      var leftIR = context.mCurrentPass.WalkAndGetResult(lhsExpression);
      // In the current block, get the value type result of the left hand side
      var leftOp = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, leftIR);

      // Logical ors/ands have to generate conditionals due to short circuit evaluation. To store the temporary
      // result we have to either generate a temporary variable or use an OpPhi instruction. For convenience generate a temporary.
      var temp = translator.CreateOpVariable(boolType.FindPointerType(StorageClass.Function), context);
      if (isOr)
        temp.DebugInfo.Name = "tempOr";
      else
        temp.DebugInfo.Name = "tempAnd";

      // If statements always have 3 new blocks
      var ifTrue = translator.CreateBlock("ifTrue");
      var ifFalse = translator.CreateBlock("ifFalse");
      var mergePoint = translator.CreateBlock("mergePoint");
      // Mark the current block as a selection that merges at the merge block
      var currentBlock = context.mCurrentBlock;
      currentBlock.mBlockType = BlockType.Selection;
      currentBlock.mMergePoint = mergePoint;

      // Branch to the true or false block depending on the value of the left op.
      // Logical Or/And are the same except for which branch they short-circuit on so
      // simply flip the true/false blocks to differentiate between them.
      var selectControlMask = translator.CreateConstantLiteral<uint>((uint)Spv.SelectionControlMask.SelectionControlMaskNone);
      translator.CreateOp(currentBlock, OpInstructionType.OpSelectionMerge, null, new List<IShaderIR> { mergePoint, selectControlMask });
      if (isOr)
        translator.CreateOp(currentBlock, OpInstructionType.OpBranchConditional, null, new List<IShaderIR> { leftOp, ifTrue, ifFalse });
      else
        translator.CreateOp(currentBlock, OpInstructionType.OpBranchConditional, null, new List<IShaderIR> { leftOp, ifFalse, ifTrue });


      // In the true condition of a LogicalOr, we don't have to walk the left
      // hand side so simply store the result into the temp variable and branch to the merge point.
      context.mCurrentBlock = ifTrue;
      context.mCurrentFunction.mBlocks.Add(ifTrue);
      translator.CreateStoreOp(ifTrue, temp, leftOp);
      translator.CreateOp(ifTrue, OpInstructionType.OpBranch, null, new List<IShaderIR> { mergePoint });

      // In the false block we have to evaluate the right hand side. If there are nested Ors/Ands they will each
      // generate an if/else chain that has to be evaluated but without actually evaluating a side that needs to be short circuited.
      context.mCurrentBlock = ifFalse;
      context.mCurrentFunction.mBlocks.Add(ifFalse);
      var rightIR = context.mCurrentPass.WalkAndGetResult(rhsExpression);

      // Walking the right hand side can change the current block
      currentBlock = context.mCurrentBlock;
      // Always store the result of the right hand side into our temp
      translator.CreateStoreOp(currentBlock, temp, rightIR);
      // And always branch to the merge point
      translator.CreateOp(currentBlock, OpInstructionType.OpBranch, null, new List<IShaderIR> { mergePoint });

      // Now continue control flow from the merge point with the result of this expression as our temporary result
      context.mCurrentBlock = mergePoint;
      context.mCurrentFunction.mBlocks.Add(mergePoint);
      context.Push(temp);
    }

    void LogicOrAndOp(FrontEndTranslator translator, ShaderType resultType, ShaderType lhsType, string opToken, ShaderType rhsType, OpInstructionType instructionType, bool isOr)
    {
      CreateComplexBinaryOpIntrinsic(new BinaryOpKey(lhsType, opToken, rhsType), (SyntaxNode lhsExpression, SyntaxNode rhsExpression, FrontEndContext context) =>
      {
        LogicalOrAnd(lhsExpression, rhsExpression, context, isOr);
        return context.Pop() as ShaderOp;
      });
    }

    void IncDecOp(FrontEndTranslator translator, ShaderType resultType, string opToken, ShaderType operandType, OpInstructionType instructionType)
    {
      CreateUnaryOpIntrinsic(new UnaryOpKey(opToken, operandType), (IShaderIR operandIR, FrontEndContext context) =>
      {
        var operandValueOp = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, operandIR);
        IShaderIR oneConstantOp = null;
        if(resultType.mBaseType == OpType.Int)
          oneConstantOp= translator.CreateConstantOp(operandType, 1);
        else if(resultType.mBaseType == OpType.Float)
          oneConstantOp = translator.CreateConstantOp(operandType, 1.0f);
        return translator.CreateOp(context.mCurrentBlock, instructionType, resultType, new List<IShaderIR> { operandValueOp, oneConstantOp });
      });
    }

    void AddCastIntrinsics(FrontEndTranslator translator)
    {
      var boolType = FindType(new TypeKey(typeof(bool)));
      var intType = FindType(new TypeKey(typeof(int)));
      var uintType = FindType(new TypeKey(typeof(uint)));
      var floatType = FindType(new TypeKey(typeof(float)));
      AddCastIntrinsics(boolType, intType, translator.CastIntToBool);
      AddCastIntrinsics(boolType, uintType, translator.CastUIntToBool);
      AddCastIntrinsics(boolType, floatType, translator.CastFloatToBool);

      AddCastIntrinsics(intType, boolType, translator.CastBoolToInt);
      AddCastIntrinsics(intType, uintType, translator.CastUIntToInt);
      AddCastIntrinsics(intType, floatType, translator.CastFloatToInt);

      AddCastIntrinsics(uintType, boolType, translator.CastBoolToUInt);
      AddCastIntrinsics(uintType, intType, translator.CastIntToUInt);
      AddCastIntrinsics(uintType, floatType, translator.CastFloatToUInt);

      AddCastIntrinsics(floatType, boolType, translator.CastBoolToFloat);
      AddCastIntrinsics(floatType, intType, translator.CastIntToFloat);
      AddCastIntrinsics(floatType, uintType, translator.CastUIntToFloat);
    }
  }
}
