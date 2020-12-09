#version 450

struct ImagePrimitiveDeclarationTest
{
    int empty_struct_member;
};

void ImagePrimitiveDeclarationTest_InitGlobals()
{
}

void ImagePrimitiveDeclarationTest_PreConstructor(ImagePrimitiveDeclarationTest self)
{
}

void ImagePrimitiveDeclarationTest_DefaultConstructor(ImagePrimitiveDeclarationTest self)
{
    ImagePrimitiveDeclarationTest_PreConstructor(self);
}

void ImagePrimitiveDeclarationTest_CopyInputs(ImagePrimitiveDeclarationTest self)
{
}

void ImagePrimitiveDeclarationTest_CopyOutputs(ImagePrimitiveDeclarationTest self)
{
}

void main()
{
    ImagePrimitiveDeclarationTest_InitGlobals();
    ImagePrimitiveDeclarationTest self;
    ImagePrimitiveDeclarationTest_DefaultConstructor(self);
    ImagePrimitiveDeclarationTest_CopyInputs(self);
    ImagePrimitiveDeclarationTest_CopyOutputs(self);
}

