#version 450

struct VectorPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void PreConstructor_VectorPrimitiveDeclarationTest(VectorPrimitiveDeclarationTest self)
{
}

void DefaultConstructor_VectorPrimitiveDeclarationTest(VectorPrimitiveDeclarationTest self)
{
    PreConstructor_VectorPrimitiveDeclarationTest(self);
}

void Main(VectorPrimitiveDeclarationTest self)
{
    vec2 vector0 = vec2(0.0);
}

void main()
{
    VectorPrimitiveDeclarationTest self;
    DefaultConstructor_VectorPrimitiveDeclarationTest(self);
    Main(self);
}

