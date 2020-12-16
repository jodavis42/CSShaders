#version 450

struct OpDPdxTest
{
    int empty_struct_member;
};

void OpDPdxTest_InitGlobals()
{
}

void OpDPdxTest_PreConstructor(OpDPdxTest self)
{
}

void OpDPdxTest_DefaultConstructor(OpDPdxTest self)
{
    OpDPdxTest_PreConstructor(self);
}

void OpDPdxTest_CopyInputs(OpDPdxTest self)
{
}

void Main(OpDPdxTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = dFdx(floatVal);
    vector2Val = dFdx(vector2Val);
    vector3Val = dFdx(vector3Val);
    vector4Val = dFdx(vector4Val);
}

void OpDPdxTest_CopyOutputs(OpDPdxTest self)
{
}

void main()
{
    OpDPdxTest_InitGlobals();
    OpDPdxTest self;
    OpDPdxTest_DefaultConstructor(self);
    OpDPdxTest_CopyInputs(self);
    Main(self);
    OpDPdxTest_CopyOutputs(self);
}

