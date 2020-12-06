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

void StructFieldConstructorInitializers_InitGlobals()
{
}

void StructFieldConstructorInitializers_PreConstructor(inout StructFieldConstructorInitializers self)
{
    SubStruct tempSubStruct;
    tempSubStruct.IntValue = 3;
    tempSubStruct.FloatVal = 3.099999904632568359375;
    self.SubStruct = tempSubStruct;
}

void StructFieldConstructorInitializers_DefaultConstructor(inout StructFieldConstructorInitializers self)
{
    StructFieldConstructorInitializers_PreConstructor(self);
}

void StructFieldConstructorInitializers_CopyInputs(StructFieldConstructorInitializers self)
{
}

void Main(StructFieldConstructorInitializers self)
{
}

void StructFieldConstructorInitializers_CopyOutputs(StructFieldConstructorInitializers self)
{
}

void main()
{
    StructFieldConstructorInitializers_InitGlobals();
    StructFieldConstructorInitializers self;
    StructFieldConstructorInitializers_DefaultConstructor(self);
    StructFieldConstructorInitializers_CopyInputs(self);
    Main(self);
    StructFieldConstructorInitializers_CopyOutputs(self);
}

