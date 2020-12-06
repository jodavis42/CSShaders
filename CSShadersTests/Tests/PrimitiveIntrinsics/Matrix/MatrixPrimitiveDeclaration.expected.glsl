#version 450

struct MatrixPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void MatrixPrimitiveDeclarationTest_InitGlobals()
{
}

void MatrixPrimitiveDeclarationTest_PreConstructor(MatrixPrimitiveDeclarationTest self)
{
}

void MatrixPrimitiveDeclarationTest_DefaultConstructor(MatrixPrimitiveDeclarationTest self)
{
    MatrixPrimitiveDeclarationTest_PreConstructor(self);
}

void MatrixPrimitiveDeclarationTest_CopyInputs(MatrixPrimitiveDeclarationTest self)
{
}

void Main(MatrixPrimitiveDeclarationTest self)
{
    vec2 _34 = vec2(0.0);
    mat2 matrix0 = mat2(_34, _34);
}

void MatrixPrimitiveDeclarationTest_CopyOutputs(MatrixPrimitiveDeclarationTest self)
{
}

void main()
{
    MatrixPrimitiveDeclarationTest_InitGlobals();
    MatrixPrimitiveDeclarationTest self;
    MatrixPrimitiveDeclarationTest_DefaultConstructor(self);
    MatrixPrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    MatrixPrimitiveDeclarationTest_CopyOutputs(self);
}

