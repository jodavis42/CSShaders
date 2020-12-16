#version 450

struct OpBitReverseTest
{
    int empty_struct_member;
};

void OpBitReverseTest_InitGlobals()
{
}

void OpBitReverseTest_PreConstructor(OpBitReverseTest self)
{
}

void OpBitReverseTest_DefaultConstructor(OpBitReverseTest self)
{
    OpBitReverseTest_PreConstructor(self);
}

void OpBitReverseTest_CopyInputs(OpBitReverseTest self)
{
}

void Main(OpBitReverseTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = bitfieldReverse(intVal);
    integer2Val = bitfieldReverse(integer2Val);
    integer3Val = bitfieldReverse(integer3Val);
    integer4Val = bitfieldReverse(integer4Val);
}

void OpBitReverseTest_CopyOutputs(OpBitReverseTest self)
{
}

void main()
{
    OpBitReverseTest_InitGlobals();
    OpBitReverseTest self;
    OpBitReverseTest_DefaultConstructor(self);
    OpBitReverseTest_CopyInputs(self);
    Main(self);
    OpBitReverseTest_CopyOutputs(self);
}

