#version 450

struct OpShiftLeftLogicalTest
{
    int empty_struct_member;
};

void OpShiftLeftLogicalTest_PreConstructor(OpShiftLeftLogicalTest self)
{
}

void OpShiftLeftLogicalTest_DefaultConstructor(OpShiftLeftLogicalTest self)
{
    OpShiftLeftLogicalTest_PreConstructor(self);
}

void Main(OpShiftLeftLogicalTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = intVal << intVal;
    integer2Val = integer2Val << integer2Val;
    integer3Val = integer3Val << integer3Val;
    integer4Val = integer4Val << integer4Val;
}

void main()
{
    OpShiftLeftLogicalTest self;
    OpShiftLeftLogicalTest_DefaultConstructor(self);
    Main(self);
}

