#version 450

struct FAbsTest
{
    int empty_struct_member;
};

void FAbsTest_PreConstructor(FAbsTest self)
{
}

void FAbsTest_DefaultConstructor(FAbsTest self)
{
    FAbsTest_PreConstructor(self);
}

void Main(FAbsTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = abs(floatVal);
    vector2Val = abs(vector2Val);
    vector3Val = abs(vector3Val);
    vector4Val = abs(vector4Val);
}

void main()
{
    FAbsTest self;
    FAbsTest_DefaultConstructor(self);
    Main(self);
}

