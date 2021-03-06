#version 450

struct MatrixInverseTest
{
    int empty_struct_member;
};

void MatrixInverseTest_PreConstructor(MatrixInverseTest self)
{
}

void MatrixInverseTest_DefaultConstructor(MatrixInverseTest self)
{
    MatrixInverseTest_PreConstructor(self);
}

void Main(MatrixInverseTest self)
{
    vec2 _41 = vec2(0.0);
    mat2 float2x2Val = mat2(_41, _41);
    float2x2Val = inverse(float2x2Val);
}

void main()
{
    MatrixInverseTest self;
    MatrixInverseTest_DefaultConstructor(self);
    Main(self);
}

