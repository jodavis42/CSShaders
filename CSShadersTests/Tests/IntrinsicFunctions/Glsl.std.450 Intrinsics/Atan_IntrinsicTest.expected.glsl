#version 450

struct AtanTest
{
    int empty_struct_member;
};

void AtanTest_PreConstructor(AtanTest self)
{
}

void AtanTest_DefaultConstructor(AtanTest self)
{
    AtanTest_PreConstructor(self);
}

void Main(AtanTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = atan(floatVal);
    vector2Val = atan(vector2Val);
    vector3Val = atan(vector3Val);
    vector4Val = atan(vector4Val);
}

void main()
{
    AtanTest self;
    AtanTest_DefaultConstructor(self);
    Main(self);
}

