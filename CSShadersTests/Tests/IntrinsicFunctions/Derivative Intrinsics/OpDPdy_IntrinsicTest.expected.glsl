#version 450

struct OpDPdyTest
{
    int empty_struct_member;
};

void OpDPdyTest_InitGlobals()
{
}

void OpDPdyTest_PreConstructor(OpDPdyTest self)
{
}

void OpDPdyTest_DefaultConstructor(OpDPdyTest self)
{
    OpDPdyTest_PreConstructor(self);
}

void OpDPdyTest_CopyInputs(OpDPdyTest self)
{
}

void Main(OpDPdyTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = dFdy(floatVal);
    vector2Val = dFdy(vector2Val);
    vector3Val = dFdy(vector3Val);
    vector4Val = dFdy(vector4Val);
}

void OpDPdyTest_CopyOutputs(OpDPdyTest self)
{
}

void main()
{
    OpDPdyTest_InitGlobals();
    OpDPdyTest self;
    OpDPdyTest_DefaultConstructor(self);
    OpDPdyTest_CopyInputs(self);
    Main(self);
    OpDPdyTest_CopyOutputs(self);
}

