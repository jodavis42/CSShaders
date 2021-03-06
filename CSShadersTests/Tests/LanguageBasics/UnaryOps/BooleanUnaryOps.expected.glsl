#version 450

struct BooleanUnaryOps
{
    int empty_struct_member;
};

void BooleanUnaryOps_PreConstructor(BooleanUnaryOps self)
{
}

void BooleanUnaryOps_DefaultConstructor(BooleanUnaryOps self)
{
    BooleanUnaryOps_PreConstructor(self);
}

void Main(BooleanUnaryOps self)
{
    bool b = false;
    bool r = !b;
}

void main()
{
    BooleanUnaryOps self;
    BooleanUnaryOps_DefaultConstructor(self);
    Main(self);
}

