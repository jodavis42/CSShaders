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

void SubStruct_PreConstructor(inout SubStruct self)
{
    self.IntValue = 0;
    self.FloatVal = 1.0;
}

void SubStruct_DefaultConstructor(inout SubStruct self)
{
    SubStruct_PreConstructor(self);
}

void StructFieldConstructorInitializers_PreConstructor(inout StructFieldConstructorInitializers self)
{
    SubStruct tempSubStruct;
    SubStruct_DefaultConstructor(tempSubStruct);
    tempSubStruct.IntValue = 3;
    tempSubStruct.FloatVal = 3.099999904632568359375;
    self.SubStruct = tempSubStruct;
}

void StructFieldConstructorInitializers_DefaultConstructor(inout StructFieldConstructorInitializers self)
{
    StructFieldConstructorInitializers_PreConstructor(self);
}

void Main(StructFieldConstructorInitializers self)
{
}

void main()
{
    StructFieldConstructorInitializers self;
    StructFieldConstructorInitializers_DefaultConstructor(self);
    Main(self);
}

