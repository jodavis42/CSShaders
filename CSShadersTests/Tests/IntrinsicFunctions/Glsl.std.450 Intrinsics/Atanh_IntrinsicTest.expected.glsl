#version 450

struct AtanhTest
{
    int empty_struct_member;
};

void AtanhTest_PreConstructor(AtanhTest self)
{
}

void AtanhTest_DefaultConstructor(AtanhTest self)
{
    AtanhTest_PreConstructor(self);
}

void Main(AtanhTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = atanh(floatVal);
    vector2Val = atanh(vector2Val);
    vector3Val = atanh(vector3Val);
    vector4Val = atanh(vector4Val);
}

void main()
{
    AtanhTest self;
    AtanhTest_DefaultConstructor(self);
    Main(self);
}

