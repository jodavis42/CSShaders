#version 450

struct StructWithPrimitiveFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void StructWithPrimitiveFields_InitGlobals()
{
}

void StructWithPrimitiveFields_PreConstructor(inout StructWithPrimitiveFields self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void StructWithPrimitiveFields_DefaultConstructor(inout StructWithPrimitiveFields self)
{
    StructWithPrimitiveFields_PreConstructor(self);
}

void StructWithPrimitiveFields_CopyInputs(StructWithPrimitiveFields self)
{
}

void StructWithPrimitiveFields_CopyOutputs(StructWithPrimitiveFields self)
{
}

void main()
{
    StructWithPrimitiveFields_InitGlobals();
    StructWithPrimitiveFields self;
    StructWithPrimitiveFields_DefaultConstructor(self);
    StructWithPrimitiveFields_CopyInputs(self);
    StructWithPrimitiveFields_CopyOutputs(self);
}

