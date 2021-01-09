#version 450

struct OpLogicalNotEqualTest
{
    int empty_struct_member;
};

void OpLogicalNotEqualTest_InitGlobals()
{
}

void OpLogicalNotEqualTest_PreConstructor(OpLogicalNotEqualTest self)
{
}

void OpLogicalNotEqualTest_DefaultConstructor(OpLogicalNotEqualTest self)
{
    OpLogicalNotEqualTest_PreConstructor(self);
}

void OpLogicalNotEqualTest_CopyInputs(OpLogicalNotEqualTest self)
{
}

void Main(OpLogicalNotEqualTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = boolVal != boolVal;
    bool2Val = notEqual(bool2Val, bool2Val);
    bool3Val = notEqual(bool3Val, bool3Val);
    bool4Val = notEqual(bool4Val, bool4Val);
}

void OpLogicalNotEqualTest_CopyOutputs(OpLogicalNotEqualTest self)
{
}

void main()
{
    OpLogicalNotEqualTest_InitGlobals();
    OpLogicalNotEqualTest self;
    OpLogicalNotEqualTest_DefaultConstructor(self);
    OpLogicalNotEqualTest_CopyInputs(self);
    Main(self);
    OpLogicalNotEqualTest_CopyOutputs(self);
}
