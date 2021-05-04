namespace Math
{
  [MatrixPrimitive(typeof(Vector2), 2)]
  public struct Float2x2
  {
    [Swizzle(0)] public Vector2 Column0;
    [Swizzle(1)] public Vector2 Column1;

    [Shader.SimpleIntrinsicFunction("OpCompositeConstruct")]
    public static Math.Float2x2 Construct(Math.Vector2 a, Math.Vector2 b)
    {
      var result = new Math.Float2x2();
      result.Column0 = a;
      result.Column1 = b;
      return result;
    }

    public Float2x2(float value)
    {
      var v2 = new Vector2(value);
      this = Float2x2.Construct(v2, v2);
    }

    public Float2x2(float m00, float m01, float m10, float m11)
    {
      this = Float2x2.Construct(new Vector2(m00, m10), new Vector2(m01, m11));
    }

    [CompositeConstruct] public Float2x2(Math.Vector2 a, Math.Vector2 b) { this.Column0 = a; this.Column1 = b; }

  }

  [MatrixPrimitive(typeof(Vector4), 4)]
  public struct Float4x4
  {
  }
}
