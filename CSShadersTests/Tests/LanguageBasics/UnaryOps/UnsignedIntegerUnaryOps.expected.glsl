#version 450

struct UnsignedIntegerUnaryOps
{
    int empty_struct_member;
};

void UnsignedIntegerUnaryOps_PreConstructor(UnsignedIntegerUnaryOps self)
{
}

void UnsignedIntegerUnaryOps_DefaultConstructor(UnsignedIntegerUnaryOps self)
{
    UnsignedIntegerUnaryOps_PreConstructor(self);
}

void Main(UnsignedIntegerUnaryOps self)
{
    uint u = 0u;
    uint r = ~u;
}

void main()
{
    UnsignedIntegerUnaryOps self;
    UnsignedIntegerUnaryOps_DefaultConstructor(self);
    Main(self);
}

