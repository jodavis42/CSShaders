#version 450

struct OpFMulTest
{
    int empty_struct_member;
};

void OpFMulTest_InitGlobals()
{
}

void OpFMulTest_PreConstructor(OpFMulTest self)
{
}

void OpFMulTest_DefaultConstructor(OpFMulTest self)
{
    OpFMulTest_PreConstructor(self);
}

void OpFMulTest_CopyInputs(OpFMulTest self)
{
}

void Main(OpFMulTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal *= floatVal;
    vector2Val *= vector2Val;
    vector3Val *= vector3Val;
    vector4Val *= vector4Val;
}

void OpFMulTest_CopyOutputs(OpFMulTest self)
{
}

void main()
{
    OpFMulTest_InitGlobals();
    OpFMulTest self;
    OpFMulTest_DefaultConstructor(self);
    OpFMulTest_CopyInputs(self);
    Main(self);
    OpFMulTest_CopyOutputs(self);
}

