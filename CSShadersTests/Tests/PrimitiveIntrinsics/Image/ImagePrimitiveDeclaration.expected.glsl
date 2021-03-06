#version 450

struct ImagePrimitiveDeclarationTest
{
    int empty_struct_member;
};

void ImagePrimitiveDeclarationTest_PreConstructor(ImagePrimitiveDeclarationTest self)
{
}

void ImagePrimitiveDeclarationTest_DefaultConstructor(ImagePrimitiveDeclarationTest self)
{
    ImagePrimitiveDeclarationTest_PreConstructor(self);
}

void main()
{
    ImagePrimitiveDeclarationTest self;
    ImagePrimitiveDeclarationTest_DefaultConstructor(self);
}

