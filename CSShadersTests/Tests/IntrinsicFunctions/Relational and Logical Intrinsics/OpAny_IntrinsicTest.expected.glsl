#version 450

struct OpAnyTest
{
    int empty_struct_member;
};

void OpAnyTest_InitGlobals()
{
}

void OpAnyTest_PreConstructor(OpAnyTest self)
{
}

void OpAnyTest_DefaultConstructor(OpAnyTest self)
{
    OpAnyTest_PreConstructor(self);
}

void OpAnyTest_CopyInputs(OpAnyTest self)
{
}

void Main(OpAnyTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = any(bool2Val);
    boolVal = any(bool3Val);
    boolVal = any(bool4Val);
}

void OpAnyTest_CopyOutputs(OpAnyTest self)
{
}

void main()
{
    OpAnyTest_InitGlobals();
    OpAnyTest self;
    OpAnyTest_DefaultConstructor(self);
    OpAnyTest_CopyInputs(self);
    Main(self);
    OpAnyTest_CopyOutputs(self);
}

