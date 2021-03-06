#version 450

struct OpFOrdEqualTest
{
    int empty_struct_member;
};

void OpFOrdEqualTest_PreConstructor(OpFOrdEqualTest self)
{
}

void OpFOrdEqualTest_DefaultConstructor(OpFOrdEqualTest self)
{
    OpFOrdEqualTest_PreConstructor(self);
}

void Main(OpFOrdEqualTest self)
{
    bool boolVal = false;
    float floatVal = 0.0;
    bvec2 bool2Val = bvec2(false);
    vec2 vector2Val = vec2(0.0);
    bvec3 bool3Val = bvec3(false);
    vec3 vector3Val = vec3(0.0);
    bvec4 bool4Val = bvec4(false);
    vec4 vector4Val = vec4(0.0);
    boolVal = floatVal == floatVal;
    bool2Val = equal(vector2Val, vector2Val);
    bool3Val = equal(vector3Val, vector3Val);
    bool4Val = equal(vector4Val, vector4Val);
}

void main()
{
    OpFOrdEqualTest self;
    OpFOrdEqualTest_DefaultConstructor(self);
    Main(self);
}

