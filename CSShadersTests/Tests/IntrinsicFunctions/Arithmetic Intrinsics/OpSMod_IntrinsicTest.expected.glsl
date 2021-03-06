#version 450

struct OpSModTest
{
    int empty_struct_member;
};

void OpSModTest_PreConstructor(OpSModTest self)
{
}

void OpSModTest_DefaultConstructor(OpSModTest self)
{
    OpSModTest_PreConstructor(self);
}

void Main(OpSModTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal %= intVal;
    integer2Val %= integer2Val;
    integer3Val %= integer3Val;
    integer4Val %= integer4Val;
}

void main()
{
    OpSModTest self;
    OpSModTest_DefaultConstructor(self);
    Main(self);
}

