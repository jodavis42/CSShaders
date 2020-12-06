#version 450

struct SimpleStaticFunctionDeclaration2
{
    int empty_struct_member;
};

void SimpleStaticFunctionDeclaration2_InitGlobals()
{
}

void SimpleStaticFunctionDeclaration2_PreConstructor(SimpleStaticFunctionDeclaration2 self)
{
}

void SimpleStaticFunctionDeclaration2_DefaultConstructor(SimpleStaticFunctionDeclaration2 self)
{
    SimpleStaticFunctionDeclaration2_PreConstructor(self);
}

void SimpleStaticFunctionDeclaration2_CopyInputs(SimpleStaticFunctionDeclaration2 self)
{
}

int GetValue(int value)
{
    return value;
}

void Main(SimpleStaticFunctionDeclaration2 self)
{
    int value1 = GetValue(1);
    int value2 = GetValue(2);
}

void SimpleStaticFunctionDeclaration2_CopyOutputs(SimpleStaticFunctionDeclaration2 self)
{
}

void main()
{
    SimpleStaticFunctionDeclaration2_InitGlobals();
    SimpleStaticFunctionDeclaration2 self;
    SimpleStaticFunctionDeclaration2_DefaultConstructor(self);
    SimpleStaticFunctionDeclaration2_CopyInputs(self);
    Main(self);
    SimpleStaticFunctionDeclaration2_CopyOutputs(self);
}

