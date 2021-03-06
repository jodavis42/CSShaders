#version 450

struct SMaxTest
{
    int empty_struct_member;
};

void SMaxTest_PreConstructor(SMaxTest self)
{
}

void SMaxTest_DefaultConstructor(SMaxTest self)
{
    SMaxTest_PreConstructor(self);
}

void Main(SMaxTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = max(intVal, intVal);
    integer2Val = max(integer2Val, integer2Val);
    integer3Val = max(integer3Val, integer3Val);
    integer4Val = max(integer4Val, integer4Val);
}

void main()
{
    SMaxTest self;
    SMaxTest_DefaultConstructor(self);
    Main(self);
}

