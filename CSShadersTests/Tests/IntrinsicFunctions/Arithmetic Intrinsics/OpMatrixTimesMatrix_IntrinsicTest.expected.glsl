#version 450

struct OpMatrixTimesMatrixTest
{
    int empty_struct_member;
};

void OpMatrixTimesMatrixTest_PreConstructor(OpMatrixTimesMatrixTest self)
{
}

void OpMatrixTimesMatrixTest_DefaultConstructor(OpMatrixTimesMatrixTest self)
{
    OpMatrixTimesMatrixTest_PreConstructor(self);
}

void Main(OpMatrixTimesMatrixTest self)
{
    vec2 _38 = vec2(0.0);
    mat2 float2x2Val = mat2(_38, _38);
    float2x2Val = float2x2Val * float2x2Val;
}

void main()
{
    OpMatrixTimesMatrixTest self;
    OpMatrixTimesMatrixTest_DefaultConstructor(self);
    Main(self);
}

