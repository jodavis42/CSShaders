#version 450

struct VectorPrimitivePropertySwizzlesTest
{
    int empty_struct_member;
};

void PreConstructor_VectorPrimitivePropertySwizzlesTest(VectorPrimitivePropertySwizzlesTest self)
{
}

void DefaultConstructor_VectorPrimitivePropertySwizzlesTest(VectorPrimitivePropertySwizzlesTest self)
{
    PreConstructor_VectorPrimitivePropertySwizzlesTest(self);
}

void Main(VectorPrimitivePropertySwizzlesTest self)
{
    vec2 vector0 = vec2(0.0);
    vec2 vXX = vector0.xx;
    vec2 vYY = vector0.yy;
    vector0 = vec2(vector0.yx.x, vector0.yx.y);
    vector0 = vec2(vector0.xy.x, vector0.xy.y);
}

void main()
{
    VectorPrimitivePropertySwizzlesTest self;
    DefaultConstructor_VectorPrimitivePropertySwizzlesTest(self);
    Main(self);
}

