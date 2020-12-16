#version 450

struct SinTest
{
    int empty_struct_member;
};

void SinTest_InitGlobals()
{
}

void SinTest_PreConstructor(SinTest self)
{
}

void SinTest_DefaultConstructor(SinTest self)
{
    SinTest_PreConstructor(self);
}

void SinTest_CopyInputs(SinTest self)
{
}

void Main(SinTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = sin(floatVal);
    vector2Val = sin(vector2Val);
    vector3Val = sin(vector3Val);
    vector4Val = sin(vector4Val);
}

void SinTest_CopyOutputs(SinTest self)
{
}

void main()
{
    SinTest_InitGlobals();
    SinTest self;
    SinTest_DefaultConstructor(self);
    SinTest_CopyInputs(self);
    Main(self);
    SinTest_CopyOutputs(self);
}

