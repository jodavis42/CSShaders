#version 450

struct FractTest
{
    int empty_struct_member;
};

void FractTest_PreConstructor(FractTest self)
{
}

void FractTest_DefaultConstructor(FractTest self)
{
    FractTest_PreConstructor(self);
}

void Main(FractTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = fract(floatVal);
    vector2Val = fract(vector2Val);
    vector3Val = fract(vector3Val);
    vector4Val = fract(vector4Val);
}

void main()
{
    FractTest self;
    FractTest_DefaultConstructor(self);
    Main(self);
}

