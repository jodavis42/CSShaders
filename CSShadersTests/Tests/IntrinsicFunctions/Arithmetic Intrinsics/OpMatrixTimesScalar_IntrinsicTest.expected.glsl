#version 450

struct OpMatrixTimesScalarTest
{
    int empty_struct_member;
};

void OpMatrixTimesScalarTest_InitGlobals()
{
}

void OpMatrixTimesScalarTest_PreConstructor(OpMatrixTimesScalarTest self)
{
}

void OpMatrixTimesScalarTest_DefaultConstructor(OpMatrixTimesScalarTest self)
{
    OpMatrixTimesScalarTest_PreConstructor(self);
}

void OpMatrixTimesScalarTest_CopyInputs(OpMatrixTimesScalarTest self)
{
}

void Main(OpMatrixTimesScalarTest self)
{
    vec2 _39 = vec2(0.0);
    mat2 float2x2Val = mat2(_39, _39);
    float floatVal = 0.0;
    float2x2Val = float2x2Val * floatVal;
}

void OpMatrixTimesScalarTest_CopyOutputs(OpMatrixTimesScalarTest self)
{
}

void main()
{
    OpMatrixTimesScalarTest_InitGlobals();
    OpMatrixTimesScalarTest self;
    OpMatrixTimesScalarTest_DefaultConstructor(self);
    OpMatrixTimesScalarTest_CopyInputs(self);
    Main(self);
    OpMatrixTimesScalarTest_CopyOutputs(self);
}

