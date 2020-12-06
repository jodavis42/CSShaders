#version 450

struct VectorPrimitivePropertySwizzlesTest
{
    int empty_struct_member;
};

void VectorPrimitivePropertySwizzlesTest_InitGlobals()
{
}

void VectorPrimitivePropertySwizzlesTest_PreConstructor(VectorPrimitivePropertySwizzlesTest self)
{
}

void VectorPrimitivePropertySwizzlesTest_DefaultConstructor(VectorPrimitivePropertySwizzlesTest self)
{
    VectorPrimitivePropertySwizzlesTest_PreConstructor(self);
}

void VectorPrimitivePropertySwizzlesTest_CopyInputs(VectorPrimitivePropertySwizzlesTest self)
{
}

void Main(VectorPrimitivePropertySwizzlesTest self)
{
    vec2 vector0 = vec2(0.0);
    vec2 vXX = vector0.xx;
    vec2 vYY = vector0.yy;
    vector0 = vec2(vector0.yx.x, vector0.yx.y);
    vector0 = vec2(vector0.xy.x, vector0.xy.y);
}

void VectorPrimitivePropertySwizzlesTest_CopyOutputs(VectorPrimitivePropertySwizzlesTest self)
{
}

void main()
{
    VectorPrimitivePropertySwizzlesTest_InitGlobals();
    VectorPrimitivePropertySwizzlesTest self;
    VectorPrimitivePropertySwizzlesTest_DefaultConstructor(self);
    VectorPrimitivePropertySwizzlesTest_CopyInputs(self);
    Main(self);
    VectorPrimitivePropertySwizzlesTest_CopyOutputs(self);
}

