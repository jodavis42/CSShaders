#version 450

struct OpIMulTest
{
    int empty_struct_member;
};

void OpIMulTest_PreConstructor(OpIMulTest self)
{
}

void OpIMulTest_DefaultConstructor(OpIMulTest self)
{
    OpIMulTest_PreConstructor(self);
}

void Main(OpIMulTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal *= intVal;
    integer2Val *= integer2Val;
    integer3Val *= integer3Val;
    integer4Val *= integer4Val;
}

void main()
{
    OpIMulTest self;
    OpIMulTest_DefaultConstructor(self);
    Main(self);
}

