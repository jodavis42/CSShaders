#version 450

struct SSignTest
{
    int empty_struct_member;
};

void SSignTest_InitGlobals()
{
}

void SSignTest_PreConstructor(SSignTest self)
{
}

void SSignTest_DefaultConstructor(SSignTest self)
{
    SSignTest_PreConstructor(self);
}

void SSignTest_CopyInputs(SSignTest self)
{
}

void Main(SSignTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = sign(intVal);
    integer2Val = sign(integer2Val);
    integer3Val = sign(integer3Val);
    integer4Val = sign(integer4Val);
}

void SSignTest_CopyOutputs(SSignTest self)
{
}

void main()
{
    SSignTest_InitGlobals();
    SSignTest self;
    SSignTest_DefaultConstructor(self);
    SSignTest_CopyInputs(self);
    Main(self);
    SSignTest_CopyOutputs(self);
}

