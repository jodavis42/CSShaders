#version 450

struct OpISubTest
{
    int empty_struct_member;
};

void OpISubTest_PreConstructor(OpISubTest self)
{
}

void OpISubTest_DefaultConstructor(OpISubTest self)
{
    OpISubTest_PreConstructor(self);
}

void Main(OpISubTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal -= intVal;
    integer2Val -= integer2Val;
    integer3Val -= integer3Val;
    integer4Val -= integer4Val;
}

void main()
{
    OpISubTest self;
    OpISubTest_DefaultConstructor(self);
    Main(self);
}

