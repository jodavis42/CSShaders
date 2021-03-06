#version 450

struct OpAllTest
{
    int empty_struct_member;
};

void OpAllTest_PreConstructor(OpAllTest self)
{
}

void OpAllTest_DefaultConstructor(OpAllTest self)
{
    OpAllTest_PreConstructor(self);
}

void Main(OpAllTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    boolVal = all(bool2Val);
    boolVal = all(bool3Val);
    boolVal = all(bool4Val);
}

void main()
{
    OpAllTest self;
    OpAllTest_DefaultConstructor(self);
    Main(self);
}

