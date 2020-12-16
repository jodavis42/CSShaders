#version 450

struct OpSDivTest
{
    int empty_struct_member;
};

void OpSDivTest_InitGlobals()
{
}

void OpSDivTest_PreConstructor(OpSDivTest self)
{
}

void OpSDivTest_DefaultConstructor(OpSDivTest self)
{
    OpSDivTest_PreConstructor(self);
}

void OpSDivTest_CopyInputs(OpSDivTest self)
{
}

void Main(OpSDivTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal /= intVal;
    integer2Val /= integer2Val;
    integer3Val /= integer3Val;
    integer4Val /= integer4Val;
}

void OpSDivTest_CopyOutputs(OpSDivTest self)
{
}

void main()
{
    OpSDivTest_InitGlobals();
    OpSDivTest self;
    OpSDivTest_DefaultConstructor(self);
    OpSDivTest_CopyInputs(self);
    Main(self);
    OpSDivTest_CopyOutputs(self);
}

