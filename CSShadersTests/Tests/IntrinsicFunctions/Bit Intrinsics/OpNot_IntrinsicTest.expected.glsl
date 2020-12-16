#version 450

struct OpNotTest
{
    int empty_struct_member;
};

void OpNotTest_InitGlobals()
{
}

void OpNotTest_PreConstructor(OpNotTest self)
{
}

void OpNotTest_DefaultConstructor(OpNotTest self)
{
    OpNotTest_PreConstructor(self);
}

void OpNotTest_CopyInputs(OpNotTest self)
{
}

void Main(OpNotTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = ~intVal;
    integer2Val = ~integer2Val;
    integer3Val = ~integer3Val;
    integer4Val = ~integer4Val;
}

void OpNotTest_CopyOutputs(OpNotTest self)
{
}

void main()
{
    OpNotTest_InitGlobals();
    OpNotTest self;
    OpNotTest_DefaultConstructor(self);
    OpNotTest_CopyInputs(self);
    Main(self);
    OpNotTest_CopyOutputs(self);
}

