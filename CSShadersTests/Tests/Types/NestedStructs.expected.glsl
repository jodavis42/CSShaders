#version 450

struct Struct1
{
    int IntValue;
};

struct Struct2
{
    Struct1 Struct1Data;
    float FloatValue;
};

struct NestedStructs
{
    bool Value;
    Struct2 Struct2Data;
    Struct1 Struct1Data;
    float FloatValue;
};

void PreConstructor_Struct1(inout Struct1 self)
{
    self.IntValue = 0;
}

void DefaultConstructor_Struct1(inout Struct1 self)
{
    PreConstructor_Struct1(self);
}

void PreConstructor_Struct2(inout Struct2 self)
{
    Struct1 tempStruct1;
    DefaultConstructor_Struct1(tempStruct1);
    self.Struct1Data = tempStruct1;
    self.FloatValue = 0.0;
}

void DefaultConstructor_Struct2(inout Struct2 self)
{
    PreConstructor_Struct2(self);
}

void PreConstructor_NestedStructs(inout NestedStructs self)
{
    self.Value = false;
    Struct2 tempStruct2;
    DefaultConstructor_Struct2(tempStruct2);
    self.Struct2Data = tempStruct2;
    Struct1 tempStruct1;
    DefaultConstructor_Struct1(tempStruct1);
    self.Struct1Data = tempStruct1;
    self.FloatValue = 0.0;
}

void DefaultConstructor_NestedStructs(inout NestedStructs self)
{
    PreConstructor_NestedStructs(self);
}

void main()
{
    NestedStructs self;
    DefaultConstructor_NestedStructs(self);
}

