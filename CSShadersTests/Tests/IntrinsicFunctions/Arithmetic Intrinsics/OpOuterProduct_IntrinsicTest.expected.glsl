#version 450

struct OpOuterProductTest
{
    int empty_struct_member;
};

void OpOuterProductTest_InitGlobals()
{
}

void OpOuterProductTest_PreConstructor(OpOuterProductTest self)
{
}

void OpOuterProductTest_DefaultConstructor(OpOuterProductTest self)
{
    OpOuterProductTest_PreConstructor(self);
}

void OpOuterProductTest_CopyInputs(OpOuterProductTest self)
{
}

void Main(OpOuterProductTest self)
{
    vec2 _39 = vec2(0.0);
    mat2 float2x2Val = mat2(_39, _39);
    vec2 vector2Val = vec2(0.0);
    float2x2Val = outerProduct(vector2Val, vector2Val);
}

void OpOuterProductTest_CopyOutputs(OpOuterProductTest self)
{
}

void main()
{
    OpOuterProductTest_InitGlobals();
    OpOuterProductTest self;
    OpOuterProductTest_DefaultConstructor(self);
    OpOuterProductTest_CopyInputs(self);
    Main(self);
    OpOuterProductTest_CopyOutputs(self);
}

