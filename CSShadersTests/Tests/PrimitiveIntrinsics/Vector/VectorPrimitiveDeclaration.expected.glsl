#version 450

struct VectorPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void VectorPrimitiveDeclarationTest_PreConstructor(VectorPrimitiveDeclarationTest self)
{
}

void VectorPrimitiveDeclarationTest_DefaultConstructor(VectorPrimitiveDeclarationTest self)
{
    VectorPrimitiveDeclarationTest_PreConstructor(self);
}

void Main(VectorPrimitiveDeclarationTest self)
{
    vec2 vector0 = vec2(0.0);
}

void main()
{
    VectorPrimitiveDeclarationTest self;
    VectorPrimitiveDeclarationTest_DefaultConstructor(self);
    Main(self);
}

