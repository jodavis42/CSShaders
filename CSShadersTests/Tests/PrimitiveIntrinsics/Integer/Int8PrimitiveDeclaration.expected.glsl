#version 450
#extension GL_EXT_shader_explicit_arithmetic_types_int8 : require

struct Int8PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Int8PrimitiveDeclarationTest_InitGlobals()
{
}

void Int8PrimitiveDeclarationTest_PreConstructor(Int8PrimitiveDeclarationTest self)
{
}

void Int8PrimitiveDeclarationTest_DefaultConstructor(Int8PrimitiveDeclarationTest self)
{
    Int8PrimitiveDeclarationTest_PreConstructor(self);
}

void Int8PrimitiveDeclarationTest_CopyInputs(Int8PrimitiveDeclarationTest self)
{
}

void Main(Int8PrimitiveDeclarationTest self)
{
    int8_t int8Val = int8_t(0);
}

void Int8PrimitiveDeclarationTest_CopyOutputs(Int8PrimitiveDeclarationTest self)
{
}

void main()
{
    Int8PrimitiveDeclarationTest_InitGlobals();
    Int8PrimitiveDeclarationTest self;
    Int8PrimitiveDeclarationTest_DefaultConstructor(self);
    Int8PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    Int8PrimitiveDeclarationTest_CopyOutputs(self);
}

