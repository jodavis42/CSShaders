#version 450

struct OpFDivTest
{
    int empty_struct_member;
};

void OpFDivTest_InitGlobals()
{
}

void OpFDivTest_PreConstructor(OpFDivTest self)
{
}

void OpFDivTest_DefaultConstructor(OpFDivTest self)
{
    OpFDivTest_PreConstructor(self);
}

void OpFDivTest_CopyInputs(OpFDivTest self)
{
}

void Main(OpFDivTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal /= floatVal;
    vector2Val /= vector2Val;
    vector3Val /= vector3Val;
    vector4Val /= vector4Val;
}

void OpFDivTest_CopyOutputs(OpFDivTest self)
{
}

void main()
{
    OpFDivTest_InitGlobals();
    OpFDivTest self;
    OpFDivTest_DefaultConstructor(self);
    OpFDivTest_CopyInputs(self);
    Main(self);
    OpFDivTest_CopyOutputs(self);
}
