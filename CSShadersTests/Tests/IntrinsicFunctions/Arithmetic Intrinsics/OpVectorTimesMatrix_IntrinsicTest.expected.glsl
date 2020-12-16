#version 450

struct OpVectorTimesMatrixTest
{
    int empty_struct_member;
};

void OpVectorTimesMatrixTest_InitGlobals()
{
}

void OpVectorTimesMatrixTest_PreConstructor(OpVectorTimesMatrixTest self)
{
}

void OpVectorTimesMatrixTest_DefaultConstructor(OpVectorTimesMatrixTest self)
{
    OpVectorTimesMatrixTest_PreConstructor(self);
}

void OpVectorTimesMatrixTest_CopyInputs(OpVectorTimesMatrixTest self)
{
}

void Main(OpVectorTimesMatrixTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec2 _41 = vec2(0.0);
    mat2 float2x2Val = mat2(_41, _41);
    vector2Val *= float2x2Val;
}

void OpVectorTimesMatrixTest_CopyOutputs(OpVectorTimesMatrixTest self)
{
}

void main()
{
    OpVectorTimesMatrixTest_InitGlobals();
    OpVectorTimesMatrixTest self;
    OpVectorTimesMatrixTest_DefaultConstructor(self);
    OpVectorTimesMatrixTest_CopyInputs(self);
    Main(self);
    OpVectorTimesMatrixTest_CopyOutputs(self);
}

