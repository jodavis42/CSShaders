#version 450

struct OpBitwiseAndTest
{
    int empty_struct_member;
};

void OpBitwiseAndTest_PreConstructor(OpBitwiseAndTest self)
{
}

void OpBitwiseAndTest_DefaultConstructor(OpBitwiseAndTest self)
{
    OpBitwiseAndTest_PreConstructor(self);
}

void Main(OpBitwiseAndTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal &= intVal;
    integer2Val &= integer2Val;
    integer3Val &= integer3Val;
    integer4Val &= integer4Val;
}

void main()
{
    OpBitwiseAndTest self;
    OpBitwiseAndTest_DefaultConstructor(self);
    Main(self);
}

