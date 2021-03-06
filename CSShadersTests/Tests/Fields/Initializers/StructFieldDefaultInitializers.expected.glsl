#version 450

struct SubStruct
{
    int Value;
};

struct StructFieldDefaultInitializers
{
    SubStruct SubStruct;
};

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

void Main(StructFieldDefaultInitializers self)
{
}

void main()
{
    StructFieldDefaultInitializers self;
    StructFieldDefaultInitializers_DefaultConstructor(self);
    Main(self);
}

