#version 450

struct SampledImagePrimitiveDeclaration
{
    int empty_struct_member;
};

uniform sampler2D SimpleSampledFloatImage2d;

void SampledImagePrimitiveDeclaration_PreConstructor(SampledImagePrimitiveDeclaration self)
{
}

void SampledImagePrimitiveDeclaration_DefaultConstructor(SampledImagePrimitiveDeclaration self)
{
    SampledImagePrimitiveDeclaration_PreConstructor(self);
}

void main()
{
    SampledImagePrimitiveDeclaration self;
    SampledImagePrimitiveDeclaration_DefaultConstructor(self);
}

