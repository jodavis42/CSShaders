#version 450

struct SimpleFunctionDeclaration1
{
    int empty_struct_member;
};

void SimpleFunctionDeclaration1_PreConstructor(SimpleFunctionDeclaration1 self)
{
}

void SimpleFunctionDeclaration1_DefaultConstructor(SimpleFunctionDeclaration1 self)
{
    SimpleFunctionDeclaration1_PreConstructor(self);
}

void Main(SimpleFunctionDeclaration1 self)
{
}

void main()
{
    SimpleFunctionDeclaration1 self;
    SimpleFunctionDeclaration1_DefaultConstructor(self);
    Main(self);
}

