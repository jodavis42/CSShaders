#version 450

struct SamplerPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void SamplerPrimitiveDeclarationTest_PreConstructor(SamplerPrimitiveDeclarationTest self)
{
}

void SamplerPrimitiveDeclarationTest_DefaultConstructor(SamplerPrimitiveDeclarationTest self)
{
    SamplerPrimitiveDeclarationTest_PreConstructor(self);
}

void main()
{
    SamplerPrimitiveDeclarationTest self;
    SamplerPrimitiveDeclarationTest_DefaultConstructor(self);
}

