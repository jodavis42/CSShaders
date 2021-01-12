#version 450

struct UnsignedIntegerUnaryOps
{
    int empty_struct_member;
};

void UnsignedIntegerUnaryOps_InitGlobals()
{
}

void UnsignedIntegerUnaryOps_PreConstructor(UnsignedIntegerUnaryOps self)
{
}

void UnsignedIntegerUnaryOps_DefaultConstructor(UnsignedIntegerUnaryOps self)
{
    UnsignedIntegerUnaryOps_PreConstructor(self);
}

void UnsignedIntegerUnaryOps_CopyInputs(UnsignedIntegerUnaryOps self)
{
}

void Main(UnsignedIntegerUnaryOps self)
{
    uint u = 0u;
    uint r = ~u;
}

void UnsignedIntegerUnaryOps_CopyOutputs(UnsignedIntegerUnaryOps self)
{
}

void main()
{
    UnsignedIntegerUnaryOps_InitGlobals();
    UnsignedIntegerUnaryOps self;
    UnsignedIntegerUnaryOps_DefaultConstructor(self);
    UnsignedIntegerUnaryOps_CopyInputs(self);
    Main(self);
    UnsignedIntegerUnaryOps_CopyOutputs(self);
}

