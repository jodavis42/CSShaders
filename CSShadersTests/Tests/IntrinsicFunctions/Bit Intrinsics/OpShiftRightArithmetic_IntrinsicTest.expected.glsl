#version 450

struct OpShiftRightArithmeticTest
{
    int empty_struct_member;
};

void OpShiftRightArithmeticTest_InitGlobals()
{
}

void OpShiftRightArithmeticTest_PreConstructor(OpShiftRightArithmeticTest self)
{
}

void OpShiftRightArithmeticTest_DefaultConstructor(OpShiftRightArithmeticTest self)
{
    OpShiftRightArithmeticTest_PreConstructor(self);
}

void OpShiftRightArithmeticTest_CopyInputs(OpShiftRightArithmeticTest self)
{
}

void Main(OpShiftRightArithmeticTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = intVal >> intVal;
    integer2Val = integer2Val >> integer2Val;
    integer3Val = integer3Val >> integer3Val;
    integer4Val = integer4Val >> integer4Val;
}

void OpShiftRightArithmeticTest_CopyOutputs(OpShiftRightArithmeticTest self)
{
}

void main()
{
    OpShiftRightArithmeticTest_InitGlobals();
    OpShiftRightArithmeticTest self;
    OpShiftRightArithmeticTest_DefaultConstructor(self);
    OpShiftRightArithmeticTest_CopyInputs(self);
    Main(self);
    OpShiftRightArithmeticTest_CopyOutputs(self);
}

