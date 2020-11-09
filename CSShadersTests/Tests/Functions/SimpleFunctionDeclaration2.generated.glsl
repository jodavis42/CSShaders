#version 450

struct SimpleFunctionDeclaration2
{
    int empty_struct_member;
};

void PreConstructor_SimpleFunctionDeclaration2(SimpleFunctionDeclaration2 self)
{
}

void DefaultConstructor_SimpleFunctionDeclaration2(SimpleFunctionDeclaration2 self)
{
    PreConstructor_SimpleFunctionDeclaration2(self);
}

int GetValue(SimpleFunctionDeclaration2 self, int value)
{
    return value;
}

void Main(SimpleFunctionDeclaration2 self)
{
    int value = GetValue(self, 1);
}

void main()
{
    SimpleFunctionDeclaration2 self;
    DefaultConstructor_SimpleFunctionDeclaration2(self);
    Main(self);
}

