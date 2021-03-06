#version 450

struct SubStruct
{
    int Value;
};

struct StructFieldConstructorInitializers
{
    SubStruct SubStruct;
};

void SubStruct_Constructor(inout SubStruct self, int value)
{
    self.Value = value;
}

void StructFieldConstructorInitializers_PreConstructor(inout StructFieldConstructorInitializers self)
{
    SubStruct tempSubStruct;
    SubStruct_Constructor(tempSubStruct, 1);
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

