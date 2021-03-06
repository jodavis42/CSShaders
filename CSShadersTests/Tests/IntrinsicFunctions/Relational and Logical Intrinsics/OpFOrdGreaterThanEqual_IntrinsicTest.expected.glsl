#version 450

struct OpFOrdGreaterThanEqualTest
{
    int empty_struct_member;
};

void OpFOrdGreaterThanEqualTest_PreConstructor(OpFOrdGreaterThanEqualTest self)
{
}

void OpFOrdGreaterThanEqualTest_DefaultConstructor(OpFOrdGreaterThanEqualTest self)
{
    OpFOrdGreaterThanEqualTest_PreConstructor(self);
}

void Main(OpFOrdGreaterThanEqualTest self)
{
    bool boolVal = false;
    float floatVal = 0.0;
    bvec2 bool2Val = bvec2(false);
    vec2 vector2Val = vec2(0.0);
    bvec3 bool3Val = bvec3(false);
    vec3 vector3Val = vec3(0.0);
    bvec4 bool4Val = bvec4(false);
    vec4 vector4Val = vec4(0.0);
    boolVal = floatVal >= floatVal;
    bool2Val = greaterThanEqual(vector2Val, vector2Val);
    bool3Val = greaterThanEqual(vector3Val, vector3Val);
    bool4Val = greaterThanEqual(vector4Val, vector4Val);
}

void main()
{
    OpFOrdGreaterThanEqualTest self;
    OpFOrdGreaterThanEqualTest_DefaultConstructor(self);
    Main(self);
}

