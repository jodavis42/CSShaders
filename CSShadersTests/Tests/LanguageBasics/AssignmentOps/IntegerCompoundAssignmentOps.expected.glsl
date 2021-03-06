#version 450

struct IntegerCompoundAssignmentOps
{
    int empty_struct_member;
};

void IntegerCompoundAssignmentOps_PreConstructor(IntegerCompoundAssignmentOps self)
{
}

void IntegerCompoundAssignmentOps_DefaultConstructor(IntegerCompoundAssignmentOps self)
{
    IntegerCompoundAssignmentOps_PreConstructor(self);
}

void Main(IntegerCompoundAssignmentOps self)
{
    int i = 0;
    int r = 0;
    r += i;
    r -= i;
    r *= i;
    r /= i;
    r %= i;
    r |= i;
    r &= i;
    r ^= i;
    r = r << i;
    r = r >> i;
}

void main()
{
    IntegerCompoundAssignmentOps self;
    IntegerCompoundAssignmentOps_DefaultConstructor(self);
    Main(self);
}

