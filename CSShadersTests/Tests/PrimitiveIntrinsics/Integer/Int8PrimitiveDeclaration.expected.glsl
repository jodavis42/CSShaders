#version 450
#extension GL_EXT_shader_explicit_arithmetic_types_int8 : require

struct Int8PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Int8PrimitiveDeclarationTest_PreConstructor(Int8PrimitiveDeclarationTest self)
{
}

void Int8PrimitiveDeclarationTest_DefaultConstructor(Int8PrimitiveDeclarationTest self)
{
    Int8PrimitiveDeclarationTest_PreConstructor(self);
}

void Main(Int8PrimitiveDeclarationTest self)
{
    int8_t int8Val = int8_t(0);
}

void main()
{
    Int8PrimitiveDeclarationTest self;
    Int8PrimitiveDeclarationTest_DefaultConstructor(self);
    Main(self);
}

