#version 450

struct OpINotEqualTest
{
    int empty_struct_member;
};

void OpINotEqualTest_PreConstructor(OpINotEqualTest self)
{
}

void OpINotEqualTest_DefaultConstructor(OpINotEqualTest self)
{
    OpINotEqualTest_PreConstructor(self);
}

void Main(OpINotEqualTest self)
{
    bool boolVal = false;
    int intVal = 0;
    bvec2 bool2Val = bvec2(false);
    ivec2 integer2Val = ivec2(0);
    bvec3 bool3Val = bvec3(false);
    ivec3 integer3Val = ivec3(0);
    bvec4 bool4Val = bvec4(false);
    ivec4 integer4Val = ivec4(0);
    boolVal = intVal != intVal;
    bool2Val = notEqual(integer2Val, integer2Val);
    bool3Val = notEqual(integer3Val, integer3Val);
    bool4Val = notEqual(integer4Val, integer4Val);
}

void main()
{
    OpINotEqualTest self;
    OpINotEqualTest_DefaultConstructor(self);
    Main(self);
}

