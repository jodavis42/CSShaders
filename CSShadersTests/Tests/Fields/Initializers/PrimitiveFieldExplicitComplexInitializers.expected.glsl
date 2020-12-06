#version 450

struct InitialValues
{
    int empty_struct_member;
};

struct PrimitiveFieldExplicitComplexInitializers
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PrimitiveFieldExplicitComplexInitializers_InitGlobals()
{
}

bool GetBool()
{
    return false;
}

int GetInt()
{
    return 1;
}

uint GetUint()
{
    return 2u;
}

float GetFloat()
{
    return 3.0;
}

void PrimitiveFieldExplicitComplexInitializers_PreConstructor(inout PrimitiveFieldExplicitComplexInitializers self)
{
    self.BoolValue = GetBool();
    self.IntValue = GetInt();
    self.UIntValue = GetUint();
    self.FloatValue = GetFloat();
}

void PrimitiveFieldExplicitComplexInitializers_DefaultConstructor(inout PrimitiveFieldExplicitComplexInitializers self)
{
    PrimitiveFieldExplicitComplexInitializers_PreConstructor(self);
}

void PrimitiveFieldExplicitComplexInitializers_CopyInputs(PrimitiveFieldExplicitComplexInitializers self)
{
}

void Main(PrimitiveFieldExplicitComplexInitializers self)
{
}

void PrimitiveFieldExplicitComplexInitializers_CopyOutputs(PrimitiveFieldExplicitComplexInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitComplexInitializers_InitGlobals();
    PrimitiveFieldExplicitComplexInitializers self;
    PrimitiveFieldExplicitComplexInitializers_DefaultConstructor(self);
    PrimitiveFieldExplicitComplexInitializers_CopyInputs(self);
    Main(self);
    PrimitiveFieldExplicitComplexInitializers_CopyOutputs(self);
}

