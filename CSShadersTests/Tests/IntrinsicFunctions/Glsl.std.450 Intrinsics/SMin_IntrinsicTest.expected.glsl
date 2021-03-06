#version 450

struct SMinTest
{
    int empty_struct_member;
};

void SMinTest_PreConstructor(SMinTest self)
{
}

void SMinTest_DefaultConstructor(SMinTest self)
{
    SMinTest_PreConstructor(self);
}

void Main(SMinTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = min(intVal, intVal);
    integer2Val = min(integer2Val, integer2Val);
    integer3Val = min(integer3Val, integer3Val);
    integer4Val = min(integer4Val, integer4Val);
}

void main()
{
    SMinTest self;
    SMinTest_DefaultConstructor(self);
    Main(self);
}

