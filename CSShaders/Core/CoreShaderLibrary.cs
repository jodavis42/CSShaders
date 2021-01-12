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
