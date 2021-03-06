#version 450

struct OpLogicalEqualTest
{
    int empty_struct_member;
};

void OpLogicalEqualTest_PreConstructor(OpLogicalEqualTest self)
{
}

void OpLogicalEqualTest_DefaultConstructor(OpLogicalEqualTest self)
{
    OpLogicalEqualTest_PreConstructor(self);
}

void Main(OpLogicalEqualTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = boolVal == boolVal;
    bool2Val = equal(bool2Val, bool2Val);
    bool3Val = equal(bool3Val, bool3Val);
    bool4Val = equal(bool4Val, bool4Val);
}

void main()
{
    OpLogicalEqualTest self;
    OpLogicalEqualTest_DefaultConstructor(self);
    Main(self);
}

