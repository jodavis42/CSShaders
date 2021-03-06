#version 450

struct InverseSqrtTest
{
    int empty_struct_member;
};

void InverseSqrtTest_PreConstructor(InverseSqrtTest self)
{
}

void InverseSqrtTest_DefaultConstructor(InverseSqrtTest self)
{
    InverseSqrtTest_PreConstructor(self);
}

void Main(InverseSqrtTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = inversesqrt(floatVal);
    vector2Val = inversesqrt(vector2Val);
    vector3Val = inversesqrt(vector3Val);
    vector4Val = inversesqrt(vector4Val);
}

void main()
{
    InverseSqrtTest self;
    InverseSqrtTest_DefaultConstructor(self);
    Main(self);
}

