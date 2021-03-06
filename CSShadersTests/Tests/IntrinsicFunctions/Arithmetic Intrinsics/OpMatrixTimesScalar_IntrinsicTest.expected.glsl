#version 450

struct OpMatrixTimesScalarTest
{
    int empty_struct_member;
};

void OpMatrixTimesScalarTest_PreConstructor(OpMatrixTimesScalarTest self)
{
}

void OpMatrixTimesScalarTest_DefaultConstructor(OpMatrixTimesScalarTest self)
{
    OpMatrixTimesScalarTest_PreConstructor(self);
}

void Main(OpMatrixTimesScalarTest self)
{
    vec2 _39 = vec2(0.0);
    mat2 float2x2Val = mat2(_39, _39);
    float floatVal = 0.0;
    float2x2Val = float2x2Val * floatVal;
}

void main()
{
    OpMatrixTimesScalarTest self;
    OpMatrixTimesScalarTest_DefaultConstructor(self);
    Main(self);
}

