using System.Collections.Generic;
using System.IO;

namespace IntrinsicsGenerators
{
  

  class Program
  {

    static void Main(string[] args)
    {
      var typeGroups = new TypeGroups();
      var intrinsicGroups = GenerateIntrinsicDeclarations(typeGroups);

      // Write the intrinsics class
      var intrinsicsClassGenerator = new IntrinsicsClassGenerator();
      intrinsicsClassGenerator.Initialize();
      intrinsicsClassGenerator.Generate(intrinsicGroups);
      intrinsicsClassGenerator.Finish();
      var intrinsicsClassStr = intrinsicsClassGenerator.ToString();
      File.WriteAllText("Intrinsics.cs", intrinsicsClassStr);

      // Write the tests for each intrinsic function
      var testGenerator = new IntrinsicsTestGenerator();
      testGenerator.Generate(intrinsicGroups);
    }

    static List<TypeName> ListOf(string value, int count)
    {
      var result = new List<TypeName>();
      var valueTypeName = new TypeName(value);
      for (var i = 0; i < count; ++i)
        result.Add(valueTypeName);
      return result;
    }

    static List<IntrinsicGroup> GenerateIntrinsicDeclarations(TypeGroups typeGroups)
    {
      var intrinsicGroups = new List<IntrinsicGroup>();
      intrinsicGroups.Add(ImageIntrinsics.Generate(typeGroups));
      intrinsicGroups.Add(GenerateConversionIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateCompositeIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateArithmeticIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateBitIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateRelationalAndLogicalIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateDerivativeIntrinsics(typeGroups));
      intrinsicGroups.Add(GenerateGlslExtensionIntrinsics(typeGroups));

      return intrinsicGroups;
    }

