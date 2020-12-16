#version 450

struct OpFOrdNotEqualTest
{
    int empty_struct_member;
};

void OpFOrdNotEqualTest_InitGlobals()
{
}

void OpFOrdNotEqualTest_PreConstructor(OpFOrdNotEqualTest self)
{
}

void OpFOrdNotEqualTest_DefaultConstructor(OpFOrdNotEqualTest self)
{
    OpFOrdNotEqualTest_PreConstructor(self);
}

void OpFOrdNotEqualTest_CopyInputs(OpFOrdNotEqualTest self)
{
}

void Main(OpFOrdNotEqualTest self)
{
    bool boolVal = false;
    float floatVal = 0.0;
    bvec2 bool2Val = bvec2(false);
    vec2 vector2Val = vec2(0.0);
    bvec3 bool3Val = bvec3(false);
    vec3 vector3Val = vec3(0.0);
    bvec4 bool4Val = bvec4(false);
    vec4 vector4Val = vec4(0.0);
    boolVal = floatVal != floatVal;
    bool2Val = notEqual(vector2Val, vector2Val);
    bool3Val = notEqual(vector3Val, vector3Val);
    bool4Val = notEqual(vector4Val, vector4Val);
}

void OpFOrdNotEqualTest_CopyOutputs(OpFOrdNotEqualTest self)
{
}

void main()
{
    OpFOrdNotEqualTest_InitGlobals();
    OpFOrdNotEqualTest self;
    OpFOrdNotEqualTest_DefaultConstructor(self);
    OpFOrdNotEqualTest_CopyInputs(self);
    Main(self);
    OpFOrdNotEqualTest_CopyOutputs(self);
}

