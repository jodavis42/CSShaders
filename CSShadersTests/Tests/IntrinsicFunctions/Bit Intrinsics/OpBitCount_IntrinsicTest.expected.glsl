#version 450

struct OpBitCountTest
{
    int empty_struct_member;
};

void OpBitCountTest_PreConstructor(OpBitCountTest self)
{
}

void OpBitCountTest_DefaultConstructor(OpBitCountTest self)
{
    OpBitCountTest_PreConstructor(self);
}

void Main(OpBitCountTest self)
{
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    intVal = bitCount(intVal);
    integer2Val = bitCount(integer2Val);
    integer3Val = bitCount(integer3Val);
    integer4Val = bitCount(integer4Val);
}

void main()
{
    OpBitCountTest self;
    OpBitCountTest_DefaultConstructor(self);
    Main(self);
}

