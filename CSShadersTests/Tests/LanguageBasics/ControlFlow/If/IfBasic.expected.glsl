#version 450

struct IfBasic
{
    int empty_struct_member;
};

void IfBasic_PreConstructor(IfBasic self)
{
}

void IfBasic_DefaultConstructor(IfBasic self)
{
    IfBasic_PreConstructor(self);
}

int If0(IfBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    return a;
}

int If1(IfBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    a++;
    return a;
}

void Main(IfBasic self)
{
    int ret = 0;
    ret = If0(self);
    ret = If1(self);
}

void main()
{
    IfBasic self;
    IfBasic_DefaultConstructor(self);
    Main(self);
}

