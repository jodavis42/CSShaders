#version 450

struct AsinhTest
{
    int empty_struct_member;
};

void AsinhTest_PreConstructor(AsinhTest self)
{
}

void AsinhTest_DefaultConstructor(AsinhTest self)
{
    AsinhTest_PreConstructor(self);
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

void main()
{
    AsinhTest self;
    AsinhTest_DefaultConstructor(self);
    Main(self);
}

