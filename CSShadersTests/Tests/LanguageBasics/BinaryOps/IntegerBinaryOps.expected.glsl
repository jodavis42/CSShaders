#version 450

struct IntegerBinaryOps
{
    int empty_struct_member;
};

void IntegerBinaryOps_PreConstructor(IntegerBinaryOps self)
{
}

void IntegerBinaryOps_DefaultConstructor(IntegerBinaryOps self)
{
    IntegerBinaryOps_PreConstructor(self);
}

void Main(IntegerBinaryOps self)
{
    int i = 0;
    int r = 0;
    r = i + i;
    r = i - i;
    r = i * i;
    r = i % i;
    r = i | i;
    r = i & i;
    r = i ^ i;
    bool b = false;
    b = i < i;
    b = i <= i;
    b = i > i;
    b = i >= i;
    b = i == i;
    b = i != i;
    i = i << 2;
    i = i >> 2;
}

void main()
{
    IntegerBinaryOps self;
    IntegerBinaryOps_DefaultConstructor(self);
    Main(self);
}

