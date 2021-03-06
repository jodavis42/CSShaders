#version 450

struct FSignTest
{
    int empty_struct_member;
};

void FSignTest_PreConstructor(FSignTest self)
{
}

void FSignTest_DefaultConstructor(FSignTest self)
{
    FSignTest_PreConstructor(self);
}

void Main(FSignTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = sign(floatVal);
    vector2Val = sign(vector2Val);
    vector3Val = sign(vector3Val);
    vector4Val = sign(vector4Val);
}

void main()
{
    FSignTest self;
    FSignTest_DefaultConstructor(self);
    Main(self);
}

