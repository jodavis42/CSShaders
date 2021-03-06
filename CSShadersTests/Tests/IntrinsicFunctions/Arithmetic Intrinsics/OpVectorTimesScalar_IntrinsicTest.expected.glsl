#version 450

struct OpVectorTimesScalarTest
{
    int empty_struct_member;
};

void OpVectorTimesScalarTest_PreConstructor(OpVectorTimesScalarTest self)
{
}

void OpVectorTimesScalarTest_DefaultConstructor(OpVectorTimesScalarTest self)
{
    OpVectorTimesScalarTest_PreConstructor(self);
}

void Main(OpVectorTimesScalarTest self)
{
    vec2 vector2Val = vec2(0.0);
    float floatVal = 0.0;
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val *= floatVal;
    vector3Val *= floatVal;
    vector4Val *= floatVal;
}

void main()
{
    OpVectorTimesScalarTest self;
    OpVectorTimesScalarTest_DefaultConstructor(self);
    Main(self);
}

