#version 450

struct RefractTest
{
    int empty_struct_member;
};

void RefractTest_PreConstructor(RefractTest self)
{
}

void RefractTest_DefaultConstructor(RefractTest self)
{
    RefractTest_PreConstructor(self);
}

void Main(RefractTest self)
{
    vec2 vector2Val = vec2(0.0);
    float floatVal = 0.0;
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val = refract(vector2Val, vector2Val, floatVal);
    vector3Val = refract(vector3Val, vector3Val, floatVal);
    vector4Val = refract(vector4Val, vector4Val, floatVal);
}

void main()
{
    RefractTest self;
    RefractTest_DefaultConstructor(self);
    Main(self);
}

