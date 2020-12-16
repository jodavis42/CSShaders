#version 450

struct OpSLessThanEqualTest
{
    int empty_struct_member;
};

void OpSLessThanEqualTest_InitGlobals()
{
}

void OpSLessThanEqualTest_PreConstructor(OpSLessThanEqualTest self)
{
}

void OpSLessThanEqualTest_DefaultConstructor(OpSLessThanEqualTest self)
{
    OpSLessThanEqualTest_PreConstructor(self);
}

void OpSLessThanEqualTest_CopyInputs(OpSLessThanEqualTest self)
{
}

void Main(OpSLessThanEqualTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal <= intVal;
    bool2Val = lessThanEqual(integer2Val, integer2Val);
    bool3Val = lessThanEqual(integer3Val, integer3Val);
    bool4Val = lessThanEqual(integer4Val, integer4Val);
}

void OpSLessThanEqualTest_CopyOutputs(OpSLessThanEqualTest self)
{
}

void main()
{
    OpSLessThanEqualTest_InitGlobals();
    OpSLessThanEqualTest self;
    OpSLessThanEqualTest_DefaultConstructor(self);
    OpSLessThanEqualTest_CopyInputs(self);
    Main(self);
    OpSLessThanEqualTest_CopyOutputs(self);
}

