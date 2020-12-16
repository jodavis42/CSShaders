#version 450

struct AsinhTest
{
    int empty_struct_member;
};

void AsinhTest_InitGlobals()
{
}

void AsinhTest_PreConstructor(AsinhTest self)
{
}

void AsinhTest_DefaultConstructor(AsinhTest self)
{
    AsinhTest_PreConstructor(self);
}

void AsinhTest_CopyInputs(AsinhTest self)
{
}

void Main(AsinhTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = asinh(floatVal);
    vector2Val = asinh(vector2Val);
    vector3Val = asinh(vector3Val);
    vector4Val = asinh(vector4Val);
}

void AsinhTest_CopyOutputs(AsinhTest self)
{
}

void main()
{
    AsinhTest_InitGlobals();
    AsinhTest self;
    AsinhTest_DefaultConstructor(self);
    AsinhTest_CopyInputs(self);
    Main(self);
    AsinhTest_CopyOutputs(self);
}

