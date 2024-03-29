#version 450

struct OpFwidthTest
{
    int empty_struct_member;
};

void OpFwidthTest_PreConstructor(OpFwidthTest self)
{
}

void OpFwidthTest_DefaultConstructor(OpFwidthTest self)
{
    OpFwidthTest_PreConstructor(self);
}

void Main(OpFwidthTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = fwidth(floatVal);
    vector2Val = fwidth(vector2Val);
    vector3Val = fwidth(vector3Val);
    vector4Val = fwidth(vector4Val);
}

void main()
{
    OpFwidthTest self;
    OpFwidthTest_DefaultConstructor(self);
    Main(self);
}

