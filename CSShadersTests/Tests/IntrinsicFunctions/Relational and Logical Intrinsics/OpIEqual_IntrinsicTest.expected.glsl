#version 450

struct OpIEqualTest
{
    int empty_struct_member;
};

void OpIEqualTest_InitGlobals()
{
}

void OpIEqualTest_PreConstructor(OpIEqualTest self)
{
}

void OpIEqualTest_DefaultConstructor(OpIEqualTest self)
{
    OpIEqualTest_PreConstructor(self);
}

void OpIEqualTest_CopyInputs(OpIEqualTest self)
{
}

void Main(OpIEqualTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal == intVal;
    bool2Val = equal(integer2Val, integer2Val);
    bool3Val = equal(integer3Val, integer3Val);
    bool4Val = equal(integer4Val, integer4Val);
}

void OpIEqualTest_CopyOutputs(OpIEqualTest self)
{
}

void main()
{
    OpIEqualTest_InitGlobals();
    OpIEqualTest self;
    OpIEqualTest_DefaultConstructor(self);
    OpIEqualTest_CopyInputs(self);
    Main(self);
    OpIEqualTest_CopyOutputs(self);
}

