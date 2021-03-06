#version 450

struct OpFSubTest
{
    int empty_struct_member;
};

void OpFSubTest_PreConstructor(OpFSubTest self)
{
}

void OpFSubTest_DefaultConstructor(OpFSubTest self)
{
    OpFSubTest_PreConstructor(self);
}

void Main(OpFSubTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal -= floatVal;
    vector2Val -= vector2Val;
    vector3Val -= vector3Val;
    vector4Val -= vector4Val;
}

void main()
{
    OpFSubTest self;
    OpFSubTest_DefaultConstructor(self);
    Main(self);
}

