namespace Shader
{
  public struct Intrinsics
  {
    [SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpDot")] public extern static float Dot(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector2 Add(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector3 Add(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpFAdd")] public extern static Math.Vector4 Add(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector2 Subtract(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector3 Subtract(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpFSub")] public extern static Math.Vector4 Subtract(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector2 Multiply(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector3 Multiply(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpFMul")] public extern static Math.Vector4 Multiply(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector2 Divide(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector3 Divide(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpFDiv")] public extern static Math.Vector4 Divide(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector2 Mod(Math.Vector2 lhs, Math.Vector2 rhs);
    [SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector3 Mod(Math.Vector3 lhs, Math.Vector3 rhs);
    [SimpleIntrinsicFunction("OpFMod")] public extern static Math.Vector4 Mod(Math.Vector4 lhs, Math.Vector4 rhs);
    [SimpleIntrinsicFunction("OpDPdx")] public extern static Math.Vector2 Ddx(Math.Vector2 value);
    [SimpleIntrinsicFunction("OpDPdy")] public extern static Math.Vector2 Ddy(Math.Vector2 value);

    [SimpleIntrinsicFunction("OpIAdd")] public extern static Math.Integer2 Add(Math.Integer2 lhs, Math.Integer2 rhs);
    [SimpleIntrinsicFunction("OpFOrdEqual")] public extern static Math.Bool2 Equal(Math.Vector2 lhs, Math.Vector2 rhs);

    // Matrix Intrinsics ------------------------------------------------------
    [Shader.SimpleIntrinsicFunction("OpTranspose")] public extern static Math.Float2x2 Transpose(Math.Float2x2 matrix);
    [Shader.SimpleIntrinsicFunction("OpMatrixTimesMatrix")] public extern static Math.Float2x2 Multiply(Math.Float2x2 lhs, Math.Float2x2 rhs);
    [Shader.SimpleIntrinsicFunction("OpMatrixTimesVector")] public extern static Math.Vector2 Multiply(Math.Float2x2 lhs, Math.Vector2 rhs);


    // Sampled Image Intrinsics -----------------------------------------------
    [Shader.ImageIntrinsicFunction("OpImageSampleImplicitLod")] public extern static Math.Vector4 Sample(FloatSampledImage2d sampledImage, Math.Vector2 coordinates);
    [Shader.ImageIntrinsicFunction("OpImageSampleExplicitLod", ImageOperands.Lod, 2)] public extern static Math.Vector4 Sample(FloatSampledImage2d sampledImage, Math.Vector2 coordinates, float lod);

    // Extension Intrinsics: Glsl ---------------------------------------------
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static float Sin(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static float Sin(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static float Sin(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sin")] public extern static float Sin(Math.Vector4 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static float Cos(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static float Cos(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static float Cos(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Cos")] public extern static float Cos(Math.Vector4 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static float Sqrt(float value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static float Sqrt(Math.Vector2 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static float Sqrt(Math.Vector3 value);
    [Shader.SimpleExtensionIntrinsic("GLSL.std.450", "Sqrt")] public extern static float Sqrt(Math.Vector4 value);
  }
}
