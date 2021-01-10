#version 450

struct ImplicitDefaultConstructorMatrixIntrinsic
{
    int empty_struct_member;
};

void ImplicitDefaultConstructorMatrixIntrinsic_InitGlobals()
{
}

void ImplicitDefaultConstructorMatrixIntrinsic_PreConstructor(ImplicitDefaultConstructorMatrixIntrinsic self)
{
}

void ImplicitDefaultConstructorMatrixIntrinsic_DefaultConstructor(ImplicitDefaultConstructorMatrixIntrinsic self)
{
    ImplicitDefaultConstructorMatrixIntrinsic_PreConstructor(self);
}

void ImplicitDefaultConstructorMatrixIntrinsic_CopyInputs(ImplicitDefaultConstructorMatrixIntrinsic self)
{
}

void Main(ImplicitDefaultConstructorMatrixIntrinsic self)
{
    vec2 _34 = vec2(0.0);
    mat2 myMatrix = mat2(_34, _34);
}

void ImplicitDefaultConstructorMatrixIntrinsic_CopyOutputs(ImplicitDefaultConstructorMatrixIntrinsic self)
{
}

void main()
{
    ImplicitDefaultConstructorMatrixIntrinsic_InitGlobals();
    ImplicitDefaultConstructorMatrixIntrinsic self;
    ImplicitDefaultConstructorMatrixIntrinsic_DefaultConstructor(self);
    ImplicitDefaultConstructorMatrixIntrinsic_CopyInputs(self);
    Main(self);
    ImplicitDefaultConstructorMatrixIntrinsic_CopyOutputs(self);
}

