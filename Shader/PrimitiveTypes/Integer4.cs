namespace Math
{
  [VectorPrimitive(typeof(int), 4)]
  public struct Integer4
  {
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(2)] public float Z;
    [Swizzle(3)] public float W;
    [CompositeConstruct] public Integer4(int value) { X = value; Y = value; Z = value; W = value; }
    [CompositeConstruct] public Integer4(int x, int y, int z, int w) { X = x; Y = y; Z = z; W = w; }
  }
}
