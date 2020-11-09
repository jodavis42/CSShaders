#version 450

struct StructWithPrimitiveFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PreConstructor_StructWithPrimitiveFields(inout StructWithPrimitiveFields self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void DefaultConstructor_StructWithPrimitiveFields(inout StructWithPrimitiveFields self)
{
    PreConstructor_StructWithPrimitiveFields(self);
}

void main()
{
    StructWithPrimitiveFields self;
    DefaultConstructor_StructWithPrimitiveFields(self);
}

