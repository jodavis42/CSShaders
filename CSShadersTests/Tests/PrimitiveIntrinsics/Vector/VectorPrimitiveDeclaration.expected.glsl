#version 450

struct VectorPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void VectorPrimitiveDeclarationTest_InitGlobals()
{
}

void VectorPrimitiveDeclarationTest_PreConstructor(VectorPrimitiveDeclarationTest self)
{
}

void VectorPrimitiveDeclarationTest_DefaultConstructor(VectorPrimitiveDeclarationTest self)
{
    VectorPrimitiveDeclarationTest_PreConstructor(self);
}

void VectorPrimitiveDeclarationTest_CopyInputs(VectorPrimitiveDeclarationTest self)
{
}

void Main(VectorPrimitiveDeclarationTest self)
{
    vec2 vector0 = vec2(0.0);
}

void VectorPrimitiveDeclarationTest_CopyOutputs(VectorPrimitiveDeclarationTest self)
{
}

void main()
{
    VectorPrimitiveDeclarationTest_InitGlobals();
    VectorPrimitiveDeclarationTest self;
    VectorPrimitiveDeclarationTest_DefaultConstructor(self);
    VectorPrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    VectorPrimitiveDeclarationTest_CopyOutputs(self);
}

