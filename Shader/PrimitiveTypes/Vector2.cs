namespace Math
{
  [VectorPrimitive(typeof(float), 2)]
  public struct Vector2
  {
    [CompositeConstruct] public Vector2(float value) { X = value; Y = value; }
    [CompositeConstruct] public Vector2(float x, float y) { X = x; Y = y; }
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(0, 1)] public Vector2 XY { get { return this; } set { X = value.X; Y = value.Y; } }
    [Swizzle(1, 0)] public Vector2 YX { get { return new Vector2(Y, X); } set { X = value.Y; Y = value.X; } }
    [Swizzle(0, 0)] public Vector2 XX { get { return new Vector2(X, X); } }
    [Swizzle(1, 1)] public Vector2 YY { get { return new Vector2(Y, Y); } }
  }
}
