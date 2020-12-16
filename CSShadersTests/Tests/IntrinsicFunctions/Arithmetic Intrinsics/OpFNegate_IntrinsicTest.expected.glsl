#version 450

struct OpFNegateTest
{
    int empty_struct_member;
};

void OpFNegateTest_InitGlobals()
{
}

void OpFNegateTest_PreConstructor(OpFNegateTest self)
{
}

void OpFNegateTest_DefaultConstructor(OpFNegateTest self)
{
    OpFNegateTest_PreConstructor(self);
}

void OpFNegateTest_CopyInputs(OpFNegateTest self)
{
}

void Main(OpFNegateTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = -floatVal;
    vector2Val = -vector2Val;
    vector3Val = -vector3Val;
    vector4Val = -vector4Val;
}

void OpFNegateTest_CopyOutputs(OpFNegateTest self)
{
}

void main()
{
    OpFNegateTest_InitGlobals();
    OpFNegateTest self;
    OpFNegateTest_DefaultConstructor(self);
    OpFNegateTest_CopyInputs(self);
    Main(self);
    OpFNegateTest_CopyOutputs(self);
}

