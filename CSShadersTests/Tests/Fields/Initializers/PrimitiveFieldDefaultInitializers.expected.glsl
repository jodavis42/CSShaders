#version 450

struct PrimitiveFieldDefaultInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PrimitiveFieldDefaultInitializers_InitGlobals()
{
}

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

void PrimitiveFieldDefaultInitializers_CopyInputs(PrimitiveFieldDefaultInitializers self)
{
}

void Main(PrimitiveFieldDefaultInitializers self)
{
}

void PrimitiveFieldDefaultInitializers_CopyOutputs(PrimitiveFieldDefaultInitializers self)
{
}

void main()
{
    PrimitiveFieldDefaultInitializers_InitGlobals();
    PrimitiveFieldDefaultInitializers self;
    PrimitiveFieldDefaultInitializers_DefaultConstructor(self);
    PrimitiveFieldDefaultInitializers_CopyInputs(self);
    Main(self);
    PrimitiveFieldDefaultInitializers_CopyOutputs(self);
}

