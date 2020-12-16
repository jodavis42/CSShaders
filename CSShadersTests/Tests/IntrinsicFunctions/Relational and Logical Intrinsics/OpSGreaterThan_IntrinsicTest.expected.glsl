#version 450

struct OpSGreaterThanTest
{
    int empty_struct_member;
};

void OpSGreaterThanTest_InitGlobals()
{
}

void OpSGreaterThanTest_PreConstructor(OpSGreaterThanTest self)
{
}

void OpSGreaterThanTest_DefaultConstructor(OpSGreaterThanTest self)
{
    OpSGreaterThanTest_PreConstructor(self);
}

void OpSGreaterThanTest_CopyInputs(OpSGreaterThanTest self)
{
}

void Main(OpSGreaterThanTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal > intVal;
    bool2Val = greaterThan(integer2Val, integer2Val);
    bool3Val = greaterThan(integer3Val, integer3Val);
    bool4Val = greaterThan(integer4Val, integer4Val);
}

void OpSGreaterThanTest_CopyOutputs(OpSGreaterThanTest self)
{
}

void main()
{
    OpSGreaterThanTest_InitGlobals();
    OpSGreaterThanTest self;
    OpSGreaterThanTest_DefaultConstructor(self);
    OpSGreaterThanTest_CopyInputs(self);
    Main(self);
    OpSGreaterThanTest_CopyOutputs(self);
}

