#version 450

struct VectorPrimitiveFieldSwizzlesTest
{
    int empty_struct_member;
};

void VectorPrimitiveFieldSwizzlesTest_PreConstructor(VectorPrimitiveFieldSwizzlesTest self)
{
}

void VectorPrimitiveFieldSwizzlesTest_DefaultConstructor(VectorPrimitiveFieldSwizzlesTest self)
{
    VectorPrimitiveFieldSwizzlesTest_PreConstructor(self);
}

void Main(VectorPrimitiveFieldSwizzlesTest self)
{
    vec2 vector0 = vec2(0.0);
    vector0.y = vector0.x;
}

void main()
{
    VectorPrimitiveFieldSwizzlesTest self;
    VectorPrimitiveFieldSwizzlesTest_DefaultConstructor(self);
    Main(self);
}

