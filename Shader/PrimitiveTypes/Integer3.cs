namespace Math
{
  [VectorPrimitive(typeof(int), 3)]
  public struct Integer3
  {
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(2)] public float Z;
    [CompositeConstruct] public Integer3(int value) { X = value; Y = value; Z = value; }
    [CompositeConstruct] public Integer3(int x, int y, int z) { X = x; Y = y; Z = z; }
  }
}
