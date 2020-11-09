#version 450

struct SimpleStaticFunctionDeclaration1
{
    int empty_struct_member;
};

void PreConstructor_SimpleStaticFunctionDeclaration1(SimpleStaticFunctionDeclaration1 self)
{
}

void DefaultConstructor_SimpleStaticFunctionDeclaration1(SimpleStaticFunctionDeclaration1 self)
{
    PreConstructor_SimpleStaticFunctionDeclaration1(self);
}

void Main(SimpleStaticFunctionDeclaration1 self)
{
}

void main()
{
    SimpleStaticFunctionDeclaration1 self;
    DefaultConstructor_SimpleStaticFunctionDeclaration1(self);
    Main(self);
}

