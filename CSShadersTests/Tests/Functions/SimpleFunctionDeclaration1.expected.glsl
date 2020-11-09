#version 450

struct SimpleFunctionDeclaration1
{
    int empty_struct_member;
};

void PreConstructor_SimpleFunctionDeclaration1(SimpleFunctionDeclaration1 self)
{
}

void DefaultConstructor_SimpleFunctionDeclaration1(SimpleFunctionDeclaration1 self)
{
    PreConstructor_SimpleFunctionDeclaration1(self);
}

void Main(SimpleFunctionDeclaration1 self)
{
}

void main()
{
    SimpleFunctionDeclaration1 self;
    DefaultConstructor_SimpleFunctionDeclaration1(self);
    Main(self);
}

