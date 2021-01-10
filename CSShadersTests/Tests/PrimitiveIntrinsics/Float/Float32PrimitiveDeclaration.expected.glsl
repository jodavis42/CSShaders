#version 450

struct Float32PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Float32PrimitiveDeclarationTest_InitGlobals()
{
}

void Float32PrimitiveDeclarationTest_PreConstructor(Float32PrimitiveDeclarationTest self)
{
}

void Float32PrimitiveDeclarationTest_DefaultConstructor(Float32PrimitiveDeclarationTest self)
{
    Float32PrimitiveDeclarationTest_PreConstructor(self);
}

void Float32PrimitiveDeclarationTest_CopyInputs(Float32PrimitiveDeclarationTest self)
{
}

void Main(Float32PrimitiveDeclarationTest self)
{
    float float32Val = 0.0;
}

void Float32PrimitiveDeclarationTest_CopyOutputs(Float32PrimitiveDeclarationTest self)
{
}

void main()
{
    Float32PrimitiveDeclarationTest_InitGlobals();
    Float32PrimitiveDeclarationTest self;
    Float32PrimitiveDeclarationTest_DefaultConstructor(self);
    Float32PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    Float32PrimitiveDeclarationTest_CopyOutputs(self);
}

