#version 450

struct VectorPrimitiveFieldSwizzlesTest
{
    int empty_struct_member;
};

void VectorPrimitiveFieldSwizzlesTest_InitGlobals()
{
}

void VectorPrimitiveFieldSwizzlesTest_PreConstructor(VectorPrimitiveFieldSwizzlesTest self)
{
}

void VectorPrimitiveFieldSwizzlesTest_DefaultConstructor(VectorPrimitiveFieldSwizzlesTest self)
{
    VectorPrimitiveFieldSwizzlesTest_PreConstructor(self);
}

void VectorPrimitiveFieldSwizzlesTest_CopyInputs(VectorPrimitiveFieldSwizzlesTest self)
{
}

void Main(VectorPrimitiveFieldSwizzlesTest self)
{
    vec2 vector0 = vec2(0.0);
    vector0.y = vector0.x;
}

void VectorPrimitiveFieldSwizzlesTest_CopyOutputs(VectorPrimitiveFieldSwizzlesTest self)
{
}

void main()
{
    VectorPrimitiveFieldSwizzlesTest_InitGlobals();
    VectorPrimitiveFieldSwizzlesTest self;
    VectorPrimitiveFieldSwizzlesTest_DefaultConstructor(self);
    VectorPrimitiveFieldSwizzlesTest_CopyInputs(self);
    Main(self);
    VectorPrimitiveFieldSwizzlesTest_CopyOutputs(self);
}

