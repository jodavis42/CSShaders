#version 450

struct SubStruct
{
    int Value;
};

struct StructFieldDefaultInitializers
{
    SubStruct SubStruct;
};

void PreConstructor_SubStruct(inout SubStruct self)
{
    self.Value = 0;
}

void DefaultConstructor_SubStruct(inout SubStruct self)
{
    PreConstructor_SubStruct(self);
}

void PreConstructor_StructFieldDefaultInitializers(inout StructFieldDefaultInitializers self)
{
    SubStruct tempSubStruct;
    DefaultConstructor_SubStruct(tempSubStruct);
    self.SubStruct = tempSubStruct;
}

void DefaultConstructor_StructFieldDefaultInitializers(inout StructFieldDefaultInitializers self)
{
    PreConstructor_StructFieldDefaultInitializers(self);
}

void Main(StructFieldDefaultInitializers self)
{
}

void main()
{
    StructFieldDefaultInitializers self;
    DefaultConstructor_StructFieldDefaultInitializers(self);
    Main(self);
}

