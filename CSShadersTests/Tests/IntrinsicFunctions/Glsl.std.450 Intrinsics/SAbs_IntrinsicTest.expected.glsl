#version 450

struct SAbsTest
{
    int empty_struct_member;
};

void SAbsTest_PreConstructor(SAbsTest self)
{
}

void SAbsTest_DefaultConstructor(SAbsTest self)
{
    SAbsTest_PreConstructor(self);
}

void Main(SAbsTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = abs(intVal);
    integer2Val = abs(integer2Val);
    integer3Val = abs(integer3Val);
    integer4Val = abs(integer4Val);
}

void main()
{
    SAbsTest self;
    SAbsTest_DefaultConstructor(self);
    Main(self);
}

