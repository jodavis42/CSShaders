#version 450

struct UnsignedIntegerBinaryOps
{
    int empty_struct_member;
};

void UnsignedIntegerBinaryOps_PreConstructor(UnsignedIntegerBinaryOps self)
{
}

void UnsignedIntegerBinaryOps_DefaultConstructor(UnsignedIntegerBinaryOps self)
{
    UnsignedIntegerBinaryOps_PreConstructor(self);
}

void Main(UnsignedIntegerBinaryOps self)
{
    uint u = 0u;
    uint r = 0u;
    r = u + u;
    r = u - u;
    r = u * u;
    r = u % u;
    r = u | u;
    r = u & u;
    r = u ^ u;
    bool b = false;
    b = u < u;
    b = u <= u;
    b = u > u;
    b = u >= u;
    b = u == u;
    b = u != u;
    u = u << uint(2);
    u = uint(int(u) >> 2);
}

void main()
{
    UnsignedIntegerBinaryOps self;
    UnsignedIntegerBinaryOps_DefaultConstructor(self);
    Main(self);
}

