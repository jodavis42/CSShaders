#version 450

struct MatrixPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void PreConstructor_MatrixPrimitiveDeclarationTest(MatrixPrimitiveDeclarationTest self)
{
}

void DefaultConstructor_MatrixPrimitiveDeclarationTest(MatrixPrimitiveDeclarationTest self)
{
    PreConstructor_MatrixPrimitiveDeclarationTest(self);
}

void Main(MatrixPrimitiveDeclarationTest self)
{
    vec2 _34 = vec2(0.0);
    mat2 matrix0 = mat2(_34, _34);
}

void main()
{
    MatrixPrimitiveDeclarationTest self;
    DefaultConstructor_MatrixPrimitiveDeclarationTest(self);
    Main(self);
}