    static IntrinsicGroup GenerateConversionIntrinsics(TypeGroups typeGroups)
    {
      var conversionIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Conversion" };
      conversionIntrinsicsGroup.GroupComment = "// Conversion Intrinsics ------------------------------------------------------";
      conversionIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpConvertFToS", "ConvertToInt"), typeGroups.IntTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("Convert (value preserving) from floating point to signed integer, with round toward 0.0");
      conversionIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpConvertSToF", "ConvertToFloat"), typeGroups.FloatTypes, typeGroups.IntTypes)
        .AddIntrinsicComment("Convert (value preserving) from signed integer to floating point.");
      conversionIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitcast", "BitcastAsInt"), typeGroups.IntTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("Bit pattern-preserving type conversion.");
      conversionIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitcast", "BitcastAsFloat"), typeGroups.FloatTypes, typeGroups.IntTypes)
        .AddIntrinsicComment("Bit pattern-preserving type conversion.");
      return conversionIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateCompositeIntrinsics(TypeGroups typeGroups)
    {
      var compositeIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Composite" };
      compositeIntrinsicsGroup.GroupComment = "// Composite Intrinsics ------------------------------------------------------";
      compositeIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpTranspose", "Transpose"), typeGroups.FloatMatrixTypes, typeGroups.FloatMatrixTypes);
      return compositeIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateArithmeticIntrinsics(TypeGroups typeGroups)
    {
      var arithmeticIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Arithmetic" };
      arithmeticIntrinsicsGroup.GroupComment = "// Arithmetic Intrinsics ------------------------------------------------------";
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSNegate", "Negate"), typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpIAdd", "Add"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpISub", "Subtract"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpIMul", "Multiply"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSDiv", "Divide"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSRem", "Remainder"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSMod", "Modulus"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);

      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFNegate", "Negate"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFAdd", "Add"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFSub", "Subtract"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFMul", "Multiply"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFDiv", "Divide"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFRem", "Remainder"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFMod", "Modulus"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);

      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpVectorTimesScalar", "Multiply"), typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes, ListOf("float", typeGroups.FloatVectorTypes.Count));
      arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpMatrixTimesScalar", "Multiply"), typeGroups.FloatMatrixTypes, typeGroups.FloatMatrixTypes, ListOf("float", typeGroups.FloatMatrixTypes.Count));

      var vectorTimesMatrixIntrinsic = arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpVectorTimesMatrix", "Multiply"));
      vectorTimesMatrixIntrinsic.AddSignature(typeGroups.Float2Type, typeGroups.Float2Type, "lhs", typeGroups.Float2x2Type, "rhs");

      var matrixTimesVectorIntrinsic = arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpMatrixTimesVector", "Multiply"));
      matrixTimesVectorIntrinsic.AddSignature(typeGroups.Float2Type, typeGroups.Float2x2Type, "lhs", typeGroups.Float2Type, "rhs");

      var matrixTimesMatrixIntrinsic = arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpMatrixTimesMatrix", "Multiply"));
      matrixTimesMatrixIntrinsic.AddSignature(typeGroups.Float2x2Type, typeGroups.Float2x2Type, "lhs", typeGroups.Float2x2Type, "rhs");

      var outerProductIntrinsic = arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpOuterProduct", "OuterProduct"));
      outerProductIntrinsic.AddSignature(typeGroups.Float2x2Type, typeGroups.Float2Type, "lhs", typeGroups.Float2Type, "rhs");

      var opDot = arithmeticIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpDot", "Dot"));
      foreach (var floatVectorType in typeGroups.FloatVectorTypes)
        opDot.AddSignature(typeGroups.FloatType, floatVectorType, "lhs", floatVectorType, "rhs");

      return arithmeticIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateBitIntrinsics(TypeGroups typeGroups)
    {
      var bitIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Bit" };
      bitIntrinsicsGroup.GroupComment = "// Bit Intrinsics ------------------------------------------------------";
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpShiftRightLogical", "ShiftRightLogical"), typeGroups.IntTypes, typeGroups.IntTypes, "baseValue", typeGroups.IntTypes, "shift")
        .AddIntrinsicComment("Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be zero filled.");
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpShiftRightArithmetic", "ShiftRightArithmetic"), typeGroups.IntTypes, typeGroups.IntTypes, "baseValue", typeGroups.IntTypes, "shift")
        .AddIntrinsicComment("Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be filled with the sign bit from 'Base'");
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpShiftLeftLogical", "ShiftLeftLogical"), typeGroups.IntTypes, typeGroups.IntTypes, "baseValue", typeGroups.IntTypes, "shift")
        .AddIntrinsicComment("Shift the bits in 'Base' left by the number of bits specified in 'Shift'. The least-significant bits will be zero filled.");
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitwiseOr", "BitwiseOr"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitwiseXor", "BitwiseXor"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitwiseAnd", "BitwiseAnd"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpNot", "BitwiseNot"), typeGroups.IntTypes, typeGroups.IntTypes);
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitReverse", "BitReverse"), typeGroups.IntTypes, typeGroups.IntTypes);
      bitIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpBitCount", "BitCount"), typeGroups.IntTypes, typeGroups.IntTypes);
      return bitIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateRelationalAndLogicalIntrinsics(TypeGroups typeGroups)
    {
      var relationalIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Relational and Logical" };
      relationalIntrinsicsGroup.GroupComment = "// Relational and Logical Intrinsics ------------------------------------------------------";
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpAny", "Any"), typeGroups.SingleBoolList, typeGroups.BoolVectorTypes)
        .AddIntrinsicComment("Returns true if any values are true.");
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpAll", "All"), typeGroups.SingleBoolList, typeGroups.BoolVectorTypes)
        .AddIntrinsicComment("Returns true if all values are true.");
      // Requires kernel
      var opSignBitSet = relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSignBitSet", "SignBitSet"));
      opSignBitSet.AddSignatures(typeGroups.BoolTypes, typeGroups.FloatTypes, "value");
      opSignBitSet.Disabled = true;
      opSignBitSet.Comments.Add("Requires Kernal capabilities");

      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpLogicalEqual", "LogicalEqual"), typeGroups.BoolTypes, typeGroups.BoolTypes, typeGroups.BoolTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpLogicalNotEqual", "LogicalNotEqual"), typeGroups.BoolTypes, typeGroups.BoolTypes, typeGroups.BoolTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpLogicalOr", "LogicalOr"), typeGroups.BoolTypes, typeGroups.BoolTypes, typeGroups.BoolTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpLogicalAnd", "LogicalAnd"), typeGroups.BoolTypes, typeGroups.BoolTypes, typeGroups.BoolTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpLogicalNot", "LogicalNot"), typeGroups.BoolTypes, typeGroups.BoolTypes);

      var opSelect = relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSelect", "Select"));
      opSelect.AddSignatures(typeGroups.BoolTypes, typeGroups.BoolTypes, "condition", typeGroups.BoolTypes, "trueResult", typeGroups.BoolTypes, "falseResult");
      opSelect.AddSignatures(typeGroups.IntTypes, typeGroups.BoolTypes, "condition", typeGroups.IntTypes, "trueResult", typeGroups.IntTypes, "falseResult");
      opSelect.AddSignatures(typeGroups.FloatTypes, typeGroups.BoolTypes, "condition", typeGroups.FloatTypes, "trueResult", typeGroups.FloatTypes, "falseResult");

      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpIEqual", "Equal"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpINotEqual", "NotEqual"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSGreaterThan", "GreaterThan"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSGreaterThanEqual", "GreaterThanEqual"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSLessThan", "LessThan"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpSLessThanEqual", "LessThanEqual"), typeGroups.BoolTypes, typeGroups.IntTypes, typeGroups.IntTypes);

      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdEqual", "Equal"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdNotEqual", "NotEqual"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdGreaterThan", "GreaterThan"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdGreaterThanEqual", "GreaterThanEqual"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdLessThan", "LessThan"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      relationalIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFOrdLessThanEqual", "LessThanEqual"), typeGroups.BoolTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      return relationalIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateDerivativeIntrinsics(TypeGroups typeGroups)
    {
      var derivativeIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Derivative" };
      derivativeIntrinsicsGroup.GroupComment = "// Derivative Intrinsics ------------------------------------------------------";
      derivativeIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpDPdx", "Ddx"), typeGroups.FloatTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("The partial derivative of 'value' with respect to the window x coordinate");
      derivativeIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpDPdy", "Ddy"), typeGroups.FloatTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("The partial derivative of 'value' with respect to the window y coordinate");
      derivativeIntrinsicsGroup.Build(new SimpleIntrinsicsFunctionDescription("OpFwidth", "Fwidth"), typeGroups.FloatTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("The same as computing the sum of the absolute values of Ddx and Ddy");
      return derivativeIntrinsicsGroup;
    }

    static IntrinsicGroup GenerateGlslExtensionIntrinsics(TypeGroups typeGroups)
    {
      var glslExtensionIntrinsicsGroup = new IntrinsicGroup() { GroupName = "Glsl.std.450" };
      glslExtensionIntrinsicsGroup.GroupComment = "// Extension Intrinsics: Glsl ------------------------------------------------------";
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Round", "Round"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("RoundEven", "RoundEven"), typeGroups.FloatTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("Result is the value equal to the nearest whole number to x.")
        .AddIntrinsicComment("A fractional part of 0.5 rounds toward the nearest even whole number. (Both 3.5 and 4.5 for x round to 4.0.)");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Trunc", "Trunc"), typeGroups.FloatTypes, typeGroups.FloatTypes)
        .AddIntrinsicComment("Result is the value equal to the nearest whole number to x whose absolute value is not larger than the absolute value of x.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FAbs", "Abs"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SAbs", "Abs"), typeGroups.IntTypes, typeGroups.IntTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FSign", "Sign"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SSign", "Sign"), typeGroups.IntTypes, typeGroups.IntTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Floor", "Floor"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Ceil", "Ceil"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Fract", "Fract"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Radians", "Radians"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Degrees", "Degrees"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Sin", "Sin"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Cos", "Cos"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Tan", "Tan"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Asin", "Asin"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Acos", "Acos"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Atan", "Atan"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Sinh", "Sinh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Cosh", "Cosh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Tanh", "Tanh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Asinh", "Asinh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Acosh", "Acosh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Atanh", "Atanh"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Atan2", "Atan2"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Pow", "Pow"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Exp", "Exp"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Log", "Log"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Exp2", "Exp2"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Log2", "Log2"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Sqrt", "Sqrt"), typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("InverseSqrt", "InverseSqrt"), typeGroups.FloatTypes, typeGroups.FloatTypes);

      var glslDeterminant = glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Determinant", "Determinant"));
      glslDeterminant.AddSignature(typeGroups.FloatType, typeGroups.Float2x2Type, "value");

      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("MatrixInverse", "Inverse"), typeGroups.FloatMatrixTypes, typeGroups.FloatMatrixTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FMin", "Min"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SMin", "Min"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FMax", "Max"), typeGroups.FloatTypes, typeGroups.FloatTypes, typeGroups.FloatTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SMax", "Max"), typeGroups.IntTypes, typeGroups.IntTypes, typeGroups.IntTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FClamp", "Clamp"), typeGroups.FloatTypes, typeGroups.FloatTypes, "value", typeGroups.FloatTypes, "min", typeGroups.FloatTypes, "max");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SClamp", "Clamp"), typeGroups.IntTypes, typeGroups.IntTypes, "value", typeGroups.IntTypes, "min", typeGroups.IntTypes, "max");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FMix", "Lerp"), typeGroups.FloatTypes, typeGroups.FloatTypes, "x", typeGroups.FloatTypes, "y", typeGroups.FloatTypes, "t")
        .AddIntrinsicComment("Result is the linear interpolation between x and y, i.e., 'x * (1 - t) + y * t'.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Step", "Step"), typeGroups.FloatTypes, typeGroups.FloatTypes, "y", typeGroups.FloatTypes, "x")
        .AddIntrinsicComment("If y <= x then 1 is returned, otherwise 0 is returned.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("SmoothStep", "SmoothStep"), typeGroups.FloatTypes, typeGroups.FloatTypes, "min", typeGroups.FloatTypes, "max", typeGroups.FloatTypes, "x")
        .AddIntrinsicComment("Returns a smooth Hermite interpolation between 0 and 1 if x is in-between min and max.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Fma", "FusedMultiplyAdd"), typeGroups.FloatTypes, typeGroups.FloatTypes, "a", typeGroups.FloatTypes, "b", typeGroups.FloatTypes, "c");

      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Length", "Length"), typeGroups.SingleFloatList, typeGroups.FloatVectorTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Distance", "Distance"), typeGroups.SingleFloatList, typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes);

      var crossIntrinsic = glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Cross", "Cross"));
      crossIntrinsic.AddSignature(typeGroups.Float3Type, typeGroups.Float3Type, "lhs", typeGroups.Float3Type, "rhs");

      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Normalize", "Normalize"), typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes);
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("FaceForward", "FaceForward"), typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes, "N", typeGroups.FloatVectorTypes, "I", typeGroups.FloatVectorTypes, "Nref")
        .AddIntrinsicComment("If the dot product of Nref and I is negative, the result is N, otherwise it is -N.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Reflect", "Reflect"), typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes, "I", typeGroups.FloatVectorTypes, "N")
        .AddIntrinsicComment("For the incident vector I and surface orientation N, the result is the reflection direction: 'I- 2 * dot(N,I) * N'")
        .AddIntrinsicComment("This computation assumes N is already normalized.");
      glslExtensionIntrinsicsGroup.Build(new GlslExtensionFunctionDescription("Refract", "Refract"), typeGroups.FloatVectorTypes, typeGroups.FloatVectorTypes, "I", typeGroups.FloatVectorTypes, "N", typeGroups.SingleFloatList, "eta")
        .AddIntrinsicComment("For the incident vector I and surface normal N , and the ratio of indices of refraction eta, the result is the refraction vector.")
        .AddIntrinsicComment("The result is computed by 'k = 1.0 -eta * eta * (1.0 - dot(N,I) * dot(N,I))'")
        .AddIntrinsicComment("if k < 0.0 the result is 0.0")
        .AddIntrinsicComment("otherwise, the result is 'eta * I - (eta * dot(N,I) + sqrt(k)) * N'.")
        .AddIntrinsicComment("This computation assumes the input parameters for the incident vector I and the surface normal N are already normalized.");

      return glslExtensionIntrinsicsGroup;
    }
  }
}
