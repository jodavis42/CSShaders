#version 450

struct FMaxTest
{
    int empty_struct_member;
};

void FMaxTest_PreConstructor(FMaxTest self)
{
}

void FMaxTest_DefaultConstructor(FMaxTest self)
{
    FMaxTest_PreConstructor(self);
}

void Main(FMaxTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = max(floatVal, floatVal);
    vector2Val = max(vector2Val, vector2Val);
    vector3Val = max(vector3Val, vector3Val);
    vector4Val = max(vector4Val, vector4Val);
}

void main()
{
    FMaxTest self;
    FMaxTest_DefaultConstructor(self);
    Main(self);
}

