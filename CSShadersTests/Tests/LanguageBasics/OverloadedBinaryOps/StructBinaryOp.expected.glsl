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

void StructUnaryOp_PreConstructor(StructUnaryOp self)
{
}

void StructUnaryOp_DefaultConstructor(StructUnaryOp self)
{
    StructUnaryOp_PreConstructor(self);
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

TestV2 op_Addition(TestV2 lhs, TestV2 rhs)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 result = tempTestV2;
    result.X = lhs.X + rhs.X;
    result.Y = lhs.Y + rhs.Y;
    return result;
}

void Main(StructUnaryOp self)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 value = tempTestV2;
    TestV2 result = op_Addition(value, value);
}

void main()
{
    StructUnaryOp self;
    StructUnaryOp_DefaultConstructor(self);
    Main(self);
}

