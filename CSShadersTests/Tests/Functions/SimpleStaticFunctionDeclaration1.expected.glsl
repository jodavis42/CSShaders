#version 450

struct SimpleStaticFunctionDeclaration1
{
    int empty_struct_member;
};

void SimpleStaticFunctionDeclaration1_PreConstructor(SimpleStaticFunctionDeclaration1 self)
{
}

void SimpleStaticFunctionDeclaration1_DefaultConstructor(SimpleStaticFunctionDeclaration1 self)
{
    SimpleStaticFunctionDeclaration1_PreConstructor(self);
}

void Main(SimpleStaticFunctionDeclaration1 self)
{
}

void main()
{
    SimpleStaticFunctionDeclaration1 self;
    SimpleStaticFunctionDeclaration1_DefaultConstructor(self);
    Main(self);
}

