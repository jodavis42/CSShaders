#version 450

struct Int32PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Int32PrimitiveDeclarationTest_InitGlobals()
{
}

void Int32PrimitiveDeclarationTest_PreConstructor(Int32PrimitiveDeclarationTest self)
{
}

void Int32PrimitiveDeclarationTest_DefaultConstructor(Int32PrimitiveDeclarationTest self)
{
    Int32PrimitiveDeclarationTest_PreConstructor(self);
}

void Int32PrimitiveDeclarationTest_CopyInputs(Int32PrimitiveDeclarationTest self)
{
}

void Main(Int32PrimitiveDeclarationTest self)
{
    int int32Val = 0;
}

void Int32PrimitiveDeclarationTest_CopyOutputs(Int32PrimitiveDeclarationTest self)
{
}

void main()
{
    Int32PrimitiveDeclarationTest_InitGlobals();
    Int32PrimitiveDeclarationTest self;
    Int32PrimitiveDeclarationTest_DefaultConstructor(self);
    Int32PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    Int32PrimitiveDeclarationTest_CopyOutputs(self);
}

