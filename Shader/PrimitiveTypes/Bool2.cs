namespace Math
{
  [VectorPrimitive(typeof(bool), 2)]
  public struct Bool2
  {
    [CompositeConstruct] public Bool2(bool value) { X = value; Y = value; }
    [CompositeConstruct] public Bool2(bool x, bool y) { X = x; Y = y; }
    [Swizzle(0)] public bool X;
    [Swizzle(1)] public bool Y;
    [Swizzle(0, 1)] public Bool2 XY { get { return this; } set { X = value.X; Y = value.Y; } }
    [Swizzle(1, 0)] public Bool2 YX { get { return new Bool2(Y, X); } set { X = value.Y; Y = value.X; } }
    [Swizzle(0, 0)] public Bool2 XX { get { return new Bool2(X, X); } }
    [Swizzle(1, 1)] public Bool2 YY { get { return new Bool2(Y, Y); } }
  }
}
