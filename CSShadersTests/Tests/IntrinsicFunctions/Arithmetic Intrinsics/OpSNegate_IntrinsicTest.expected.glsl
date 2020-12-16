#version 450

struct OpSNegateTest
{
    int empty_struct_member;
};

void OpSNegateTest_InitGlobals()
{
}

void OpSNegateTest_PreConstructor(OpSNegateTest self)
{
}

void OpSNegateTest_DefaultConstructor(OpSNegateTest self)
{
    OpSNegateTest_PreConstructor(self);
}

void OpSNegateTest_CopyInputs(OpSNegateTest self)
{
}

void Main(OpSNegateTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = -intVal;
    integer2Val = -integer2Val;
    integer3Val = -integer3Val;
    integer4Val = -integer4Val;
}

void OpSNegateTest_CopyOutputs(OpSNegateTest self)
{
}

void main()
{
    OpSNegateTest_InitGlobals();
    OpSNegateTest self;
    OpSNegateTest_DefaultConstructor(self);
    OpSNegateTest_CopyInputs(self);
    Main(self);
    OpSNegateTest_CopyOutputs(self);
}

