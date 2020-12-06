#version 450

struct PrimitiveFieldExplicitSimpleInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PrimitiveFieldExplicitSimpleInitializers_InitGlobals()
{
}

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

void PrimitiveFieldExplicitSimpleInitializers_CopyInputs(PrimitiveFieldExplicitSimpleInitializers self)
{
}

void Main(PrimitiveFieldExplicitSimpleInitializers self)
{
}

void PrimitiveFieldExplicitSimpleInitializers_CopyOutputs(PrimitiveFieldExplicitSimpleInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitSimpleInitializers_InitGlobals();
    PrimitiveFieldExplicitSimpleInitializers self;
    PrimitiveFieldExplicitSimpleInitializers_DefaultConstructor(self);
    PrimitiveFieldExplicitSimpleInitializers_CopyInputs(self);
    Main(self);
    PrimitiveFieldExplicitSimpleInitializers_CopyOutputs(self);
}

