#version 450

struct SubStruct
{
    int Value;
};

struct StructFieldDefaultInitializers
{
    SubStruct SubStruct;
};

void StructFieldDefaultInitializers_InitGlobals()
{
}

void SubStruct_PreConstructor(inout SubStruct self)
{
    self.Value = 0;
}

void SubStruct_DefaultConstructor(inout SubStruct self)
{
    SubStruct_PreConstructor(self);
}

void StructFieldDefaultInitializers_PreConstructor(inout StructFieldDefaultInitializers self)
{
    SubStruct tempSubStruct;
    SubStruct_DefaultConstructor(tempSubStruct);
    self.SubStruct = tempSubStruct;
}

void StructFieldDefaultInitializers_DefaultConstructor(inout StructFieldDefaultInitializers self)
{
    StructFieldDefaultInitializers_PreConstructor(self);
}

void StructFieldDefaultInitializers_CopyInputs(StructFieldDefaultInitializers self)
{
}

void Main(StructFieldDefaultInitializers self)
{
}

void StructFieldDefaultInitializers_CopyOutputs(StructFieldDefaultInitializers self)
{
}

void main()
{
    StructFieldDefaultInitializers_InitGlobals();
    StructFieldDefaultInitializers self;
    StructFieldDefaultInitializers_DefaultConstructor(self);
    StructFieldDefaultInitializers_CopyInputs(self);
    Main(self);
    StructFieldDefaultInitializers_CopyOutputs(self);
}

