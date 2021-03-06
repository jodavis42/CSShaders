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

void Struct1_PreConstructor(inout Struct1 self)
{
    self.IntValue = 0;
}

void Struct1_DefaultConstructor(inout Struct1 self)
{
    Struct1_PreConstructor(self);
}

void Struct2_PreConstructor(inout Struct2 self)
{
    Struct1 tempStruct1;
    Struct1_DefaultConstructor(tempStruct1);
    self.Struct1Data = tempStruct1;
    self.FloatValue = 0.0;
}

void Struct2_DefaultConstructor(inout Struct2 self)
{
    Struct2_PreConstructor(self);
}

void NestedStructs_PreConstructor(inout NestedStructs self)
{
    self.Value = false;
    Struct2 tempStruct2;
    Struct2_DefaultConstructor(tempStruct2);
    self.Struct2Data = tempStruct2;
    Struct1 tempStruct1;
    Struct1_DefaultConstructor(tempStruct1);
    self.Struct1Data = tempStruct1;
    self.FloatValue = 0.0;
}

void NestedStructs_DefaultConstructor(inout NestedStructs self)
{
    NestedStructs_PreConstructor(self);
}

void main()
{
    NestedStructs self;
    NestedStructs_DefaultConstructor(self);
}

