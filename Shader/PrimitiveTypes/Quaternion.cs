namespace Math
{
  [VectorPrimitive(typeof(float), 4)]
  public struct Quaternion
  {
    [CompositeConstruct] public Quaternion(float x, float y, float z, float w) { X = x; Y = y; Z = z; W = w; }
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(2)] public float Z;
    [Swizzle(3)] public float W;
    [Swizzle(0, 1, 2, 3)] public Quaternion XYZW { get { return this; } set { X = value.X; Y = value.Y; Z = value.Z; W = value.W; } }
  }
}
