#version 450

struct InstanceFunctionRef
{
    int empty_struct_member;
};

void InstanceFunctionRef_PreConstructor(InstanceFunctionRef self)
{
}

void InstanceFunctionRef_DefaultConstructor(InstanceFunctionRef self)
{
    InstanceFunctionRef_PreConstructor(self);
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
    InstanceFunctionRef_DefaultConstructor(self);
    Main(self);
}

