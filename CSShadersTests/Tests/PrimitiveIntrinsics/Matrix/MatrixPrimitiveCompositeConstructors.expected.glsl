#version 450

struct VectorPrimitiveCompositeConstructorsTest
{
    int empty_struct_member;
};

void PreConstructor_VectorPrimitiveCompositeConstructorsTest(VectorPrimitiveCompositeConstructorsTest self)
{
}

void DefaultConstructor_VectorPrimitiveCompositeConstructorsTest(VectorPrimitiveCompositeConstructorsTest self)
{
    PreConstructor_VectorPrimitiveCompositeConstructorsTest(self);
}

void Main(VectorPrimitiveCompositeConstructorsTest self)
{
    vec2 vector0 = vec2(0.0);
    vec2 vector1 = vec2(0.0);
    mat2 matrix0 = mat2(vector0, vector1);
}

void main()
{
    VectorPrimitiveCompositeConstructorsTest self;
    DefaultConstructor_VectorPrimitiveCompositeConstructorsTest(self);
    Main(self);
}

