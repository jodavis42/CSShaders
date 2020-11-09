#version 450

struct GetSetStaticFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

bool BoolValue;
int IntValue;
uint UIntValue;
float FloatValue;

void PreConstructor_GetSetStaticFields(inout GetSetStaticFields self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void DefaultConstructor_GetSetStaticFields(inout GetSetStaticFields self)
{
    PreConstructor_GetSetStaticFields(self);
}

void Main(GetSetStaticFields self)
{
    BoolValue = false;
    bool boolValue = BoolValue;
    boolValue = BoolValue;
    IntValue = 0;
    int intValue = IntValue;
    intValue = IntValue;
    UIntValue = 0u;
    uint uintValue = UIntValue;
    uintValue = UIntValue;
    FloatValue = 0.0;
    float floatValue = FloatValue;
    floatValue = FloatValue;
}

void main()
{
    GetSetStaticFields self;
    DefaultConstructor_GetSetStaticFields(self);
    Main(self);
}

