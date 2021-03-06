#version 450

struct RoundEvenTest
{
    int empty_struct_member;
};

void RoundEvenTest_PreConstructor(RoundEvenTest self)
{
}

void RoundEvenTest_DefaultConstructor(RoundEvenTest self)
{
    RoundEvenTest_PreConstructor(self);
}

void Main(RoundEvenTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = roundEven(floatVal);
    vector2Val = roundEven(vector2Val);
    vector3Val = roundEven(vector3Val);
    vector4Val = roundEven(vector4Val);
}

void main()
{
    RoundEvenTest self;
    RoundEvenTest_DefaultConstructor(self);
    Main(self);
}

