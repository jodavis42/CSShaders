#version 450

struct MatrixPrimitiveFieldSwizzlesTest
{
    int empty_struct_member;
};

void PreConstructor_MatrixPrimitiveFieldSwizzlesTest(MatrixPrimitiveFieldSwizzlesTest self)
{
}

void DefaultConstructor_MatrixPrimitiveFieldSwizzlesTest(MatrixPrimitiveFieldSwizzlesTest self)
{
    PreConstructor_MatrixPrimitiveFieldSwizzlesTest(self);
}

void Main(MatrixPrimitiveFieldSwizzlesTest self)
{
    vec2 _35 = vec2(0.0);
    mat2 matrix0 = mat2(_35, _35);
    matrix0[1u] = matrix0[0];
}

void main()
{
    MatrixPrimitiveFieldSwizzlesTest self;
    DefaultConstructor_MatrixPrimitiveFieldSwizzlesTest(self);
    Main(self);
}

