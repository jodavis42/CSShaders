namespace Math
{
  [VectorPrimitive(typeof(float), 4)]
  public struct Vector4
  {
    [CompositeConstruct] public Vector4(float value) { X = value; Y = value; Z = value; W = value; }
    [CompositeConstruct] public Vector4(float x, float y, float z, float w) { X = x; Y = y; Z = z; W = w; }
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(2)] public float Z;
    [Swizzle(3)] public float W;
    [Swizzle(0, 1, 2, 3)] public Vector4 XYZW { get { return this; } set { X = value.X; Y = value.Y; Z = value.Z; W = value.W; } }
  }
}
