namespace Math
{
  [VectorPrimitive(typeof(int), 2)]
  public struct Integer2
  {
    [CompositeConstruct] public Integer2(int value) { X = value; Y = value; }
    [CompositeConstruct] public Integer2(int x, int y) { X = x; Y = y; }
    [Swizzle(0)] public int X;
    [Swizzle(1)] public int Y;
    [Swizzle(0, 1)] public Integer2 XY { get { return this; } set { X = value.X; Y = value.Y; } }
    [Swizzle(1, 0)] public Integer2 YX { get { return new Integer2(Y, X); } set { X = value.Y; Y = value.X; } }
    [Swizzle(0, 0)] public Integer2 XX { get { return new Integer2(X, X); } }
    [Swizzle(1, 1)] public Integer2 YY { get { return new Integer2(Y, Y); } }
  }
}
