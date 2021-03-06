#version 450

struct OpAnyTest
{
    int empty_struct_member;
};

void OpAnyTest_PreConstructor(OpAnyTest self)
{
}

void OpAnyTest_DefaultConstructor(OpAnyTest self)
{
    OpAnyTest_PreConstructor(self);
}

void Main(OpAnyTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = any(bool2Val);
    boolVal = any(bool3Val);
    boolVal = any(bool4Val);
}

void main()
{
    OpAnyTest self;
    OpAnyTest_DefaultConstructor(self);
    Main(self);
}

