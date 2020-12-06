#version 450

struct GetSetStaticFields
{
    int empty_struct_member;
};

bool BoolValue;
int IntValue;
uint UIntValue;
float FloatValue;

void GetSetStaticFields_InitGlobals()
{
    BoolValue = false;
    IntValue = 0;
    UIntValue = 0u;
    FloatValue = 0.0;
}

void GetSetStaticFields_PreConstructor(GetSetStaticFields self)
{
}

void GetSetStaticFields_DefaultConstructor(GetSetStaticFields self)
{
    GetSetStaticFields_PreConstructor(self);
}

void GetSetStaticFields_CopyInputs(GetSetStaticFields self)
{
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

void GetSetStaticFields_CopyOutputs(GetSetStaticFields self)
{
}

void main()
{
    GetSetStaticFields_InitGlobals();
    GetSetStaticFields self;
    GetSetStaticFields_DefaultConstructor(self);
    GetSetStaticFields_CopyInputs(self);
    Main(self);
    GetSetStaticFields_CopyOutputs(self);
}

