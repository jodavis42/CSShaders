#version 450

struct FMinTest
{
    int empty_struct_member;
};

void FMinTest_PreConstructor(FMinTest self)
{
}

void FMinTest_DefaultConstructor(FMinTest self)
{
    FMinTest_PreConstructor(self);
}

void Main(FMinTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = min(floatVal, floatVal);
    vector2Val = min(vector2Val, vector2Val);
    vector3Val = min(vector3Val, vector3Val);
    vector4Val = min(vector4Val, vector4Val);
}

void main()
{
    FMinTest self;
    FMinTest_DefaultConstructor(self);
    Main(self);
}

