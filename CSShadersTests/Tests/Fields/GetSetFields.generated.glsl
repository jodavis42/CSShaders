#version 450

struct GetSetFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void PreConstructor_GetSetFields(inout GetSetFields self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void DefaultConstructor_GetSetFields(inout GetSetFields self)
{
    PreConstructor_GetSetFields(self);
}

void Main(inout GetSetFields self)
{
    self.BoolValue = false;
    bool boolValue = self.BoolValue;
    boolValue = self.BoolValue;
    self.IntValue = 0;
    int intValue = self.IntValue;
    intValue = self.IntValue;
    self.UIntValue = 0u;
    uint uintValue = self.UIntValue;
    uintValue = self.UIntValue;
    self.FloatValue = 0.0;
    float floatValue = self.FloatValue;
    floatValue = self.FloatValue;
}

void main()
{
    GetSetFields self;
    DefaultConstructor_GetSetFields(self);
    Main(self);
}

