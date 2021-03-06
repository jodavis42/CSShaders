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

void Main(PrimitiveFieldExplicitComplexInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitComplexInitializers self;
    PrimitiveFieldExplicitComplexInitializers_DefaultConstructor(self);
    Main(self);
}

