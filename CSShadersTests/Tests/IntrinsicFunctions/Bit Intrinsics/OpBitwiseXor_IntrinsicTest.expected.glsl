#version 450

struct OpBitwiseXorTest
{
    int empty_struct_member;
};

void OpBitwiseXorTest_InitGlobals()
{
}

void OpBitwiseXorTest_PreConstructor(OpBitwiseXorTest self)
{
}

void OpBitwiseXorTest_DefaultConstructor(OpBitwiseXorTest self)
{
    OpBitwiseXorTest_PreConstructor(self);
}

void OpBitwiseXorTest_CopyInputs(OpBitwiseXorTest self)
{
}

void Main(OpBitwiseXorTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal ^= intVal;
    integer2Val ^= integer2Val;
    integer3Val ^= integer3Val;
    integer4Val ^= integer4Val;
}

void OpBitwiseXorTest_CopyOutputs(OpBitwiseXorTest self)
{
}

void main()
{
    OpBitwiseXorTest_InitGlobals();
    OpBitwiseXorTest self;
    OpBitwiseXorTest_DefaultConstructor(self);
    OpBitwiseXorTest_CopyInputs(self);
    Main(self);
    OpBitwiseXorTest_CopyOutputs(self);
}

