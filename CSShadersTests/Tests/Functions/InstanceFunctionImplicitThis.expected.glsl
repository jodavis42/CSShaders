#version 450

struct InstanceFunctionImplicitThis
{
    int empty_struct_member;
};

void InstanceFunctionImplicitThis_PreConstructor(InstanceFunctionImplicitThis self)
{
}

void InstanceFunctionImplicitThis_DefaultConstructor(InstanceFunctionImplicitThis self)
{
    InstanceFunctionImplicitThis_PreConstructor(self);
}

void TestFn1(InstanceFunctionImplicitThis self)
{
}

void Main(InstanceFunctionImplicitThis self)
{
    TestFn1(self);
}

void main()
{
    InstanceFunctionImplicitThis self;
    InstanceFunctionImplicitThis_DefaultConstructor(self);
    Main(self);
}

