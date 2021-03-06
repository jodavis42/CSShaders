#version 450

struct SClampTest
{
    int empty_struct_member;
};

void SClampTest_PreConstructor(SClampTest self)
{
}

void SClampTest_DefaultConstructor(SClampTest self)
{
    SClampTest_PreConstructor(self);
}

void Main(SClampTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = clamp(intVal, intVal, intVal);
    integer2Val = clamp(integer2Val, integer2Val, integer2Val);
    integer3Val = clamp(integer3Val, integer3Val, integer3Val);
    integer4Val = clamp(integer4Val, integer4Val, integer4Val);
}

void main()
{
    SClampTest self;
    SClampTest_DefaultConstructor(self);
    Main(self);
}

