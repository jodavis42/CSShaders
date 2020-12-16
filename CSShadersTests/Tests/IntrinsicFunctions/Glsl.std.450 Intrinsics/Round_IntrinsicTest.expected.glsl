#version 450

struct RoundTest
{
    int empty_struct_member;
};

void RoundTest_InitGlobals()
{
}

void RoundTest_PreConstructor(RoundTest self)
{
}

void RoundTest_DefaultConstructor(RoundTest self)
{
    RoundTest_PreConstructor(self);
}

void RoundTest_CopyInputs(RoundTest self)
{
}

void Main(RoundTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = round(floatVal);
    vector2Val = round(vector2Val);
    vector3Val = round(vector3Val);
    vector4Val = round(vector4Val);
}

void RoundTest_CopyOutputs(RoundTest self)
{
}

void main()
{
    RoundTest_InitGlobals();
    RoundTest self;
    RoundTest_DefaultConstructor(self);
    RoundTest_CopyInputs(self);
    Main(self);
    RoundTest_CopyOutputs(self);
}

