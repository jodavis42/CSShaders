#version 450

struct OpMatrixTimesVectorTest
{
    int empty_struct_member;
};

void OpMatrixTimesVectorTest_PreConstructor(OpMatrixTimesVectorTest self)
{
}

void OpMatrixTimesVectorTest_DefaultConstructor(OpMatrixTimesVectorTest self)
{
    OpMatrixTimesVectorTest_PreConstructor(self);
}

void Main(OpMatrixTimesVectorTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec2 _41 = vec2(0.0);
    mat2 float2x2Val = mat2(_41, _41);
    vector2Val = float2x2Val * vector2Val;
}

void main()
{
    OpMatrixTimesVectorTest self;
    OpMatrixTimesVectorTest_DefaultConstructor(self);
    Main(self);
}

