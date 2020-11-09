#version 450

struct PrimitiveFieldExplicitSimpleInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PreConstructor_PrimitiveFieldExplicitSimpleInitializers(inout PrimitiveFieldExplicitSimpleInitializers self)
{
    self.BoolValue = true;
    self.IntValue = 1;
    self.UIntValue = 2u;
    self.FloatValue = 3.0;
}

void DefaultConstructor_PrimitiveFieldExplicitSimpleInitializers(inout PrimitiveFieldExplicitSimpleInitializers self)
{
    PreConstructor_PrimitiveFieldExplicitSimpleInitializers(self);
}

void Main(PrimitiveFieldExplicitSimpleInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitSimpleInitializers self;
    DefaultConstructor_PrimitiveFieldExplicitSimpleInitializers(self);
    Main(self);
}

