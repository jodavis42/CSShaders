#version 450

struct ImplicitDefaultConstructorFloatIntrinsic
{
    int empty_struct_member;
};

void ImplicitDefaultConstructorFloatIntrinsic_PreConstructor(ImplicitDefaultConstructorFloatIntrinsic self)
{
}

void ImplicitDefaultConstructorFloatIntrinsic_DefaultConstructor(ImplicitDefaultConstructorFloatIntrinsic self)
{
    ImplicitDefaultConstructorFloatIntrinsic_PreConstructor(self);
}

void Main(ImplicitDefaultConstructorFloatIntrinsic self)
{
    float myFloat = 0.0;
}

void main()
{
    ImplicitDefaultConstructorFloatIntrinsic self;
    ImplicitDefaultConstructorFloatIntrinsic_DefaultConstructor(self);
    Main(self);
}

