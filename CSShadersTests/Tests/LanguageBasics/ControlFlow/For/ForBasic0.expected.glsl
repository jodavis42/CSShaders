#version 450

struct ForBasic0
{
    int empty_struct_member;
};

void ForBasic0_PreConstructor(ForBasic0 self)
{
}

void ForBasic0_DefaultConstructor(ForBasic0 self)
{
    ForBasic0_PreConstructor(self);
}

int For0(ForBasic0 self)
{
    for (int i = 0; i < 10; i++)
    {
    }
    return 0;
}

int For1(ForBasic0 self)
{
    int count = 10;
    int result = 1;
    for (int i = 0; i < count; i++)
    {
        result *= (i + 1);
    }
    return result;
}

int For2(ForBasic0 self)
{
    int count = 10;
    int result = 1;
    for (int i = 1; i < count; i++)
    {
        result += i;
    }
    return result;
}

void Main(ForBasic0 self)
{
    int ret = 0;
    ret = For0(self);
    ret = For1(self);
    ret = For2(self);
}

void main()
{
    ForBasic0 self;
    ForBasic0_DefaultConstructor(self);
    Main(self);
}

