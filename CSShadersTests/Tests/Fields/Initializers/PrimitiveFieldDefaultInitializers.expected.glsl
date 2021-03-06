#version 450

struct PrimitiveFieldDefaultInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PrimitiveFieldDefaultInitializers_PreConstructor(inout PrimitiveFieldDefaultInitializers self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void PrimitiveFieldDefaultInitializers_DefaultConstructor(inout PrimitiveFieldDefaultInitializers self)
{
    PrimitiveFieldDefaultInitializers_PreConstructor(self);
}

void Main(PrimitiveFieldDefaultInitializers self)
{
}

void main()
{
    PrimitiveFieldDefaultInitializers self;
    PrimitiveFieldDefaultInitializers_DefaultConstructor(self);
    Main(self);
}

