#version 450

struct BooleanBinaryOps
{
    int empty_struct_member;
};

void BooleanBinaryOps_PreConstructor(BooleanBinaryOps self)
{
}

void BooleanBinaryOps_DefaultConstructor(BooleanBinaryOps self)
{
    BooleanBinaryOps_PreConstructor(self);
}

void Main(BooleanBinaryOps self)
{
    bool b = false;
    bool r = false;
    r = b || b;
    r = b && b;
    r = b != b;
}

void main()
{
    BooleanBinaryOps self;
    BooleanBinaryOps_DefaultConstructor(self);
    Main(self);
}

