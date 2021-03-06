#version 450

struct PrimitiveFieldExplicitSimpleInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PrimitiveFieldExplicitSimpleInitializers_PreConstructor(inout PrimitiveFieldExplicitSimpleInitializers self)
{
    self.BoolValue = true;
    self.IntValue = 1;
    self.UIntValue = 2u;
    self.FloatValue = 3.0;
}

void PrimitiveFieldExplicitSimpleInitializers_DefaultConstructor(inout PrimitiveFieldExplicitSimpleInitializers self)
{
    PrimitiveFieldExplicitSimpleInitializers_PreConstructor(self);
}

void Main(PrimitiveFieldExplicitSimpleInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitSimpleInitializers self;
    PrimitiveFieldExplicitSimpleInitializers_DefaultConstructor(self);
    Main(self);
}

