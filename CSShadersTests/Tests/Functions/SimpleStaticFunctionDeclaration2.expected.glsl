#version 450

struct SimpleStaticFunctionDeclaration2
{
    int empty_struct_member;
};

void SimpleStaticFunctionDeclaration2_PreConstructor(SimpleStaticFunctionDeclaration2 self)
{
}

void SimpleStaticFunctionDeclaration2_DefaultConstructor(SimpleStaticFunctionDeclaration2 self)
{
    SimpleStaticFunctionDeclaration2_PreConstructor(self);
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

void main()
{
    SimpleStaticFunctionDeclaration2 self;
    SimpleStaticFunctionDeclaration2_DefaultConstructor(self);
    Main(self);
}

