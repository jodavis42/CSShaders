#version 450

struct OpTransposeTest
{
    int empty_struct_member;
};

void OpTransposeTest_PreConstructor(OpTransposeTest self)
{
}

void OpTransposeTest_DefaultConstructor(OpTransposeTest self)
{
    OpTransposeTest_PreConstructor(self);
}

void Main(OpTransposeTest self)
{
    vec2 _38 = vec2(0.0);
    mat2 float2x2Val = mat2(_38, _38);
    float2x2Val = transpose(float2x2Val);
}

void main()
{
    OpTransposeTest self;
    OpTransposeTest_DefaultConstructor(self);
    Main(self);
}

