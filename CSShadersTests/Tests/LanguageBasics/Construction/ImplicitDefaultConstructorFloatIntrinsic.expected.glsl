#version 450

struct ImplicitDefaultConstructorFloatIntrinsic
{
    int empty_struct_member;
};

void ImplicitDefaultConstructorFloatIntrinsic_InitGlobals()
{
}

void ImplicitDefaultConstructorFloatIntrinsic_PreConstructor(ImplicitDefaultConstructorFloatIntrinsic self)
{
}

void ImplicitDefaultConstructorFloatIntrinsic_DefaultConstructor(ImplicitDefaultConstructorFloatIntrinsic self)
{
    ImplicitDefaultConstructorFloatIntrinsic_PreConstructor(self);
}

void ImplicitDefaultConstructorFloatIntrinsic_CopyInputs(ImplicitDefaultConstructorFloatIntrinsic self)
{
}

void Main(ImplicitDefaultConstructorFloatIntrinsic self)
{
    float myFloat = 0.0;
}

void ImplicitDefaultConstructorFloatIntrinsic_CopyOutputs(ImplicitDefaultConstructorFloatIntrinsic self)
{
}

void main()
{
    ImplicitDefaultConstructorFloatIntrinsic_InitGlobals();
    ImplicitDefaultConstructorFloatIntrinsic self;
    ImplicitDefaultConstructorFloatIntrinsic_DefaultConstructor(self);
    ImplicitDefaultConstructorFloatIntrinsic_CopyInputs(self);
    Main(self);
    ImplicitDefaultConstructorFloatIntrinsic_CopyOutputs(self);
}

