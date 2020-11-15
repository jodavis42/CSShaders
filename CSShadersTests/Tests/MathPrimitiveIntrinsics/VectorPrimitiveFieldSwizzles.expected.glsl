#version 450

struct VectorPrimitiveFieldSwizzlesTest
{
    int empty_struct_member;
};

void PreConstructor_VectorPrimitiveFieldSwizzlesTest(VectorPrimitiveFieldSwizzlesTest self)
{
}

void DefaultConstructor_VectorPrimitiveFieldSwizzlesTest(VectorPrimitiveFieldSwizzlesTest self)
{
    PreConstructor_VectorPrimitiveFieldSwizzlesTest(self);
}

void Main(VectorPrimitiveFieldSwizzlesTest self)
{
    vec2 vector0 = vec2(0.0);
    vector0.y = vector0.x;
}

void main()
{
    VectorPrimitiveFieldSwizzlesTest self;
    DefaultConstructor_VectorPrimitiveFieldSwizzlesTest(self);
    Main(self);
}

