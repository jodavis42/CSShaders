#version 450

struct OpIAddTest
{
    int empty_struct_member;
};

void OpIAddTest_InitGlobals()
{
}

void OpIAddTest_PreConstructor(OpIAddTest self)
{
}

void OpIAddTest_DefaultConstructor(OpIAddTest self)
{
    OpIAddTest_PreConstructor(self);
}

void OpIAddTest_CopyInputs(OpIAddTest self)
{
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

void OpIAddTest_CopyOutputs(OpIAddTest self)
{
}

void main()
{
    OpIAddTest_InitGlobals();
    OpIAddTest self;
    OpIAddTest_DefaultConstructor(self);
    OpIAddTest_CopyInputs(self);
    Main(self);
    OpIAddTest_CopyOutputs(self);
}

