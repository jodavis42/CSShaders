#version 450

struct IntegerCompoundAssignmentOps
{
    int empty_struct_member;
};

void IntegerCompoundAssignmentOps_InitGlobals()
{
}

void IntegerCompoundAssignmentOps_PreConstructor(IntegerCompoundAssignmentOps self)
{
}

void IntegerCompoundAssignmentOps_DefaultConstructor(IntegerCompoundAssignmentOps self)
{
    IntegerCompoundAssignmentOps_PreConstructor(self);
}

void IntegerCompoundAssignmentOps_CopyInputs(IntegerCompoundAssignmentOps self)
{
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

void IntegerCompoundAssignmentOps_CopyOutputs(IntegerCompoundAssignmentOps self)
{
}

void main()
{
    IntegerCompoundAssignmentOps_InitGlobals();
    IntegerCompoundAssignmentOps self;
    IntegerCompoundAssignmentOps_DefaultConstructor(self);
    IntegerCompoundAssignmentOps_CopyInputs(self);
    Main(self);
    IntegerCompoundAssignmentOps_CopyOutputs(self);
}

