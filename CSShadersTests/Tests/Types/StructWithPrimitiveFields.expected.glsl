#version 450

struct StructWithPrimitiveFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

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

void main()
{
    StructWithPrimitiveFields self;
    StructWithPrimitiveFields_DefaultConstructor(self);
}

