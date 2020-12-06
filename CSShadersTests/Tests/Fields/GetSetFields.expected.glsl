#version 450

struct GetSetFields
{
    bool BoolValue;
    int IntValue;
    uint UIntValue;
    float FloatValue;
};

void GetSetFields_InitGlobals()
{
}

void GetSetFields_PreConstructor(inout GetSetFields self)
{
    self.BoolValue = false;
    self.IntValue = 0;
    self.UIntValue = 0u;
    self.FloatValue = 0.0;
}

void GetSetFields_DefaultConstructor(inout GetSetFields self)
{
    GetSetFields_PreConstructor(self);
}

void GetSetFields_CopyInputs(GetSetFields self)
{
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

void GetSetFields_CopyOutputs(GetSetFields self)
{
}

void main()
{
    GetSetFields_InitGlobals();
    GetSetFields self;
    GetSetFields_DefaultConstructor(self);
    GetSetFields_CopyInputs(self);
    Main(self);
    GetSetFields_CopyOutputs(self);
}

