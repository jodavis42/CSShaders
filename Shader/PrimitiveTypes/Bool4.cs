namespace Math
{
  [VectorPrimitive(typeof(bool), 4)]
  public struct Bool4
  {
    [Swizzle(0)] public bool X;
    [Swizzle(1)] public bool Y;
    [Swizzle(2)] public bool Z;
    [Swizzle(3)] public bool W;

    [CompositeConstruct] public Bool4(bool value) { X = value; Y = value; Z = value; W = value; }
    [CompositeConstruct] public Bool4(bool x, bool y, bool z, bool w) { X = x; Y = y; Z = z; W = w; }
  }
}
