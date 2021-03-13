#version 450

struct IfElseBasic
{
    int empty_struct_member;
};

void IfElseBasic_PreConstructor(IfElseBasic self)
{
}

void IfElseBasic_DefaultConstructor(IfElseBasic self)
{
    IfElseBasic_PreConstructor(self);
}

int IfElse0(IfElseBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    else
    {
        a = 2;
    }
    a++;
    return a;
}

void Main(IfElseBasic self)
{
    int ret = 0;
    ret = IfElse0(self);
}

void main()
{
    IfElseBasic self;
    IfElseBasic_DefaultConstructor(self);
    Main(self);
}

