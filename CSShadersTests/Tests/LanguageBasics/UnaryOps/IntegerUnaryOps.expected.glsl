#version 450

struct IntegerUnaryOps
{
    int empty_struct_member;
};

void IntegerUnaryOps_InitGlobals()
{
}

void IntegerUnaryOps_PreConstructor(IntegerUnaryOps self)
{
}

void IntegerUnaryOps_DefaultConstructor(IntegerUnaryOps self)
{
    IntegerUnaryOps_PreConstructor(self);
}

void IntegerUnaryOps_CopyInputs(IntegerUnaryOps self)
{
}

void Main(IntegerUnaryOps self)
{
    int i = 0;
    int r = 0;
    r = ~i;
    r = -i;
}

void IntegerUnaryOps_CopyOutputs(IntegerUnaryOps self)
{
}

void main()
{
    IntegerUnaryOps_InitGlobals();
    IntegerUnaryOps self;
    IntegerUnaryOps_DefaultConstructor(self);
    IntegerUnaryOps_CopyInputs(self);
    Main(self);
    IntegerUnaryOps_CopyOutputs(self);
}

