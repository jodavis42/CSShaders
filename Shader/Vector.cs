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
    [Swizzle(0, 0)] public Vector2 XX { get { return new Vector2(X, X); }}
    [Swizzle(1, 1)] public Vector2 YY { get { return new Vector2(Y, Y); }}
  }

  [VectorPrimitive(typeof(float), 3)]
  public struct Vector3
  {
    [CompositeConstruct] public Vector3(float value) { X = value; Y = value; Z = value; }
    [CompositeConstruct] public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }
    [Swizzle(0)] public float X;
    [Swizzle(1)] public float Y;
    [Swizzle(2)] public float Z;
    [Swizzle(0, 0)] public Vector2 XX { get { return new Vector2(X, X); } }
    [Swizzle(0, 1)] public Vector2 XY { get { return new Vector2(X, Y); } set { X = value.X; Y = value.Y; } }
    [Swizzle(0, 2)] public Vector2 XZ { get { return new Vector2(X, Z); } set { X = value.X; Z = value.Y; } }
    [Swizzle(1, 0)] public Vector2 YX { get { return new Vector2(Y, X); } set { Y = value.X; X = value.Y; } }
    [Swizzle(1, 1)] public Vector2 YY { get { return new Vector2(Y, Y); } }
    [Swizzle(1, 2)] public Vector2 YZ { get { return new Vector2(Y, Z); } set { Y = value.X; Z = value.Y; } }
    [Swizzle(2, 0)] public Vector2 ZX { get { return new Vector2(Z, X); } set { Z = value.X; X = value.Y; } }
    [Swizzle(2, 1)] public Vector2 ZY { get { return new Vector2(Z, Y); } set { Z = value.X; Z = value.Y; } }
    [Swizzle(2, 2)] public Vector2 ZZ { get { return new Vector2(Z, Z); } }

    [Swizzle(0, 0, 0)] public Vector3 XXX { get { return new Vector3(X, X, X); } }
    [Swizzle(0, 0, 1)] public Vector3 XXY { get { return new Vector3(X, X, Y); } }
    [Swizzle(0, 0, 2)] public Vector3 XXZ { get { return new Vector3(X, X, Z); } }
    [Swizzle(0, 1, 0)] public Vector3 XYX { get { return new Vector3(X, Y, X); } }
    [Swizzle(0, 1, 1)] public Vector3 XYY { get { return new Vector3(X, Y, Y); } }
    [Swizzle(0, 1, 2)] public Vector3 XYZ { get { return new Vector3(X, Y, Z); } set { X = value.X; Y = value.Y; Z = value.Z; } }
    [Swizzle(0, 2, 0)] public Vector3 XZX { get { return new Vector3(X, Z, X); } }
    [Swizzle(0, 2, 1)] public Vector3 XZY { get { return new Vector3(X, Z, Y); } set { X = value.X; Z = value.Y; Y = value.Z; } }
    [Swizzle(0, 2, 2)] public Vector3 XZZ { get { return new Vector3(X, Z, Z); } }

    [Swizzle(1, 0, 0)] public Vector3 YXX { get { return new Vector3(Y, X, X); } }
    [Swizzle(1, 0, 1)] public Vector3 YXY { get { return new Vector3(Y, X, Y); } }
    [Swizzle(1, 0, 2)] public Vector3 YXZ { get { return new Vector3(Y, X, Z); } set { Y = value.X; X = value.Y; Z = value.Z; } }
    [Swizzle(1, 1, 0)] public Vector3 YYX { get { return new Vector3(Y, Y, X); } }
    [Swizzle(1, 1, 1)] public Vector3 YYY { get { return new Vector3(Y, Y, Y); } }
    [Swizzle(1, 1, 2)] public Vector3 YYZ { get { return new Vector3(Y, Y, Z); } }
    [Swizzle(1, 2, 0)] public Vector3 YZX { get { return new Vector3(Y, Z, X); } set { Y = value.X; Z = value.Y; X = value.Z; } }
    [Swizzle(1, 2, 1)] public Vector3 YZY { get { return new Vector3(Y, Z, Y); } }
    [Swizzle(1, 2, 2)] public Vector3 YZZ { get { return new Vector3(Y, Z, Z); } }

    [Swizzle(1, 0, 0)] public Vector3 ZXX { get { return new Vector3(Z, X, X); } }
    [Swizzle(1, 0, 1)] public Vector3 ZXY { get { return new Vector3(Z, X, Y); } set { Z = value.X; X = value.Y; Y = value.Z; } }
    [Swizzle(1, 0, 2)] public Vector3 ZXZ { get { return new Vector3(Z, X, Z); } }
    [Swizzle(1, 1, 0)] public Vector3 ZYX { get { return new Vector3(Z, Y, X); } set { Z = value.Z; Y = value.Y; X = value.X; } }
    [Swizzle(1, 1, 1)] public Vector3 ZYY { get { return new Vector3(Z, Y, Y); } }
    [Swizzle(1, 1, 2)] public Vector3 ZYZ { get { return new Vector3(Z, Y, Z); } }
    [Swizzle(1, 2, 0)] public Vector3 ZZX { get { return new Vector3(Z, Z, X); } }
    [Swizzle(1, 2, 1)] public Vector3 ZZY { get { return new Vector3(Z, Z, Y); } }
    [Swizzle(1, 2, 2)] public Vector3 ZZZ { get { return new Vector3(Z, Z, Z); } }
    public static Vector3 operator +(Vector3 lhs, Vector3 rhs) { return Shader.Intrinsics.Add(lhs, rhs); }
  }

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

  [VectorPrimitive(typeof(float), 2)]
  public struct ComplexNumber
  {
    [CompositeConstruct] public ComplexNumber(float real, float imaginary) { Real = real; Imaginary = imaginary; }
    [Swizzle(0)] public float Real;
    [Swizzle(1)] public float Imaginary;

    [Swizzle(0)] public float X { get { return Real; } set { Real = value; } }
    [Swizzle(0)] public float Y { get { return Imaginary; } set { Imaginary = value; } }
    [Swizzle(0, 1)] public ComplexNumber XY { get { return this; } set { Real = value.Real; Imaginary = value.Imaginary; } }
    [Swizzle(1, 0)] public ComplexNumber YX { get { return new ComplexNumber(Y, X); } set { Real = value.Y; Imaginary = value.X; } }
  }

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
