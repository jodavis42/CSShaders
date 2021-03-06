#version 450

struct VectorPrimitiveCompositeConstructorsTest
{
    int empty_struct_member;
};

void VectorPrimitiveCompositeConstructorsTest_PreConstructor(VectorPrimitiveCompositeConstructorsTest self)
{
}

void VectorPrimitiveCompositeConstructorsTest_DefaultConstructor(VectorPrimitiveCompositeConstructorsTest self)
{
    VectorPrimitiveCompositeConstructorsTest_PreConstructor(self);
}

void Main(VectorPrimitiveCompositeConstructorsTest self)
{
    vec2 vector0 = vec2(1.0);
    vec2 vector1 = vec2(1.0, 2.0);
}

void main()
{
    VectorPrimitiveCompositeConstructorsTest self;
    VectorPrimitiveCompositeConstructorsTest_DefaultConstructor(self);
    Main(self);
}

