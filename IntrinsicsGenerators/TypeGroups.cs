using System.Collections.Generic;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// A collection of types. Some types are grouped into lists for common batch operations (like vector types).
  /// </summary>
  class TypeGroups
  {
    public TypeName BoolType = new TypeName("bool");
    public TypeName Bool2Type = new TypeName("Math.Bool2");
    public TypeName Bool3Type = new TypeName("Math.Bool3");
    public TypeName Bool4Type = new TypeName("Math.Bool4");
    public TypeName IntType = new TypeName("int");
    public TypeName Int2Type = new TypeName("Math.Integer2");
    public TypeName Int3Type = new TypeName("Math.Integer3");
    public TypeName Int4Type = new TypeName("Math.Integer4");
    public TypeName FloatType = new TypeName("float");
    public TypeName Float2Type = new TypeName("Math.Vector2");
    public TypeName Float3Type = new TypeName("Math.Vector3");
    public TypeName Float4Type = new TypeName("Math.Vector4");
    public TypeName Float2x2Type = new TypeName("Math.Float2x2");
    public TypeName FloatSampledImage2dType = new TypeName("Shader.FloatSampledImage2d", true);


    public List<TypeName> BoolTypes;
    public List<TypeName> BoolVectorTypes;
    public List<TypeName> IntTypes;
    public List<TypeName> IntVectorTypes;
    public List<TypeName> FloatTypes;
    public List<TypeName> FloatVectorTypes;
    public List<TypeName> FloatMatrixTypes;
    public List<TypeName> SingleBoolList;
    public List<TypeName> SingleIntList;
    public List<TypeName> SingleFloatList;

    public TypeGroups()
    {
      BoolTypes = new List<TypeName>() { BoolType, Bool2Type, Bool3Type, Bool4Type };
      BoolVectorTypes = new List<TypeName>() { Bool2Type, Bool3Type, Bool4Type };
      IntTypes = new List<TypeName>() { IntType, Int2Type, Int3Type, Int4Type };
      IntVectorTypes = new List<TypeName>() { Int2Type, Int3Type, Int4Type };
      FloatTypes = new List<TypeName>() { FloatType, Float2Type, Float3Type, Float4Type };
      FloatVectorTypes = new List<TypeName>() { Float2Type, Float3Type, Float4Type };
      FloatMatrixTypes = new List<TypeName>() { Float2x2Type };
      SingleBoolList = new List<TypeName>() { BoolType, BoolType, BoolType };
      SingleIntList = new List<TypeName>() { IntType, IntType, IntType };
      SingleFloatList = new List<TypeName>() { FloatType, FloatType, FloatType };
    }
  }
}
