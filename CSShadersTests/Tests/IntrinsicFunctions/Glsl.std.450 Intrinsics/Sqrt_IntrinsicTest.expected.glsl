#version 450

struct SqrtTest
{
    int empty_struct_member;
};

void SqrtTest_PreConstructor(SqrtTest self)
{
}

void SqrtTest_DefaultConstructor(SqrtTest self)
{
    SqrtTest_PreConstructor(self);
}

void Main(SqrtTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = sqrt(floatVal);
    vector2Val = sqrt(vector2Val);
    vector3Val = sqrt(vector3Val);
    vector4Val = sqrt(vector4Val);
}

void main()
{
    SqrtTest self;
    SqrtTest_DefaultConstructor(self);
    Main(self);
}

