#version 450

struct IntegerBinaryOps
{
    int empty_struct_member;
};

void IntegerBinaryOps_InitGlobals()
{
}

void IntegerBinaryOps_PreConstructor(IntegerBinaryOps self)
{
}

void IntegerBinaryOps_DefaultConstructor(IntegerBinaryOps self)
{
    IntegerBinaryOps_PreConstructor(self);
}

void IntegerBinaryOps_CopyInputs(IntegerBinaryOps self)
{
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
}

void IntegerBinaryOps_CopyOutputs(IntegerBinaryOps self)
{
}

void main()
{
    IntegerBinaryOps_InitGlobals();
    IntegerBinaryOps self;
    IntegerBinaryOps_DefaultConstructor(self);
    IntegerBinaryOps_CopyInputs(self);
    Main(self);
    IntegerBinaryOps_CopyOutputs(self);
}

