#version 450

struct MatrixInverseTest
{
    int empty_struct_member;
};

void MatrixInverseTest_InitGlobals()
{
}

void MatrixInverseTest_PreConstructor(MatrixInverseTest self)
{
}

void MatrixInverseTest_DefaultConstructor(MatrixInverseTest self)
{
    MatrixInverseTest_PreConstructor(self);
}

void MatrixInverseTest_CopyInputs(MatrixInverseTest self)
{
}

void Main(MatrixInverseTest self)
{
    vec2 _41 = vec2(0.0);
    mat2 float2x2Val = mat2(_41, _41);
    float2x2Val = inverse(float2x2Val);
}

void MatrixInverseTest_CopyOutputs(MatrixInverseTest self)
{
}

void main()
{
    MatrixInverseTest_InitGlobals();
    MatrixInverseTest self;
    MatrixInverseTest_DefaultConstructor(self);
    MatrixInverseTest_CopyInputs(self);
    Main(self);
    MatrixInverseTest_CopyOutputs(self);
}

