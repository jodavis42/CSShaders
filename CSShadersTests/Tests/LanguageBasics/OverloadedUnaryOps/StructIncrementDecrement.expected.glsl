#version 450

struct TestV2
{
    float X;
    float Y;
};

struct StructIncrementDecrement
{
    int empty_struct_member;
};

void StructIncrementDecrement_InitGlobals()
{
}

void StructIncrementDecrement_PreConstructor(StructIncrementDecrement self)
{
}

void StructIncrementDecrement_DefaultConstructor(StructIncrementDecrement self)
{
    StructIncrementDecrement_PreConstructor(self);
}

void StructIncrementDecrement_CopyInputs(StructIncrementDecrement self)
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

TestV2 op_Increment(TestV2 value)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 result = tempTestV2;
    result.X = value.X + 1.0;
    result.Y = value.Y + 1.0;
    return result;
}

TestV2 op_Decrement(TestV2 value)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 result = tempTestV2;
    result.X = value.X - 1.0;
    result.Y = value.Y - 1.0;
    return result;
}

void Main(StructIncrementDecrement self)
{
    TestV2 tempTestV2;
    TestV2_DefaultConstructor(tempTestV2);
    TestV2 v2 = tempTestV2;
    TestV2 tempTestV2_1;
    TestV2_DefaultConstructor(tempTestV2_1);
    TestV2 r = tempTestV2_1;
    v2 = op_Increment(v2);
    v2 = op_Increment(v2);
    r = v2;
    v2 = op_Increment(v2);
    TestV2 _112 = v2;
    v2 = op_Increment(_112);
    r = _112;
    v2 = op_Decrement(v2);
    v2 = op_Decrement(v2);
    r = v2;
    v2 = op_Decrement(v2);
    TestV2 _127 = v2;
    v2 = op_Decrement(_127);
    r = _127;
}

void StructIncrementDecrement_CopyOutputs(StructIncrementDecrement self)
{
}

void main()
{
    StructIncrementDecrement_InitGlobals();
    StructIncrementDecrement self;
    StructIncrementDecrement_DefaultConstructor(self);
    StructIncrementDecrement_CopyInputs(self);
    Main(self);
    StructIncrementDecrement_CopyOutputs(self);
}

