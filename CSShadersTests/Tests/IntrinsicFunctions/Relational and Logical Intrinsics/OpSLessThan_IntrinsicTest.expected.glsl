#version 450

struct OpSLessThanTest
{
    int empty_struct_member;
};

void OpSLessThanTest_InitGlobals()
{
}

void OpSLessThanTest_PreConstructor(OpSLessThanTest self)
{
}

void OpSLessThanTest_DefaultConstructor(OpSLessThanTest self)
{
    OpSLessThanTest_PreConstructor(self);
}

void OpSLessThanTest_CopyInputs(OpSLessThanTest self)
{
}

void Main(OpSLessThanTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal < intVal;
    bool2Val = lessThan(integer2Val, integer2Val);
    bool3Val = lessThan(integer3Val, integer3Val);
    bool4Val = lessThan(integer4Val, integer4Val);
}

void OpSLessThanTest_CopyOutputs(OpSLessThanTest self)
{
}

void main()
{
    OpSLessThanTest_InitGlobals();
    OpSLessThanTest self;
    OpSLessThanTest_DefaultConstructor(self);
    OpSLessThanTest_CopyInputs(self);
    Main(self);
    OpSLessThanTest_CopyOutputs(self);
}

