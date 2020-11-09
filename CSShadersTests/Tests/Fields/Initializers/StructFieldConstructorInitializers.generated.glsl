#version 450

struct SubStruct
{
    int Value;
};

struct StructFieldConstructorInitializers
{
    SubStruct SubStruct;
};

void SubStructConstructor(inout SubStruct self, int value)
{
    self.Value = value;
}

void PreConstructor_StructFieldConstructorInitializers(inout StructFieldConstructorInitializers self)
{
    SubStruct tempSubStruct;
    SubStructConstructor(tempSubStruct, 1);
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

