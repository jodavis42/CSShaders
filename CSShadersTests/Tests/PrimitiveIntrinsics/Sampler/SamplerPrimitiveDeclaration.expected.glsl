#version 450

struct SamplerPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void SamplerPrimitiveDeclarationTest_InitGlobals()
{
}

void SamplerPrimitiveDeclarationTest_PreConstructor(SamplerPrimitiveDeclarationTest self)
{
}

void SamplerPrimitiveDeclarationTest_DefaultConstructor(SamplerPrimitiveDeclarationTest self)
{
    SamplerPrimitiveDeclarationTest_PreConstructor(self);
}

void SamplerPrimitiveDeclarationTest_CopyInputs(SamplerPrimitiveDeclarationTest self)
{
}

void SamplerPrimitiveDeclarationTest_CopyOutputs(SamplerPrimitiveDeclarationTest self)
{
}

void main()
{
    SamplerPrimitiveDeclarationTest_InitGlobals();
    SamplerPrimitiveDeclarationTest self;
    SamplerPrimitiveDeclarationTest_DefaultConstructor(self);
    SamplerPrimitiveDeclarationTest_CopyInputs(self);
    SamplerPrimitiveDeclarationTest_CopyOutputs(self);
}

