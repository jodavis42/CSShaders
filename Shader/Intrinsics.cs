namespace Shader
{
  public struct Intrinsics
  {
    // Image Intrinsics ------------------------------------------------------
    // Sample an image with an implicit level of detail.
    [Shader.SplitSampledImageIntrinsicFunction("OpImageSampleImplicitLod")] public extern static Math.Vector4 SampleImplicitLod(Shader.FloatImage2d image, Shader.Sampler sampler, Math.Vector2 coordinates);
    // Sample an image with an implicit level of detail.
    [Shader.ImageIntrinsicFunction("OpImageSampleImplicitLod")] public extern static Math.Vector4 SampleImplicitLod(Shader.FloatSampledImage2d sampledImage, Math.Vector2 coordinates);

    // Sample an image with an explicit level of detail.
    [Shader.SplitSampledImageIntrinsicFunction("OpImageSampleExplicitLod", ImageOperands.Lod, 3)] public extern static Math.Vector4 SampleExplicitLod(Shader.FloatImage2d image, Shader.Sampler sampler, Math.Vector2 coordinates, float lod);
    // Sample an image with an explicit level of detail.
    [Shader.ImageIntrinsicFunction("OpImageSampleExplicitLod", ImageOperands.Lod, 2)] public extern static Math.Vector4 SampleExplicitLod(Shader.FloatSampledImage2d sampledImage, Math.Vector2 coordinates, float lod);

    // Conversion Intrinsics ------------------------------------------------------
    // Convert (value preserving) from floating point to signed integer, with round toward 0.0
    [Shader.SimpleIntrinsicFunction("OpConvertFToS")] public extern static int ConvertToInt(float value);
    // Convert (value preserving) from floating point to signed integer, with round toward 0.0
    [Shader.SimpleIntrinsicFunction("OpConvertFToS")] public extern static Math.Integer2 ConvertToInt(Math.Vector2 value);
    // Convert (value preserving) from floating point to signed integer, with round toward 0.0
    [Shader.SimpleIntrinsicFunction("OpConvertFToS")] public extern static Math.Integer3 ConvertToInt(Math.Vector3 value);
    // Convert (value preserving) from floating point to signed integer, with round toward 0.0
    [Shader.SimpleIntrinsicFunction("OpConvertFToS")] public extern static Math.Integer4 ConvertToInt(Math.Vector4 value);

    // Convert (value preserving) from signed integer to floating point.
    [Shader.SimpleIntrinsicFunction("OpConvertSToF")] public extern static float ConvertToFloat(int value);
    // Convert (value preserving) from signed integer to floating point.
    [Shader.SimpleIntrinsicFunction("OpConvertSToF")] public extern static Math.Vector2 ConvertToFloat(Math.Integer2 value);
    // Convert (value preserving) from signed integer to floating point.
    [Shader.SimpleIntrinsicFunction("OpConvertSToF")] public extern static Math.Vector3 ConvertToFloat(Math.Integer3 value);
    // Convert (value preserving) from signed integer to floating point.
    [Shader.SimpleIntrinsicFunction("OpConvertSToF")] public extern static Math.Vector4 ConvertToFloat(Math.Integer4 value);

    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static int BitcastAsInt(float value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Integer2 BitcastAsInt(Math.Vector2 value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Integer3 BitcastAsInt(Math.Vector3 value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Integer4 BitcastAsInt(Math.Vector4 value);

    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static float BitcastAsFloat(int value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Vector2 BitcastAsFloat(Math.Integer2 value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Vector3 BitcastAsFloat(Math.Integer3 value);
    // Bit pattern-preserving type conversion.
    [Shader.SimpleIntrinsicFunction("OpBitcast")] public extern static Math.Vector4 BitcastAsFloat(Math.Integer4 value);

    // Composite Intrinsics ------------------------------------------------------
    [Shader.SimpleIntrinsicFunction("OpTranspose")] public extern static Math.Float2x2 Transpose(Math.Float2x2 value);

    // Arithmetic Intrinsics ------------------------------------------------------
    [Shader.SimpleIntrinsicFunction("OpSNegate")] public extern static int Negate(int value);
    [Shader.SimpleIntrinsicFunction("OpSNegate")] public extern static Math.Integer2 Negate(Math.Integer2 value);
    [Shader.SimpleIntrinsicFunction("OpSNegate")] public extern static Math.Integer3 Negate(Math.Integer3 value);
    [Shader.SimpleIntrinsicFunction("OpSNegate")] public extern static Math.Integer4 Negate(Math.Integer4 value);

    [Shader.SimpleIntrinsicFunction("OpIAdd")] public extern static int Add(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpIAdd")] public extern static Math.Integer2 Add(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpIAdd")] public extern static Math.Integer3 Add(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpIAdd")] public extern static Math.Integer4 Add(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpISub")] public extern static int Subtract(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpISub")] public extern static Math.Integer2 Subtract(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpISub")] public extern static Math.Integer3 Subtract(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpISub")] public extern static Math.Integer4 Subtract(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpIMul")] public extern static int Multiply(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpIMul")] public extern static Math.Integer2 Multiply(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpIMul")] public extern static Math.Integer3 Multiply(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpIMul")] public extern static Math.Integer4 Multiply(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSDiv")] public extern static int Divide(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSDiv")] public extern static Math.Integer2 Divide(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSDiv")] public extern static Math.Integer3 Divide(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSDiv")] public extern static Math.Integer4 Divide(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSRem")] public extern static int Remainder(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSRem")] public extern static Math.Integer2 Remainder(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSRem")] public extern static Math.Integer3 Remainder(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSRem")] public extern static Math.Integer4 Remainder(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSMod")] public extern static int Modulus(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSMod")] public extern static Math.Integer2 Modulus(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSMod")] public extern static Math.Integer3 Modulus(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSMod")] public extern static Math.Integer4 Modulus(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFNegate")] public extern static float Negate(float value);
    [Shader.SimpleIntrinsicFunction("OpFNegate")] public extern static Math.Vector2 Negate(Math.Vector2 value);
    [Shader.SimpleIntrinsicFunction("OpFNegate")] public extern static Math.Vector3 Negate(Math.Vector3 value);
    [Shader.SimpleIntrinsicFunction("OpFNegate")] public extern static Math.Vector4 Negate(Math.Vector4 value);

    [Shader.SimpleIntrinsicFunction("OpFAdd")] public extern static float Add(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector2 Add(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector3 Add(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector4 Add(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFSub")] public extern static float Subtract(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector2 Subtract(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector3 Subtract(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector4 Subtract(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFMul")] public extern static float Multiply(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector2 Multiply(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector3 Multiply(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector4 Multiply(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFDiv")] public extern static float Divide(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector2 Divide(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector3 Divide(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector4 Divide(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFRem")] public extern static float Remainder(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFRem")] public extern static Math.Vector2 Remainder(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFRem")] public extern static Math.Vector3 Remainder(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFRem")] public extern static Math.Vector4 Remainder(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFMod")] public extern static float Modulus(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector2 Modulus(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector3 Modulus(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector4 Modulus(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpVectorTimesScalar")] public extern static Math.Vector2 Multiply(Math.Vector2 lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpVectorTimesScalar")] public extern static Math.Vector3 Multiply(Math.Vector3 lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpVectorTimesScalar")] public extern static Math.Vector4 Multiply(Math.Vector4 lhs, float rhs);

    [Shader.SimpleIntrinsicFunction("OpMatrixTimesScalar")] public extern static Math.Float2x2 Multiply(Math.Float2x2 lhs, float rhs);

    [Shader.SimpleIntrinsicFunction("OpVectorTimesMatrix")] public extern static Math.Vector2 Multiply(Math.Vector2 lhs, Math.Float2x2 rhs);

    [Shader.SimpleIntrinsicFunction("OpMatrixTimesVector")] public extern static Math.Vector2 Multiply(Math.Float2x2 lhs, Math.Vector2 rhs);

    [Shader.SimpleIntrinsicFunction("OpMatrixTimesMatrix")] public extern static Math.Float2x2 Multiply(Math.Float2x2 lhs, Math.Float2x2 rhs);

    [Shader.SimpleIntrinsicFunction("OpOuterProduct")] public extern static Math.Float2x2 OuterProduct(Math.Vector2 lhs, Math.Vector2 rhs);

    [Shader.SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector4 lhs, Math.Vector4 rhs);

    // Bit Intrinsics ------------------------------------------------------
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftRightLogical")] public extern static int ShiftRightLogical(int baseValue, int shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftRightLogical")] public extern static Math.Integer2 ShiftRightLogical(Math.Integer2 baseValue, Math.Integer2 shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftRightLogical")] public extern static Math.Integer3 ShiftRightLogical(Math.Integer3 baseValue, Math.Integer3 shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftRightLogical")] public extern static Math.Integer4 ShiftRightLogical(Math.Integer4 baseValue, Math.Integer4 shift);

    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be filled with the sign bit from 'Base'
    [Shader.SimpleIntrinsicFunction("OpShiftRightArithmetic")] public extern static int ShiftRightArithmetic(int baseValue, int shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be filled with the sign bit from 'Base'
    [Shader.SimpleIntrinsicFunction("OpShiftRightArithmetic")] public extern static Math.Integer2 ShiftRightArithmetic(Math.Integer2 baseValue, Math.Integer2 shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be filled with the sign bit from 'Base'
    [Shader.SimpleIntrinsicFunction("OpShiftRightArithmetic")] public extern static Math.Integer3 ShiftRightArithmetic(Math.Integer3 baseValue, Math.Integer3 shift);
    // Shift the bits in 'Base' right by the number of bits specified in 'Shift'. The most-significant bits will be filled with the sign bit from 'Base'
    [Shader.SimpleIntrinsicFunction("OpShiftRightArithmetic")] public extern static Math.Integer4 ShiftRightArithmetic(Math.Integer4 baseValue, Math.Integer4 shift);

    // Shift the bits in 'Base' left by the number of bits specified in 'Shift'. The least-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftLeftLogical")] public extern static int ShiftLeftLogical(int baseValue, int shift);
    // Shift the bits in 'Base' left by the number of bits specified in 'Shift'. The least-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftLeftLogical")] public extern static Math.Integer2 ShiftLeftLogical(Math.Integer2 baseValue, Math.Integer2 shift);
    // Shift the bits in 'Base' left by the number of bits specified in 'Shift'. The least-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftLeftLogical")] public extern static Math.Integer3 ShiftLeftLogical(Math.Integer3 baseValue, Math.Integer3 shift);
    // Shift the bits in 'Base' left by the number of bits specified in 'Shift'. The least-significant bits will be zero filled.
    [Shader.SimpleIntrinsicFunction("OpShiftLeftLogical")] public extern static Math.Integer4 ShiftLeftLogical(Math.Integer4 baseValue, Math.Integer4 shift);

    [Shader.SimpleIntrinsicFunction("OpBitwiseOr")] public extern static int BitwiseOr(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseOr")] public extern static Math.Integer2 BitwiseOr(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseOr")] public extern static Math.Integer3 BitwiseOr(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseOr")] public extern static Math.Integer4 BitwiseOr(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpBitwiseXor")] public extern static int BitwiseXor(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseXor")] public extern static Math.Integer2 BitwiseXor(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseXor")] public extern static Math.Integer3 BitwiseXor(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseXor")] public extern static Math.Integer4 BitwiseXor(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpBitwiseAnd")] public extern static int BitwiseAnd(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseAnd")] public extern static Math.Integer2 BitwiseAnd(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseAnd")] public extern static Math.Integer3 BitwiseAnd(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpBitwiseAnd")] public extern static Math.Integer4 BitwiseAnd(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpNot")] public extern static int BitwiseNot(int value);
    [Shader.SimpleIntrinsicFunction("OpNot")] public extern static Math.Integer2 BitwiseNot(Math.Integer2 value);
    [Shader.SimpleIntrinsicFunction("OpNot")] public extern static Math.Integer3 BitwiseNot(Math.Integer3 value);
    [Shader.SimpleIntrinsicFunction("OpNot")] public extern static Math.Integer4 BitwiseNot(Math.Integer4 value);

    [Shader.SimpleIntrinsicFunction("OpBitReverse")] public extern static int BitReverse(int value);
    [Shader.SimpleIntrinsicFunction("OpBitReverse")] public extern static Math.Integer2 BitReverse(Math.Integer2 value);
    [Shader.SimpleIntrinsicFunction("OpBitReverse")] public extern static Math.Integer3 BitReverse(Math.Integer3 value);
    [Shader.SimpleIntrinsicFunction("OpBitReverse")] public extern static Math.Integer4 BitReverse(Math.Integer4 value);

    [Shader.SimpleIntrinsicFunction("OpBitCount")] public extern static int BitCount(int value);
    [Shader.SimpleIntrinsicFunction("OpBitCount")] public extern static Math.Integer2 BitCount(Math.Integer2 value);
    [Shader.SimpleIntrinsicFunction("OpBitCount")] public extern static Math.Integer3 BitCount(Math.Integer3 value);
    [Shader.SimpleIntrinsicFunction("OpBitCount")] public extern static Math.Integer4 BitCount(Math.Integer4 value);

    // Relational and Logical Intrinsics ------------------------------------------------------
    // Returns true if any values are true.
    [Shader.SimpleIntrinsicFunction("OpAny")] public extern static bool Any(Math.Bool2 value);
    // Returns true if any values are true.
    [Shader.SimpleIntrinsicFunction("OpAny")] public extern static bool Any(Math.Bool3 value);
    // Returns true if any values are true.
    [Shader.SimpleIntrinsicFunction("OpAny")] public extern static bool Any(Math.Bool4 value);

    // Returns true if all values are true.
    [Shader.SimpleIntrinsicFunction("OpAll")] public extern static bool All(Math.Bool2 value);
    // Returns true if all values are true.
    [Shader.SimpleIntrinsicFunction("OpAll")] public extern static bool All(Math.Bool3 value);
    // Returns true if all values are true.
    [Shader.SimpleIntrinsicFunction("OpAll")] public extern static bool All(Math.Bool4 value);

    // Requires Kernal capabilities
    //[Shader.SimpleIntrinsicFunction("OpSignBitSet")] public extern static bool SignBitSet(float value);
    // Requires Kernal capabilities
    //[Shader.SimpleIntrinsicFunction("OpSignBitSet")] public extern static Math.Bool2 SignBitSet(Math.Vector2 value);
    // Requires Kernal capabilities
    //[Shader.SimpleIntrinsicFunction("OpSignBitSet")] public extern static Math.Bool3 SignBitSet(Math.Vector3 value);
    // Requires Kernal capabilities
    //[Shader.SimpleIntrinsicFunction("OpSignBitSet")] public extern static Math.Bool4 SignBitSet(Math.Vector4 value);

    [Shader.SimpleIntrinsicFunction("OpLogicalEqual")] public extern static bool LogicalEqual(bool lhs, bool rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalEqual")] public extern static Math.Bool2 LogicalEqual(Math.Bool2 lhs, Math.Bool2 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalEqual")] public extern static Math.Bool3 LogicalEqual(Math.Bool3 lhs, Math.Bool3 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalEqual")] public extern static Math.Bool4 LogicalEqual(Math.Bool4 lhs, Math.Bool4 rhs);

    [Shader.SimpleIntrinsicFunction("OpLogicalNotEqual")] public extern static bool LogicalNotEqual(bool lhs, bool rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalNotEqual")] public extern static Math.Bool2 LogicalNotEqual(Math.Bool2 lhs, Math.Bool2 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalNotEqual")] public extern static Math.Bool3 LogicalNotEqual(Math.Bool3 lhs, Math.Bool3 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalNotEqual")] public extern static Math.Bool4 LogicalNotEqual(Math.Bool4 lhs, Math.Bool4 rhs);

    [Shader.SimpleIntrinsicFunction("OpLogicalOr")] public extern static bool LogicalOr(bool lhs, bool rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalOr")] public extern static Math.Bool2 LogicalOr(Math.Bool2 lhs, Math.Bool2 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalOr")] public extern static Math.Bool3 LogicalOr(Math.Bool3 lhs, Math.Bool3 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalOr")] public extern static Math.Bool4 LogicalOr(Math.Bool4 lhs, Math.Bool4 rhs);

    [Shader.SimpleIntrinsicFunction("OpLogicalAnd")] public extern static bool LogicalAnd(bool lhs, bool rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalAnd")] public extern static Math.Bool2 LogicalAnd(Math.Bool2 lhs, Math.Bool2 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalAnd")] public extern static Math.Bool3 LogicalAnd(Math.Bool3 lhs, Math.Bool3 rhs);
    [Shader.SimpleIntrinsicFunction("OpLogicalAnd")] public extern static Math.Bool4 LogicalAnd(Math.Bool4 lhs, Math.Bool4 rhs);

    [Shader.SimpleIntrinsicFunction("OpLogicalNot")] public extern static bool LogicalNot(bool value);
    [Shader.SimpleIntrinsicFunction("OpLogicalNot")] public extern static Math.Bool2 LogicalNot(Math.Bool2 value);
    [Shader.SimpleIntrinsicFunction("OpLogicalNot")] public extern static Math.Bool3 LogicalNot(Math.Bool3 value);
    [Shader.SimpleIntrinsicFunction("OpLogicalNot")] public extern static Math.Bool4 LogicalNot(Math.Bool4 value);

    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static bool Select(bool condition, bool trueResult, bool falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Bool2 Select(Math.Bool2 condition, Math.Bool2 trueResult, Math.Bool2 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Bool3 Select(Math.Bool3 condition, Math.Bool3 trueResult, Math.Bool3 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Bool4 Select(Math.Bool4 condition, Math.Bool4 trueResult, Math.Bool4 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static int Select(bool condition, int trueResult, int falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Integer2 Select(Math.Bool2 condition, Math.Integer2 trueResult, Math.Integer2 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Integer3 Select(Math.Bool3 condition, Math.Integer3 trueResult, Math.Integer3 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Integer4 Select(Math.Bool4 condition, Math.Integer4 trueResult, Math.Integer4 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static float Select(bool condition, float trueResult, float falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Vector2 Select(Math.Bool2 condition, Math.Vector2 trueResult, Math.Vector2 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Vector3 Select(Math.Bool3 condition, Math.Vector3 trueResult, Math.Vector3 falseResult);
    [Shader.SimpleIntrinsicFunction("OpSelect")] public extern static Math.Vector4 Select(Math.Bool4 condition, Math.Vector4 trueResult, Math.Vector4 falseResult);

    [Shader.SimpleIntrinsicFunction("OpIEqual")] public extern static bool Equal(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpIEqual")] public extern static Math.Bool2 Equal(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpIEqual")] public extern static Math.Bool3 Equal(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpIEqual")] public extern static Math.Bool4 Equal(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpINotEqual")] public extern static bool NotEqual(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpINotEqual")] public extern static Math.Bool2 NotEqual(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpINotEqual")] public extern static Math.Bool3 NotEqual(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpINotEqual")] public extern static Math.Bool4 NotEqual(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSGreaterThan")] public extern static bool GreaterThan(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThan")] public extern static Math.Bool2 GreaterThan(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThan")] public extern static Math.Bool3 GreaterThan(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThan")] public extern static Math.Bool4 GreaterThan(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSGreaterThanEqual")] public extern static bool GreaterThanEqual(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThanEqual")] public extern static Math.Bool2 GreaterThanEqual(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThanEqual")] public extern static Math.Bool3 GreaterThanEqual(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSGreaterThanEqual")] public extern static Math.Bool4 GreaterThanEqual(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSLessThan")] public extern static bool LessThan(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThan")] public extern static Math.Bool2 LessThan(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThan")] public extern static Math.Bool3 LessThan(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThan")] public extern static Math.Bool4 LessThan(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpSLessThanEqual")] public extern static bool LessThanEqual(int lhs, int rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThanEqual")] public extern static Math.Bool2 LessThanEqual(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThanEqual")] public extern static Math.Bool3 LessThanEqual(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleIntrinsicFunction("OpSLessThanEqual")] public extern static Math.Bool4 LessThanEqual(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdEqual")] public extern static bool Equal(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdEqual")] public extern static Math.Bool2 Equal(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdEqual")] public extern static Math.Bool3 Equal(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdEqual")] public extern static Math.Bool4 Equal(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdNotEqual")] public extern static bool NotEqual(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdNotEqual")] public extern static Math.Bool2 NotEqual(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdNotEqual")] public extern static Math.Bool3 NotEqual(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdNotEqual")] public extern static Math.Bool4 NotEqual(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThan")] public extern static bool GreaterThan(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThan")] public extern static Math.Bool2 GreaterThan(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThan")] public extern static Math.Bool3 GreaterThan(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThan")] public extern static Math.Bool4 GreaterThan(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThanEqual")] public extern static bool GreaterThanEqual(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThanEqual")] public extern static Math.Bool2 GreaterThanEqual(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThanEqual")] public extern static Math.Bool3 GreaterThanEqual(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdGreaterThanEqual")] public extern static Math.Bool4 GreaterThanEqual(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdLessThan")] public extern static bool LessThan(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThan")] public extern static Math.Bool2 LessThan(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThan")] public extern static Math.Bool3 LessThan(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThan")] public extern static Math.Bool4 LessThan(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleIntrinsicFunction("OpFOrdLessThanEqual")] public extern static bool LessThanEqual(float lhs, float rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThanEqual")] public extern static Math.Bool2 LessThanEqual(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThanEqual")] public extern static Math.Bool3 LessThanEqual(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleIntrinsicFunction("OpFOrdLessThanEqual")] public extern static Math.Bool4 LessThanEqual(Math.Vector4 lhs, Math.Vector4 rhs);

    // Derivative Intrinsics ------------------------------------------------------
    // The partial derivative of 'value' with respect to the window x coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdx")] public extern static float Ddx(float value);
    // The partial derivative of 'value' with respect to the window x coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdx")] public extern static Math.Vector2 Ddx(Math.Vector2 value);
    // The partial derivative of 'value' with respect to the window x coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdx")] public extern static Math.Vector3 Ddx(Math.Vector3 value);
    // The partial derivative of 'value' with respect to the window x coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdx")] public extern static Math.Vector4 Ddx(Math.Vector4 value);

    // The partial derivative of 'value' with respect to the window y coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdy")] public extern static float Ddy(float value);
    // The partial derivative of 'value' with respect to the window y coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdy")] public extern static Math.Vector2 Ddy(Math.Vector2 value);
    // The partial derivative of 'value' with respect to the window y coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdy")] public extern static Math.Vector3 Ddy(Math.Vector3 value);
    // The partial derivative of 'value' with respect to the window y coordinate
    [Shader.SimpleIntrinsicFunction("OpDPdy")] public extern static Math.Vector4 Ddy(Math.Vector4 value);

    // The same as computing the sum of the absolute values of Ddx and Ddy
    [Shader.SimpleIntrinsicFunction("OpFwidth")] public extern static float Fwidth(float value);
    // The same as computing the sum of the absolute values of Ddx and Ddy
    [Shader.SimpleIntrinsicFunction("OpFwidth")] public extern static Math.Vector2 Fwidth(Math.Vector2 value);
    // The same as computing the sum of the absolute values of Ddx and Ddy
    [Shader.SimpleIntrinsicFunction("OpFwidth")] public extern static Math.Vector3 Fwidth(Math.Vector3 value);
    // The same as computing the sum of the absolute values of Ddx and Ddy
    [Shader.SimpleIntrinsicFunction("OpFwidth")] public extern static Math.Vector4 Fwidth(Math.Vector4 value);

    // Extension Intrinsics: Glsl ------------------------------------------------------
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Round")] public extern static float Round(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Round")] public extern static Math.Vector2 Round(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Round")] public extern static Math.Vector3 Round(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Round")] public extern static Math.Vector4 Round(Math.Vector4 value);

    // Result is the value equal to the nearest whole number to x.
    // A fractional part of 0.5 rounds toward the nearest even whole number. (Both 3.5 and 4.5 for x round to 4.0.)
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "RoundEven")] public extern static float RoundEven(float value);
    // Result is the value equal to the nearest whole number to x.
    // A fractional part of 0.5 rounds toward the nearest even whole number. (Both 3.5 and 4.5 for x round to 4.0.)
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "RoundEven")] public extern static Math.Vector2 RoundEven(Math.Vector2 value);
    // Result is the value equal to the nearest whole number to x.
    // A fractional part of 0.5 rounds toward the nearest even whole number. (Both 3.5 and 4.5 for x round to 4.0.)
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "RoundEven")] public extern static Math.Vector3 RoundEven(Math.Vector3 value);
    // Result is the value equal to the nearest whole number to x.
    // A fractional part of 0.5 rounds toward the nearest even whole number. (Both 3.5 and 4.5 for x round to 4.0.)
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "RoundEven")] public extern static Math.Vector4 RoundEven(Math.Vector4 value);

    // Result is the value equal to the nearest whole number to x whose absolute value is not larger than the absolute value of x.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Trunc")] public extern static float Trunc(float value);
    // Result is the value equal to the nearest whole number to x whose absolute value is not larger than the absolute value of x.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Trunc")] public extern static Math.Vector2 Trunc(Math.Vector2 value);
    // Result is the value equal to the nearest whole number to x whose absolute value is not larger than the absolute value of x.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Trunc")] public extern static Math.Vector3 Trunc(Math.Vector3 value);
    // Result is the value equal to the nearest whole number to x whose absolute value is not larger than the absolute value of x.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Trunc")] public extern static Math.Vector4 Trunc(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FAbs")] public extern static float Abs(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FAbs")] public extern static Math.Vector2 Abs(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FAbs")] public extern static Math.Vector3 Abs(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FAbs")] public extern static Math.Vector4 Abs(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SAbs")] public extern static int Abs(int value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SAbs")] public extern static Math.Integer2 Abs(Math.Integer2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SAbs")] public extern static Math.Integer3 Abs(Math.Integer3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SAbs")] public extern static Math.Integer4 Abs(Math.Integer4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FSign")] public extern static float Sign(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FSign")] public extern static Math.Vector2 Sign(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FSign")] public extern static Math.Vector3 Sign(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FSign")] public extern static Math.Vector4 Sign(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SSign")] public extern static int Sign(int value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SSign")] public extern static Math.Integer2 Sign(Math.Integer2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SSign")] public extern static Math.Integer3 Sign(Math.Integer3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SSign")] public extern static Math.Integer4 Sign(Math.Integer4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Floor")] public extern static float Floor(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Floor")] public extern static Math.Vector2 Floor(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Floor")] public extern static Math.Vector3 Floor(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Floor")] public extern static Math.Vector4 Floor(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Ceil")] public extern static float Ceil(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Ceil")] public extern static Math.Vector2 Ceil(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Ceil")] public extern static Math.Vector3 Ceil(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Ceil")] public extern static Math.Vector4 Ceil(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fract")] public extern static float Fract(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fract")] public extern static Math.Vector2 Fract(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fract")] public extern static Math.Vector3 Fract(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fract")] public extern static Math.Vector4 Fract(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Radians")] public extern static float Radians(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Radians")] public extern static Math.Vector2 Radians(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Radians")] public extern static Math.Vector3 Radians(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Radians")] public extern static Math.Vector4 Radians(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Degrees")] public extern static float Degrees(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Degrees")] public extern static Math.Vector2 Degrees(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Degrees")] public extern static Math.Vector3 Degrees(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Degrees")] public extern static Math.Vector4 Degrees(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static float Sin(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static Math.Vector2 Sin(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static Math.Vector3 Sin(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static Math.Vector4 Sin(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static float Cos(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static Math.Vector2 Cos(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static Math.Vector3 Cos(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static Math.Vector4 Cos(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tan")] public extern static float Tan(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tan")] public extern static Math.Vector2 Tan(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tan")] public extern static Math.Vector3 Tan(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tan")] public extern static Math.Vector4 Tan(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asin")] public extern static float Asin(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asin")] public extern static Math.Vector2 Asin(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asin")] public extern static Math.Vector3 Asin(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asin")] public extern static Math.Vector4 Asin(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acos")] public extern static float Acos(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acos")] public extern static Math.Vector2 Acos(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acos")] public extern static Math.Vector3 Acos(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acos")] public extern static Math.Vector4 Acos(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan")] public extern static float Atan(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan")] public extern static Math.Vector2 Atan(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan")] public extern static Math.Vector3 Atan(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan")] public extern static Math.Vector4 Atan(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sinh")] public extern static float Sinh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sinh")] public extern static Math.Vector2 Sinh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sinh")] public extern static Math.Vector3 Sinh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sinh")] public extern static Math.Vector4 Sinh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cosh")] public extern static float Cosh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cosh")] public extern static Math.Vector2 Cosh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cosh")] public extern static Math.Vector3 Cosh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cosh")] public extern static Math.Vector4 Cosh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tanh")] public extern static float Tanh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tanh")] public extern static Math.Vector2 Tanh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tanh")] public extern static Math.Vector3 Tanh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Tanh")] public extern static Math.Vector4 Tanh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asinh")] public extern static float Asinh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asinh")] public extern static Math.Vector2 Asinh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asinh")] public extern static Math.Vector3 Asinh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Asinh")] public extern static Math.Vector4 Asinh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acosh")] public extern static float Acosh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acosh")] public extern static Math.Vector2 Acosh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acosh")] public extern static Math.Vector3 Acosh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Acosh")] public extern static Math.Vector4 Acosh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atanh")] public extern static float Atanh(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atanh")] public extern static Math.Vector2 Atanh(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atanh")] public extern static Math.Vector3 Atanh(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atanh")] public extern static Math.Vector4 Atanh(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan2")] public extern static float Atan2(float lhs, float rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan2")] public extern static Math.Vector2 Atan2(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan2")] public extern static Math.Vector3 Atan2(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Atan2")] public extern static Math.Vector4 Atan2(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Pow")] public extern static float Pow(float lhs, float rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Pow")] public extern static Math.Vector2 Pow(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Pow")] public extern static Math.Vector3 Pow(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Pow")] public extern static Math.Vector4 Pow(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp")] public extern static float Exp(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp")] public extern static Math.Vector2 Exp(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp")] public extern static Math.Vector3 Exp(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp")] public extern static Math.Vector4 Exp(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log")] public extern static float Log(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log")] public extern static Math.Vector2 Log(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log")] public extern static Math.Vector3 Log(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log")] public extern static Math.Vector4 Log(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp2")] public extern static float Exp2(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp2")] public extern static Math.Vector2 Exp2(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp2")] public extern static Math.Vector3 Exp2(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Exp2")] public extern static Math.Vector4 Exp2(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log2")] public extern static float Log2(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log2")] public extern static Math.Vector2 Log2(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log2")] public extern static Math.Vector3 Log2(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Log2")] public extern static Math.Vector4 Log2(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static float Sqrt(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static Math.Vector2 Sqrt(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static Math.Vector3 Sqrt(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static Math.Vector4 Sqrt(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "InverseSqrt")] public extern static float InverseSqrt(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "InverseSqrt")] public extern static Math.Vector2 InverseSqrt(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "InverseSqrt")] public extern static Math.Vector3 InverseSqrt(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "InverseSqrt")] public extern static Math.Vector4 InverseSqrt(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Determinant")] public extern static float Determinant(Math.Float2x2 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "MatrixInverse")] public extern static Math.Float2x2 Inverse(Math.Float2x2 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMin")] public extern static float Min(float lhs, float rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMin")] public extern static Math.Vector2 Min(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMin")] public extern static Math.Vector3 Min(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMin")] public extern static Math.Vector4 Min(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMin")] public extern static int Min(int lhs, int rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMin")] public extern static Math.Integer2 Min(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMin")] public extern static Math.Integer3 Min(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMin")] public extern static Math.Integer4 Min(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMax")] public extern static float Max(float lhs, float rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMax")] public extern static Math.Vector2 Max(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMax")] public extern static Math.Vector3 Max(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMax")] public extern static Math.Vector4 Max(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMax")] public extern static int Max(int lhs, int rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMax")] public extern static Math.Integer2 Max(Math.Integer2 lhs, Math.Integer2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMax")] public extern static Math.Integer3 Max(Math.Integer3 lhs, Math.Integer3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SMax")] public extern static Math.Integer4 Max(Math.Integer4 lhs, Math.Integer4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FClamp")] public extern static float Clamp(float value, float min, float max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FClamp")] public extern static Math.Vector2 Clamp(Math.Vector2 value, Math.Vector2 min, Math.Vector2 max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FClamp")] public extern static Math.Vector3 Clamp(Math.Vector3 value, Math.Vector3 min, Math.Vector3 max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FClamp")] public extern static Math.Vector4 Clamp(Math.Vector4 value, Math.Vector4 min, Math.Vector4 max);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SClamp")] public extern static int Clamp(int value, int min, int max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SClamp")] public extern static Math.Integer2 Clamp(Math.Integer2 value, Math.Integer2 min, Math.Integer2 max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SClamp")] public extern static Math.Integer3 Clamp(Math.Integer3 value, Math.Integer3 min, Math.Integer3 max);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SClamp")] public extern static Math.Integer4 Clamp(Math.Integer4 value, Math.Integer4 min, Math.Integer4 max);

    // Result is the linear interpolation between x and y, i.e., 'x * (1 - t) + y * t'.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMix")] public extern static float Lerp(float x, float y, float t);
    // Result is the linear interpolation between x and y, i.e., 'x * (1 - t) + y * t'.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMix")] public extern static Math.Vector2 Lerp(Math.Vector2 x, Math.Vector2 y, Math.Vector2 t);
    // Result is the linear interpolation between x and y, i.e., 'x * (1 - t) + y * t'.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMix")] public extern static Math.Vector3 Lerp(Math.Vector3 x, Math.Vector3 y, Math.Vector3 t);
    // Result is the linear interpolation between x and y, i.e., 'x * (1 - t) + y * t'.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FMix")] public extern static Math.Vector4 Lerp(Math.Vector4 x, Math.Vector4 y, Math.Vector4 t);

    // If y <= x then 1 is returned, otherwise 0 is returned.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Step")] public extern static float Step(float y, float x);
    // If y <= x then 1 is returned, otherwise 0 is returned.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Step")] public extern static Math.Vector2 Step(Math.Vector2 y, Math.Vector2 x);
    // If y <= x then 1 is returned, otherwise 0 is returned.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Step")] public extern static Math.Vector3 Step(Math.Vector3 y, Math.Vector3 x);
    // If y <= x then 1 is returned, otherwise 0 is returned.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Step")] public extern static Math.Vector4 Step(Math.Vector4 y, Math.Vector4 x);

    // Returns a smooth Hermite interpolation between 0 and 1 if x is in-between min and max.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SmoothStep")] public extern static float SmoothStep(float min, float max, float x);
    // Returns a smooth Hermite interpolation between 0 and 1 if x is in-between min and max.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SmoothStep")] public extern static Math.Vector2 SmoothStep(Math.Vector2 min, Math.Vector2 max, Math.Vector2 x);
    // Returns a smooth Hermite interpolation between 0 and 1 if x is in-between min and max.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SmoothStep")] public extern static Math.Vector3 SmoothStep(Math.Vector3 min, Math.Vector3 max, Math.Vector3 x);
    // Returns a smooth Hermite interpolation between 0 and 1 if x is in-between min and max.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "SmoothStep")] public extern static Math.Vector4 SmoothStep(Math.Vector4 min, Math.Vector4 max, Math.Vector4 x);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fma")] public extern static float FusedMultiplyAdd(float a, float b, float c);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fma")] public extern static Math.Vector2 FusedMultiplyAdd(Math.Vector2 a, Math.Vector2 b, Math.Vector2 c);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fma")] public extern static Math.Vector3 FusedMultiplyAdd(Math.Vector3 a, Math.Vector3 b, Math.Vector3 c);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Fma")] public extern static Math.Vector4 FusedMultiplyAdd(Math.Vector4 a, Math.Vector4 b, Math.Vector4 c);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Length")] public extern static float Length(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Length")] public extern static float Length(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Length")] public extern static float Length(Math.Vector4 value);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Distance")] public extern static float Distance(Math.Vector2 lhs, Math.Vector2 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Distance")] public extern static float Distance(Math.Vector3 lhs, Math.Vector3 rhs);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Distance")] public extern static float Distance(Math.Vector4 lhs, Math.Vector4 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cross")] public extern static Math.Vector3 Cross(Math.Vector3 lhs, Math.Vector3 rhs);

    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Normalize")] public extern static Math.Vector2 Normalize(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Normalize")] public extern static Math.Vector3 Normalize(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Normalize")] public extern static Math.Vector4 Normalize(Math.Vector4 value);

    // If the dot product of Nref and I is negative, the result is N, otherwise it is -N.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FaceForward")] public extern static Math.Vector2 FaceForward(Math.Vector2 N, Math.Vector2 I, Math.Vector2 Nref);
    // If the dot product of Nref and I is negative, the result is N, otherwise it is -N.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FaceForward")] public extern static Math.Vector3 FaceForward(Math.Vector3 N, Math.Vector3 I, Math.Vector3 Nref);
    // If the dot product of Nref and I is negative, the result is N, otherwise it is -N.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "FaceForward")] public extern static Math.Vector4 FaceForward(Math.Vector4 N, Math.Vector4 I, Math.Vector4 Nref);

    // For the incident vector I and surface orientation N, the result is the reflection direction: 'I- 2 * dot(N,I) * N'
    // This computation assumes N is already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Reflect")] public extern static Math.Vector2 Reflect(Math.Vector2 I, Math.Vector2 N);
    // For the incident vector I and surface orientation N, the result is the reflection direction: 'I- 2 * dot(N,I) * N'
    // This computation assumes N is already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Reflect")] public extern static Math.Vector3 Reflect(Math.Vector3 I, Math.Vector3 N);
    // For the incident vector I and surface orientation N, the result is the reflection direction: 'I- 2 * dot(N,I) * N'
    // This computation assumes N is already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Reflect")] public extern static Math.Vector4 Reflect(Math.Vector4 I, Math.Vector4 N);

    // For the incident vector I and surface normal N , and the ratio of indices of refraction eta, the result is the refraction vector.
    // The result is computed by 'k = 1.0 -eta * eta * (1.0 - dot(N,I) * dot(N,I))'
    // if k < 0.0 the result is 0.0
    // otherwise, the result is 'eta * I - (eta * dot(N,I) + sqrt(k)) * N'.
    // This computation assumes the input parameters for the incident vector I and the surface normal N are already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Refract")] public extern static Math.Vector2 Refract(Math.Vector2 I, Math.Vector2 N, float eta);
    // For the incident vector I and surface normal N , and the ratio of indices of refraction eta, the result is the refraction vector.
    // The result is computed by 'k = 1.0 -eta * eta * (1.0 - dot(N,I) * dot(N,I))'
    // if k < 0.0 the result is 0.0
    // otherwise, the result is 'eta * I - (eta * dot(N,I) + sqrt(k)) * N'.
    // This computation assumes the input parameters for the incident vector I and the surface normal N are already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Refract")] public extern static Math.Vector3 Refract(Math.Vector3 I, Math.Vector3 N, float eta);
    // For the incident vector I and surface normal N , and the ratio of indices of refraction eta, the result is the refraction vector.
    // The result is computed by 'k = 1.0 -eta * eta * (1.0 - dot(N,I) * dot(N,I))'
    // if k < 0.0 the result is 0.0
    // otherwise, the result is 'eta * I - (eta * dot(N,I) + sqrt(k)) * N'.
    // This computation assumes the input parameters for the incident vector I and the surface normal N are already normalized.
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Refract")] public extern static Math.Vector4 Refract(Math.Vector4 I, Math.Vector4 N, float eta);

  }
}
