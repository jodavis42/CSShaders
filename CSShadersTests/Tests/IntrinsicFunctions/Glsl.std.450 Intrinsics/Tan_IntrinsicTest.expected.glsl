#version 450

struct TanTest
{
    int empty_struct_member;
};

void TanTest_PreConstructor(TanTest self)
{
}

void TanTest_DefaultConstructor(TanTest self)
{
    TanTest_PreConstructor(self);
}

void Main(TanTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = tan(floatVal);
    vector2Val = tan(vector2Val);
    vector3Val = tan(vector3Val);
    vector4Val = tan(vector4Val);
}

void main()
{
    TanTest self;
    TanTest_DefaultConstructor(self);
    Main(self);
}

