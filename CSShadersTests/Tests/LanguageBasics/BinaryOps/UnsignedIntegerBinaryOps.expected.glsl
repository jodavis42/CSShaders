#version 450

struct UnsignedIntegerBinaryOps
{
    int empty_struct_member;
};

void UnsignedIntegerBinaryOps_InitGlobals()
{
}

void UnsignedIntegerBinaryOps_PreConstructor(UnsignedIntegerBinaryOps self)
{
}

void UnsignedIntegerBinaryOps_DefaultConstructor(UnsignedIntegerBinaryOps self)
{
    UnsignedIntegerBinaryOps_PreConstructor(self);
}

void UnsignedIntegerBinaryOps_CopyInputs(UnsignedIntegerBinaryOps self)
{
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
}

void UnsignedIntegerBinaryOps_CopyOutputs(UnsignedIntegerBinaryOps self)
{
}

void main()
{
    UnsignedIntegerBinaryOps_InitGlobals();
    UnsignedIntegerBinaryOps self;
    UnsignedIntegerBinaryOps_DefaultConstructor(self);
    UnsignedIntegerBinaryOps_CopyInputs(self);
    Main(self);
    UnsignedIntegerBinaryOps_CopyOutputs(self);
}

