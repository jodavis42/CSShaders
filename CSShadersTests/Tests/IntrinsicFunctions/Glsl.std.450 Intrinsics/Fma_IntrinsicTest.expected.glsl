#version 450

struct FmaTest
{
    int empty_struct_member;
};

void FmaTest_PreConstructor(FmaTest self)
{
}

void FmaTest_DefaultConstructor(FmaTest self)
{
    FmaTest_PreConstructor(self);
}

void Main(FmaTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = fma(floatVal, floatVal, floatVal);
    vector2Val = fma(vector2Val, vector2Val, vector2Val);
    vector3Val = fma(vector3Val, vector3Val, vector3Val);
    vector4Val = fma(vector4Val, vector4Val, vector4Val);
}

void main()
{
    FmaTest self;
    FmaTest_DefaultConstructor(self);
    Main(self);
}

