#version 450

struct OpSGreaterThanEqualTest
{
    int empty_struct_member;
};

void OpSGreaterThanEqualTest_InitGlobals()
{
}

void OpSGreaterThanEqualTest_PreConstructor(OpSGreaterThanEqualTest self)
{
}

void OpSGreaterThanEqualTest_DefaultConstructor(OpSGreaterThanEqualTest self)
{
    OpSGreaterThanEqualTest_PreConstructor(self);
}

void OpSGreaterThanEqualTest_CopyInputs(OpSGreaterThanEqualTest self)
{
}

void Main(OpSGreaterThanEqualTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal >= intVal;
    bool2Val = greaterThanEqual(integer2Val, integer2Val);
    bool3Val = greaterThanEqual(integer3Val, integer3Val);
    bool4Val = greaterThanEqual(integer4Val, integer4Val);
}

void OpSGreaterThanEqualTest_CopyOutputs(OpSGreaterThanEqualTest self)
{
}

void main()
{
    OpSGreaterThanEqualTest_InitGlobals();
    OpSGreaterThanEqualTest self;
    OpSGreaterThanEqualTest_DefaultConstructor(self);
    OpSGreaterThanEqualTest_CopyInputs(self);
    Main(self);
    OpSGreaterThanEqualTest_CopyOutputs(self);
}

