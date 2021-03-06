#version 450

struct OpVectorTimesMatrixTest
{
    int empty_struct_member;
};

void OpVectorTimesMatrixTest_PreConstructor(OpVectorTimesMatrixTest self)
{
}

void OpVectorTimesMatrixTest_DefaultConstructor(OpVectorTimesMatrixTest self)
{
    OpVectorTimesMatrixTest_PreConstructor(self);
}

void Main(OpVectorTimesMatrixTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec2 _41 = vec2(0.0);
    mat2 float2x2Val = mat2(_41, _41);
    vector2Val *= float2x2Val;
}

void main()
{
    OpVectorTimesMatrixTest self;
    OpVectorTimesMatrixTest_DefaultConstructor(self);
    Main(self);
}

