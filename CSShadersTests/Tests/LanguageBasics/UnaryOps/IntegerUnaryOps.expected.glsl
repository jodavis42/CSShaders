#version 450

struct IntegerUnaryOps
{
    int empty_struct_member;
};

void IntegerUnaryOps_PreConstructor(IntegerUnaryOps self)
{
}

void IntegerUnaryOps_DefaultConstructor(IntegerUnaryOps self)
{
    IntegerUnaryOps_PreConstructor(self);
}

void Main(IntegerUnaryOps self)
{
    int i = 0;
    int r = 0;
    r = ~i;
    r = -i;
}

void main()
{
    IntegerUnaryOps self;
    IntegerUnaryOps_DefaultConstructor(self);
    Main(self);
}

