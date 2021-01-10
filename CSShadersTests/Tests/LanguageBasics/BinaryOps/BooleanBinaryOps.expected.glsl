#version 450

struct BooleanBinaryOps
{
    int empty_struct_member;
};

void BooleanBinaryOps_InitGlobals()
{
}

void BooleanBinaryOps_PreConstructor(BooleanBinaryOps self)
{
}

void BooleanBinaryOps_DefaultConstructor(BooleanBinaryOps self)
{
    BooleanBinaryOps_PreConstructor(self);
}

void BooleanBinaryOps_CopyInputs(BooleanBinaryOps self)
{
}

void Main(BooleanBinaryOps self)
{
    bool b = false;
    bool r = false;
    r = b || b;
    r = b && b;
    r = b != b;
}

void BooleanBinaryOps_CopyOutputs(BooleanBinaryOps self)
{
}

void main()
{
    BooleanBinaryOps_InitGlobals();
    BooleanBinaryOps self;
    BooleanBinaryOps_DefaultConstructor(self);
    BooleanBinaryOps_CopyInputs(self);
    Main(self);
    BooleanBinaryOps_CopyOutputs(self);
}

