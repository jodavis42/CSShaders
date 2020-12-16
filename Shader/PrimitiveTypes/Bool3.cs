namespace Math
{
  [VectorPrimitive(typeof(bool), 3)]
  public struct Bool3
  {
    [Swizzle(0)] public bool X;
    [Swizzle(1)] public bool Y;
    [Swizzle(2)] public bool Z;

    [CompositeConstruct] public Bool3(bool value) { X = value; Y = value; Z = value; }
    [CompositeConstruct] public Bool3(bool x, bool y, bool z) { X = x; Y = y; Z = z; }
  }
}
