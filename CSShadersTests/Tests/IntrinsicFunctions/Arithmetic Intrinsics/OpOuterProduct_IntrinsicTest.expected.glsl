#version 450

struct OpOuterProductTest
{
    int empty_struct_member;
};

void OpOuterProductTest_PreConstructor(OpOuterProductTest self)
{
}

void OpOuterProductTest_DefaultConstructor(OpOuterProductTest self)
{
    OpOuterProductTest_PreConstructor(self);
}

void Main(OpOuterProductTest self)
{
    vec2 _39 = vec2(0.0);
    mat2 float2x2Val = mat2(_39, _39);
    vec2 vector2Val = vec2(0.0);
    float2x2Val = outerProduct(vector2Val, vector2Val);
}

void main()
{
    OpOuterProductTest self;
    OpOuterProductTest_DefaultConstructor(self);
    Main(self);
}

