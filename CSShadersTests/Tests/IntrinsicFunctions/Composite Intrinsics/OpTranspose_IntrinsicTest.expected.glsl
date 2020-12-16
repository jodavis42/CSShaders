#version 450

struct OpTransposeTest
{
    int empty_struct_member;
};

void OpTransposeTest_InitGlobals()
{
}

void OpTransposeTest_PreConstructor(OpTransposeTest self)
{
}

void OpTransposeTest_DefaultConstructor(OpTransposeTest self)
{
    OpTransposeTest_PreConstructor(self);
}

void OpTransposeTest_CopyInputs(OpTransposeTest self)
{
}

void Main(OpTransposeTest self)
{
    vec2 _38 = vec2(0.0);
    mat2 float2x2Val = mat2(_38, _38);
    float2x2Val = transpose(float2x2Val);
}

void OpTransposeTest_CopyOutputs(OpTransposeTest self)
{
}

void main()
{
    OpTransposeTest_InitGlobals();
    OpTransposeTest self;
    OpTransposeTest_DefaultConstructor(self);
    OpTransposeTest_CopyInputs(self);
    Main(self);
    OpTransposeTest_CopyOutputs(self);
}

