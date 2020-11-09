#version 450

struct PrimitiveFieldDefaultInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PreConstructor_PrimitiveFieldDefaultInitializers(inout PrimitiveFieldDefaultInitializers self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void DefaultConstructor_PrimitiveFieldDefaultInitializers(inout PrimitiveFieldDefaultInitializers self)
{
    PreConstructor_PrimitiveFieldDefaultInitializers(self);
}

void Main(PrimitiveFieldDefaultInitializers self)
{
}

void main()
{
    PrimitiveFieldDefaultInitializers self;
    DefaultConstructor_PrimitiveFieldDefaultInitializers(self);
    Main(self);
}

