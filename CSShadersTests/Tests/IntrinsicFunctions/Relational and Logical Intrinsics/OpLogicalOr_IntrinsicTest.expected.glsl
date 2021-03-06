#version 450

struct OpLogicalOrTest
{
    int empty_struct_member;
};

void OpLogicalOrTest_PreConstructor(OpLogicalOrTest self)
{
}

void OpLogicalOrTest_DefaultConstructor(OpLogicalOrTest self)
{
    OpLogicalOrTest_PreConstructor(self);
}

void Main(OpLogicalOrTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = boolVal || boolVal;
    bool2Val = bvec2(bool2Val.x || bool2Val.x, bool2Val.y || bool2Val.y);
    bool3Val = bvec3(bool3Val.x || bool3Val.x, bool3Val.y || bool3Val.y, bool3Val.z || bool3Val.z);
    bool4Val = bvec4(bool4Val.x || bool4Val.x, bool4Val.y || bool4Val.y, bool4Val.z || bool4Val.z, bool4Val.w || bool4Val.w);
}

void main()
{
    OpLogicalOrTest self;
    OpLogicalOrTest_DefaultConstructor(self);
    Main(self);
}

