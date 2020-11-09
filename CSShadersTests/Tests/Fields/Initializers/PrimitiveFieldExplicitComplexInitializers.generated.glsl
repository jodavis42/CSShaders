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

void PreConstructor_PrimitiveFieldExplicitComplexInitializers(inout PrimitiveFieldExplicitComplexInitializers self)
{
    self.BoolValue = GetBool();
    self.IntValue = GetInt();
    self.UIntValue = GetUint();
    self.FloatValue = GetFloat();
}

void DefaultConstructor_PrimitiveFieldExplicitComplexInitializers(inout PrimitiveFieldExplicitComplexInitializers self)
{
    PreConstructor_PrimitiveFieldExplicitComplexInitializers(self);
}

void Main(PrimitiveFieldExplicitComplexInitializers self)
{
}

void main()
{
    PrimitiveFieldExplicitComplexInitializers self;
    DefaultConstructor_PrimitiveFieldExplicitComplexInitializers(self);
    Main(self);
}

