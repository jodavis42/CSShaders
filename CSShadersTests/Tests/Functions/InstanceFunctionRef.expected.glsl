#version 450

struct InstanceFunctionRef
{
    int empty_struct_member;
};

void PreConstructor_InstanceFunctionRef(InstanceFunctionRef self)
{
}

void DefaultConstructor_InstanceFunctionRef(InstanceFunctionRef self)
{
    PreConstructor_InstanceFunctionRef(self);
}

void TestFn1(InstanceFunctionRef self, out int value)
{
    value = 3;
}

void Main(InstanceFunctionRef self)
{
    int value = 1;
    TestFn1(self, value);
}

void main()
{
    InstanceFunctionRef self;
    DefaultConstructor_InstanceFunctionRef(self);
    Main(self);
}

