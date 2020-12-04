#version 450

struct MatrixPrimitiveImplicitExplicitThisTests
{
    int empty_struct_member;
};

void PreConstructor_MatrixPrimitiveImplicitExplicitThisTests(MatrixPrimitiveImplicitExplicitThisTests self)
{
}

void DefaultConstructor_MatrixPrimitiveImplicitExplicitThisTests(MatrixPrimitiveImplicitExplicitThisTests self)
{
    PreConstructor_MatrixPrimitiveImplicitExplicitThisTests(self);
}

vec2 FieldThisExplicit(mat2 self)
{
    return self[0];
}

vec2 FieldThisImplicit(mat2 self)
{
    return self[0];
}

void Main(MatrixPrimitiveImplicitExplicitThisTests self)
{
    vec2 _52 = vec2(0.0);
    mat2 matrix0 = mat2(_52, _52);
    vec2 fieldThisExplicit = FieldThisExplicit(matrix0);
    vec2 fieldThisImplicit = FieldThisImplicit(matrix0);
}

void main()
{
    MatrixPrimitiveImplicitExplicitThisTests self;
    DefaultConstructor_MatrixPrimitiveImplicitExplicitThisTests(self);
    Main(self);
}

