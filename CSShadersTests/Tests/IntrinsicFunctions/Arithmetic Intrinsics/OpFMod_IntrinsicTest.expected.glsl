#version 450

struct OpFModTest
{
    int empty_struct_member;
};

void OpFModTest_PreConstructor(OpFModTest self)
{
}

void OpFModTest_DefaultConstructor(OpFModTest self)
{
    OpFModTest_PreConstructor(self);
}

void Main(OpFModTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = mod(floatVal, floatVal);
    vector2Val = mod(vector2Val, vector2Val);
    vector3Val = mod(vector3Val, vector3Val);
    vector4Val = mod(vector4Val, vector4Val);
}

void main()
{
    OpFModTest self;
    OpFModTest_DefaultConstructor(self);
    Main(self);
}

