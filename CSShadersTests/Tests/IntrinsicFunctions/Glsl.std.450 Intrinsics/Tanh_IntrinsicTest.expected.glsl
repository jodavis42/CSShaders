#version 450

struct TanhTest
{
    int empty_struct_member;
};

void TanhTest_PreConstructor(TanhTest self)
{
}

void TanhTest_DefaultConstructor(TanhTest self)
{
    TanhTest_PreConstructor(self);
}

void Main(TanhTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = tanh(floatVal);
    vector2Val = tanh(vector2Val);
    vector3Val = tanh(vector3Val);
    vector4Val = tanh(vector4Val);
}

void main()
{
    TanhTest self;
    TanhTest_DefaultConstructor(self);
    Main(self);
}

