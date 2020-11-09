#version 450

struct SubStruct
{
    int IntValue;
    float FloatVal;
};

struct StructFieldConstructorInitializers
{
    SubStruct SubStruct;
};

void PreConstructor_StructFieldConstructorInitializers(inout StructFieldConstructorInitializers self)
{
    SubStruct tempSubStruct;
    tempSubStruct.IntValue = 3;
    tempSubStruct.FloatVal = 3.099999904632568359375;
    self.SubStruct = tempSubStruct;
}

void DefaultConstructor_StructFieldConstructorInitializers(inout StructFieldConstructorInitializers self)
{
    PreConstructor_StructFieldConstructorInitializers(self);
}

void Main(StructFieldConstructorInitializers self)
{
}

void main()
{
    StructFieldConstructorInitializers self;
    DefaultConstructor_StructFieldConstructorInitializers(self);
    Main(self);
}

