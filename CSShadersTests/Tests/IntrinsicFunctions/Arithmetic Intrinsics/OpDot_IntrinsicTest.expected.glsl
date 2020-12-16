#version 450

struct OpDotTest
{
    int empty_struct_member;
};

void OpDotTest_InitGlobals()
{
}

void OpDotTest_PreConstructor(OpDotTest self)
{
}

void OpDotTest_DefaultConstructor(OpDotTest self)
{
    OpDotTest_PreConstructor(self);
}

void OpDotTest_CopyInputs(OpDotTest self)
{
}

void Main(OpDotTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = dot(vector2Val, vector2Val);
    floatVal = dot(vector3Val, vector3Val);
    floatVal = dot(vector4Val, vector4Val);
}

void OpDotTest_CopyOutputs(OpDotTest self)
{
}

void main()
{
    OpDotTest_InitGlobals();
    OpDotTest self;
    OpDotTest_DefaultConstructor(self);
    OpDotTest_CopyInputs(self);
    Main(self);
    OpDotTest_CopyOutputs(self);
}

