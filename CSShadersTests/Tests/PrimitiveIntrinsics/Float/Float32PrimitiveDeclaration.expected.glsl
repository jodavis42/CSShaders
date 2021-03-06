#version 450

struct Float32PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Float32PrimitiveDeclarationTest_PreConstructor(Float32PrimitiveDeclarationTest self)
{
}

void Float32PrimitiveDeclarationTest_DefaultConstructor(Float32PrimitiveDeclarationTest self)
{
    Float32PrimitiveDeclarationTest_PreConstructor(self);
}

void Main(Float32PrimitiveDeclarationTest self)
{
    float float32Val = 0.0;
}

void main()
{
    Float32PrimitiveDeclarationTest self;
    Float32PrimitiveDeclarationTest_DefaultConstructor(self);
    Main(self);
}

