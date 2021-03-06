#version 450

struct OpDPdyTest
{
    int empty_struct_member;
};

void OpDPdyTest_PreConstructor(OpDPdyTest self)
{
}

void OpDPdyTest_DefaultConstructor(OpDPdyTest self)
{
    OpDPdyTest_PreConstructor(self);
}

void Main(OpDPdyTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = dFdy(floatVal);
    vector2Val = dFdy(vector2Val);
    vector3Val = dFdy(vector3Val);
    vector4Val = dFdy(vector4Val);
}

void main()
{
    OpDPdyTest self;
    OpDPdyTest_DefaultConstructor(self);
    Main(self);
}

