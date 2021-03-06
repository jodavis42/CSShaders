#version 450

struct OpDPdxTest
{
    int empty_struct_member;
};

void OpDPdxTest_PreConstructor(OpDPdxTest self)
{
}

void OpDPdxTest_DefaultConstructor(OpDPdxTest self)
{
    OpDPdxTest_PreConstructor(self);
}

void Main(OpDPdxTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = dFdx(floatVal);
    vector2Val = dFdx(vector2Val);
    vector3Val = dFdx(vector3Val);
    vector4Val = dFdx(vector4Val);
}

void main()
{
    OpDPdxTest self;
    OpDPdxTest_DefaultConstructor(self);
    Main(self);
}

