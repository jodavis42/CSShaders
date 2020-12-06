#version 450

struct MatrixPrimitiveImplicitExplicitThisTests
{
    int empty_struct_member;
};

void MatrixPrimitiveImplicitExplicitThisTests_InitGlobals()
{
}

void MatrixPrimitiveImplicitExplicitThisTests_PreConstructor(MatrixPrimitiveImplicitExplicitThisTests self)
{
}

void MatrixPrimitiveImplicitExplicitThisTests_DefaultConstructor(MatrixPrimitiveImplicitExplicitThisTests self)
{
    MatrixPrimitiveImplicitExplicitThisTests_PreConstructor(self);
}

void MatrixPrimitiveImplicitExplicitThisTests_CopyInputs(MatrixPrimitiveImplicitExplicitThisTests self)
{
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

void MatrixPrimitiveImplicitExplicitThisTests_CopyOutputs(MatrixPrimitiveImplicitExplicitThisTests self)
{
}

void main()
{
    MatrixPrimitiveImplicitExplicitThisTests_InitGlobals();
    MatrixPrimitiveImplicitExplicitThisTests self;
    MatrixPrimitiveImplicitExplicitThisTests_DefaultConstructor(self);
    MatrixPrimitiveImplicitExplicitThisTests_CopyInputs(self);
    Main(self);
    MatrixPrimitiveImplicitExplicitThisTests_CopyOutputs(self);
}

