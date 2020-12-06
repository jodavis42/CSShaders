#version 450

struct SimpleFunctionDeclaration2
{
    int empty_struct_member;
};

void SimpleFunctionDeclaration2_InitGlobals()
{
}

void SimpleFunctionDeclaration2_PreConstructor(SimpleFunctionDeclaration2 self)
{
}

void SimpleFunctionDeclaration2_DefaultConstructor(SimpleFunctionDeclaration2 self)
{
    SimpleFunctionDeclaration2_PreConstructor(self);
}

void SimpleFunctionDeclaration2_CopyInputs(SimpleFunctionDeclaration2 self)
{
}

int GetValue(SimpleFunctionDeclaration2 self, int value)
{
    return value;
}

void Main(SimpleFunctionDeclaration2 self)
{
    int value = GetValue(self, 1);
}

void SimpleFunctionDeclaration2_CopyOutputs(SimpleFunctionDeclaration2 self)
{
}

void main()
{
    SimpleFunctionDeclaration2_InitGlobals();
    SimpleFunctionDeclaration2 self;
    SimpleFunctionDeclaration2_DefaultConstructor(self);
    SimpleFunctionDeclaration2_CopyInputs(self);
    Main(self);
    SimpleFunctionDeclaration2_CopyOutputs(self);
}

