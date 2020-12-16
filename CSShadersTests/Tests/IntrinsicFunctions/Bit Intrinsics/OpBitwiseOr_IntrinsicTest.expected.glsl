#version 450

struct OpBitwiseOrTest
{
    int empty_struct_member;
};

void OpBitwiseOrTest_InitGlobals()
{
}

void OpBitwiseOrTest_PreConstructor(OpBitwiseOrTest self)
{
}

void OpBitwiseOrTest_DefaultConstructor(OpBitwiseOrTest self)
{
    OpBitwiseOrTest_PreConstructor(self);
}

void OpBitwiseOrTest_CopyInputs(OpBitwiseOrTest self)
{
}

void Main(OpBitwiseOrTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal |= intVal;
    integer2Val |= integer2Val;
    integer3Val |= integer3Val;
    integer4Val |= integer4Val;
}

void OpBitwiseOrTest_CopyOutputs(OpBitwiseOrTest self)
{
}

void main()
{
    OpBitwiseOrTest_InitGlobals();
    OpBitwiseOrTest self;
    OpBitwiseOrTest_DefaultConstructor(self);
    OpBitwiseOrTest_CopyInputs(self);
    Main(self);
    OpBitwiseOrTest_CopyOutputs(self);
}

