#version 450

struct BooleanUnaryOps
{
    int empty_struct_member;
};

void BooleanUnaryOps_InitGlobals()
{
}

void BooleanUnaryOps_PreConstructor(BooleanUnaryOps self)
{
}

void BooleanUnaryOps_DefaultConstructor(BooleanUnaryOps self)
{
    BooleanUnaryOps_PreConstructor(self);
}

void BooleanUnaryOps_CopyInputs(BooleanUnaryOps self)
{
}

void Main(BooleanUnaryOps self)
{
    bool b = false;
    bool r = !b;
}

void BooleanUnaryOps_CopyOutputs(BooleanUnaryOps self)
{
}

void main()
{
    BooleanUnaryOps_InitGlobals();
    BooleanUnaryOps self;
    BooleanUnaryOps_DefaultConstructor(self);
    BooleanUnaryOps_CopyInputs(self);
    Main(self);
    BooleanUnaryOps_CopyOutputs(self);
}

