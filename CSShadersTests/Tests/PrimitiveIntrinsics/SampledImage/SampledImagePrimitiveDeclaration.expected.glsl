#version 450

struct SampledImagePrimitiveDeclaration
{
    int empty_struct_member;
};

uniform sampler2D SimpleSampledFloatImage2d;

void SampledImagePrimitiveDeclaration_InitGlobals()
{
}

void SampledImagePrimitiveDeclaration_PreConstructor(SampledImagePrimitiveDeclaration self)
{
}

void SampledImagePrimitiveDeclaration_DefaultConstructor(SampledImagePrimitiveDeclaration self)
{
    SampledImagePrimitiveDeclaration_PreConstructor(self);
}

void SampledImagePrimitiveDeclaration_CopyInputs(SampledImagePrimitiveDeclaration self)
{
}

void SampledImagePrimitiveDeclaration_CopyOutputs(SampledImagePrimitiveDeclaration self)
{
}

void main()
{
    SampledImagePrimitiveDeclaration_InitGlobals();
    SampledImagePrimitiveDeclaration self;
    SampledImagePrimitiveDeclaration_DefaultConstructor(self);
    SampledImagePrimitiveDeclaration_CopyInputs(self);
    SampledImagePrimitiveDeclaration_CopyOutputs(self);
}

