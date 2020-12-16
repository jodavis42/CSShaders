#version 450

struct DeterminantTest
{
    int empty_struct_member;
};

void DeterminantTest_InitGlobals()
{
}

void DeterminantTest_PreConstructor(DeterminantTest self)
{
}

void DeterminantTest_DefaultConstructor(DeterminantTest self)
{
    DeterminantTest_PreConstructor(self);
}

void DeterminantTest_CopyInputs(DeterminantTest self)
{
}

void Main(DeterminantTest self)
{
    float floatVal = 0.0;
    vec2 _43 = vec2(0.0);
    mat2 float2x2Val = mat2(_43, _43);
    floatVal = determinant(float2x2Val);
}

void DeterminantTest_CopyOutputs(DeterminantTest self)
{
}

void main()
{
    DeterminantTest_InitGlobals();
    DeterminantTest self;
    DeterminantTest_DefaultConstructor(self);
    DeterminantTest_CopyInputs(self);
    Main(self);
    DeterminantTest_CopyOutputs(self);
}

