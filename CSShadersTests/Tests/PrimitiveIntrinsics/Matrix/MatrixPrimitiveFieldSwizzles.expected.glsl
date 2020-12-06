#version 450

struct MatrixPrimitiveFieldSwizzlesTest
{
    int empty_struct_member;
};

void MatrixPrimitiveFieldSwizzlesTest_InitGlobals()
{
}

void MatrixPrimitiveFieldSwizzlesTest_PreConstructor(MatrixPrimitiveFieldSwizzlesTest self)
{
}

void MatrixPrimitiveFieldSwizzlesTest_DefaultConstructor(MatrixPrimitiveFieldSwizzlesTest self)
{
    MatrixPrimitiveFieldSwizzlesTest_PreConstructor(self);
}

void MatrixPrimitiveFieldSwizzlesTest_CopyInputs(MatrixPrimitiveFieldSwizzlesTest self)
{
}

void Main(MatrixPrimitiveFieldSwizzlesTest self)
{
    vec2 _35 = vec2(0.0);
    mat2 matrix0 = mat2(_35, _35);
    matrix0[1u] = matrix0[0];
}

void MatrixPrimitiveFieldSwizzlesTest_CopyOutputs(MatrixPrimitiveFieldSwizzlesTest self)
{
}

void main()
{
    MatrixPrimitiveFieldSwizzlesTest_InitGlobals();
    MatrixPrimitiveFieldSwizzlesTest self;
    MatrixPrimitiveFieldSwizzlesTest_DefaultConstructor(self);
    MatrixPrimitiveFieldSwizzlesTest_CopyInputs(self);
    Main(self);
    MatrixPrimitiveFieldSwizzlesTest_CopyOutputs(self);
}

