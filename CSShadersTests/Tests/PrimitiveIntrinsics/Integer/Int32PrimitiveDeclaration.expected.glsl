#version 450

struct Int32PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Int32PrimitiveDeclarationTest_PreConstructor(Int32PrimitiveDeclarationTest self)
{
}

void Int32PrimitiveDeclarationTest_DefaultConstructor(Int32PrimitiveDeclarationTest self)
{
    Int32PrimitiveDeclarationTest_PreConstructor(self);
}

void Main(Int32PrimitiveDeclarationTest self)
{
    int int32Val = 0;
}

void main()
{
    Int32PrimitiveDeclarationTest self;
    Int32PrimitiveDeclarationTest_DefaultConstructor(self);
    Main(self);
}

