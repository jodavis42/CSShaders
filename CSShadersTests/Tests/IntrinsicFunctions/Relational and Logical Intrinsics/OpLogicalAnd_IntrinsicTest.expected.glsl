#version 450

struct OpLogicalAndTest
{
    int empty_struct_member;
};

void OpLogicalAndTest_PreConstructor(OpLogicalAndTest self)
{
}

void OpLogicalAndTest_DefaultConstructor(OpLogicalAndTest self)
{
    OpLogicalAndTest_PreConstructor(self);
}

void Main(OpLogicalAndTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = boolVal && boolVal;
    bool2Val = bvec2(bool2Val.x && bool2Val.x, bool2Val.y && bool2Val.y);
    bool3Val = bvec3(bool3Val.x && bool3Val.x, bool3Val.y && bool3Val.y, bool3Val.z && bool3Val.z);
    bool4Val = bvec4(bool4Val.x && bool4Val.x, bool4Val.y && bool4Val.y, bool4Val.z && bool4Val.z, bool4Val.w && bool4Val.w);
}

void main()
{
    OpLogicalAndTest self;
    OpLogicalAndTest_DefaultConstructor(self);
    Main(self);
}

