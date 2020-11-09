#version 450

struct SimpleStaticFunctionDeclaration2
{
    int empty_struct_member;
};

void PreConstructor_SimpleStaticFunctionDeclaration2(SimpleStaticFunctionDeclaration2 self)
{
}

void DefaultConstructor_SimpleStaticFunctionDeclaration2(SimpleStaticFunctionDeclaration2 self)
{
    PreConstructor_SimpleStaticFunctionDeclaration2(self);
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
    DefaultConstructor_SimpleStaticFunctionDeclaration2(self);
    Main(self);
}

