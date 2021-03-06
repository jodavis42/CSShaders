#version 450

struct OpIAddTest
{
    int empty_struct_member;
};

void OpIAddTest_PreConstructor(OpIAddTest self)
{
}

void OpIAddTest_DefaultConstructor(OpIAddTest self)
{
    OpIAddTest_PreConstructor(self);
}

void Main(OpIAddTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal += intVal;
    integer2Val += integer2Val;
    integer3Val += integer3Val;
    integer4Val += integer4Val;
}

void main()
{
    OpIAddTest self;
    OpIAddTest_DefaultConstructor(self);
    Main(self);
}

