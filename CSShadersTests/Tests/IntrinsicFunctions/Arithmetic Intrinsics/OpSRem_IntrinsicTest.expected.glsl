#version 450

struct OpSRemTest
{
    int empty_struct_member;
};

void OpSRemTest_InitGlobals()
{
}

void OpSRemTest_PreConstructor(OpSRemTest self)
{
}

void OpSRemTest_DefaultConstructor(OpSRemTest self)
{
    OpSRemTest_PreConstructor(self);
}

void OpSRemTest_CopyInputs(OpSRemTest self)
{
}

void Main(OpSRemTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal -= intVal * (intVal / intVal);
    integer2Val -= integer2Val * (integer2Val / integer2Val);
    integer3Val -= integer3Val * (integer3Val / integer3Val);
    integer4Val -= integer4Val * (integer4Val / integer4Val);
}

void OpSRemTest_CopyOutputs(OpSRemTest self)
{
}

void main()
{
    OpSRemTest_InitGlobals();
    OpSRemTest self;
    OpSRemTest_DefaultConstructor(self);
    OpSRemTest_CopyInputs(self);
    Main(self);
    OpSRemTest_CopyOutputs(self);
}
