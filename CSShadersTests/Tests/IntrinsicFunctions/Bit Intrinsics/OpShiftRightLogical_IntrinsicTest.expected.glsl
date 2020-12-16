#version 450

struct OpShiftRightLogicalTest
{
    int empty_struct_member;
};

void OpShiftRightLogicalTest_InitGlobals()
{
}

void OpShiftRightLogicalTest_PreConstructor(OpShiftRightLogicalTest self)
{
}

void OpShiftRightLogicalTest_DefaultConstructor(OpShiftRightLogicalTest self)
{
    OpShiftRightLogicalTest_PreConstructor(self);
}

void OpShiftRightLogicalTest_CopyInputs(OpShiftRightLogicalTest self)
{
}

void Main(OpShiftRightLogicalTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = int(uint(intVal) >> uint(intVal));
    integer2Val = ivec2(uvec2(integer2Val) >> uvec2(integer2Val));
    integer3Val = ivec3(uvec3(integer3Val) >> uvec3(integer3Val));
    integer4Val = ivec4(uvec4(integer4Val) >> uvec4(integer4Val));
}

void OpShiftRightLogicalTest_CopyOutputs(OpShiftRightLogicalTest self)
{
}

void main()
{
    OpShiftRightLogicalTest_InitGlobals();
    OpShiftRightLogicalTest self;
    OpShiftRightLogicalTest_DefaultConstructor(self);
    OpShiftRightLogicalTest_CopyInputs(self);
    Main(self);
    OpShiftRightLogicalTest_CopyOutputs(self);
}

