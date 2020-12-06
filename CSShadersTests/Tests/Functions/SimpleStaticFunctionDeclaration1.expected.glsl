#version 450

struct SimpleStaticFunctionDeclaration1
{
    int empty_struct_member;
};

void SimpleStaticFunctionDeclaration1_InitGlobals()
{
}

void SimpleStaticFunctionDeclaration1_PreConstructor(SimpleStaticFunctionDeclaration1 self)
{
}

void SimpleStaticFunctionDeclaration1_DefaultConstructor(SimpleStaticFunctionDeclaration1 self)
{
    SimpleStaticFunctionDeclaration1_PreConstructor(self);
}

void SimpleStaticFunctionDeclaration1_CopyInputs(SimpleStaticFunctionDeclaration1 self)
{
}

void Main(SimpleStaticFunctionDeclaration1 self)
{
}

void SimpleStaticFunctionDeclaration1_CopyOutputs(SimpleStaticFunctionDeclaration1 self)
{
}

void main()
{
    SimpleStaticFunctionDeclaration1_InitGlobals();
    SimpleStaticFunctionDeclaration1 self;
    SimpleStaticFunctionDeclaration1_DefaultConstructor(self);
    SimpleStaticFunctionDeclaration1_CopyInputs(self);
    Main(self);
    SimpleStaticFunctionDeclaration1_CopyOutputs(self);
}

