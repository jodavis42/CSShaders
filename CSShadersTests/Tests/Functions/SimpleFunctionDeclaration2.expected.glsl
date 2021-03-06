#version 450

struct SimpleFunctionDeclaration2
{
    int empty_struct_member;
};

void SimpleFunctionDeclaration2_PreConstructor(SimpleFunctionDeclaration2 self)
{
}

void SimpleFunctionDeclaration2_DefaultConstructor(SimpleFunctionDeclaration2 self)
{
    SimpleFunctionDeclaration2_PreConstructor(self);
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
    SimpleFunctionDeclaration2_DefaultConstructor(self);
    Main(self);
}

