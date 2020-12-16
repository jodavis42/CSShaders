#version 450

struct OpLogicalNotTest
{
    int empty_struct_member;
};

void OpLogicalNotTest_InitGlobals()
{
}

void OpLogicalNotTest_PreConstructor(OpLogicalNotTest self)
{
}

void OpLogicalNotTest_DefaultConstructor(OpLogicalNotTest self)
{
    OpLogicalNotTest_PreConstructor(self);
}

void OpLogicalNotTest_CopyInputs(OpLogicalNotTest self)
{
}

void Main(OpLogicalNotTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = !boolVal;
    bool2Val = not(bool2Val);
    bool3Val = not(bool3Val);
    bool4Val = not(bool4Val);
}

void OpLogicalNotTest_CopyOutputs(OpLogicalNotTest self)
{
}

void main()
{
    OpLogicalNotTest_InitGlobals();
    OpLogicalNotTest self;
    OpLogicalNotTest_DefaultConstructor(self);
    OpLogicalNotTest_CopyInputs(self);
    Main(self);
    OpLogicalNotTest_CopyOutputs(self);
}

