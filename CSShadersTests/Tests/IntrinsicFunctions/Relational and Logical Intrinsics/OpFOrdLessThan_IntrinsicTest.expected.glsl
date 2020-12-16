#version 450

struct OpFOrdLessThanTest
{
    int empty_struct_member;
};

void OpFOrdLessThanTest_InitGlobals()
{
}

void OpFOrdLessThanTest_PreConstructor(OpFOrdLessThanTest self)
{
}

void OpFOrdLessThanTest_DefaultConstructor(OpFOrdLessThanTest self)
{
    OpFOrdLessThanTest_PreConstructor(self);
}

void OpFOrdLessThanTest_CopyInputs(OpFOrdLessThanTest self)
{
}

void Main(OpFOrdLessThanTest self)
{
    bool boolVal = false;
    float floatVal = 0.0;
    bvec2 bool2Val = bvec2(false);
    vec2 vector2Val = vec2(0.0);
    bvec3 bool3Val = bvec3(false);
    vec3 vector3Val = vec3(0.0);
    bvec4 bool4Val = bvec4(false);
    vec4 vector4Val = vec4(0.0);
    boolVal = floatVal < floatVal;
    bool2Val = lessThan(vector2Val, vector2Val);
    bool3Val = lessThan(vector3Val, vector3Val);
    bool4Val = lessThan(vector4Val, vector4Val);
}

void OpFOrdLessThanTest_CopyOutputs(OpFOrdLessThanTest self)
{
}

void main()
{
    OpFOrdLessThanTest_InitGlobals();
    OpFOrdLessThanTest self;
    OpFOrdLessThanTest_DefaultConstructor(self);
    OpFOrdLessThanTest_CopyInputs(self);
    Main(self);
    OpFOrdLessThanTest_CopyOutputs(self);
}

