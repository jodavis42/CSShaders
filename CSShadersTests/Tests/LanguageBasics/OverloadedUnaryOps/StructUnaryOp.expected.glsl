#version 450

struct TestV2
{
    float X;
    float Y;
};

struct StructUnaryOp
{
    int empty_struct_member;
};

void StructUnaryOp_InitGlobals()
{
}

void StructUnaryOp_PreConstructor(StructUnaryOp self)
{
}

void StructUnaryOp_DefaultConstructor(StructUnaryOp self)
{
    StructUnaryOp_PreConstructor(self);
}

void StructUnaryOp_CopyInputs(StructUnaryOp self)
{
}

void TestV2_PreConstructor(inout TestV2 self)
{
    self.X = 0.0;
    self.Y = 0.0;
}

void TestV2_DefaultConstructor(inout TestV2 self)
{
    TestV2_PreConstructor(self);
}

TestV2 op_UnaryNegation(TestV2 value)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 result = tempTestV2;
    result.X = -value.X;
    result.Y = -value.Y;
    return result;
}

void Main(StructUnaryOp self)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 result = op_UnaryNegation(tempTestV2);
}

void StructUnaryOp_CopyOutputs(StructUnaryOp self)
{
}

void main()
{
    StructUnaryOp_InitGlobals();
    StructUnaryOp self;
    StructUnaryOp_DefaultConstructor(self);
    StructUnaryOp_CopyInputs(self);
    Main(self);
    StructUnaryOp_CopyOutputs(self);
}

