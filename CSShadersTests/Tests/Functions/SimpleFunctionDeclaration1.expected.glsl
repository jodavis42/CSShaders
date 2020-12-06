#version 450

struct SimpleFunctionDeclaration1
{
    int empty_struct_member;
};

void SimpleFunctionDeclaration1_InitGlobals()
{
}

void SimpleFunctionDeclaration1_PreConstructor(SimpleFunctionDeclaration1 self)
{
}

void SimpleFunctionDeclaration1_DefaultConstructor(SimpleFunctionDeclaration1 self)
{
    SimpleFunctionDeclaration1_PreConstructor(self);
}

void SimpleFunctionDeclaration1_CopyInputs(SimpleFunctionDeclaration1 self)
{
}

void Main(SimpleFunctionDeclaration1 self)
{
}

void SimpleFunctionDeclaration1_CopyOutputs(SimpleFunctionDeclaration1 self)
{
}

void main()
{
    SimpleFunctionDeclaration1_InitGlobals();
    SimpleFunctionDeclaration1 self;
    SimpleFunctionDeclaration1_DefaultConstructor(self);
    SimpleFunctionDeclaration1_CopyInputs(self);
    Main(self);
    SimpleFunctionDeclaration1_CopyOutputs(self);
}

