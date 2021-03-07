#version 450

struct WhileNested
{
    int empty_struct_member;
};

void WhileNested_PreConstructor(WhileNested self)
{
}

void WhileNested_DefaultConstructor(WhileNested self)
{
    WhileNested_PreConstructor(self);
}

int WhileNested0(WhileNested self)
{
    int a = 1;
    while (a > 0)
    {
        int b = 1;
        while (b > 0)
        {
            b--;
        }
        a--;
    }
    return a;
}

int WhileNested1(WhileNested self)
{
    int a = 1;
    while (a > 0)
    {
        int b = 1;
        while (b > 0)
        {
            b--;
        }
        a--;
    }
    return a;
}

int WhileNested2(WhileNested self)
{
    int a = 1;
    while (a > 0)
    {
        int b = 1;
        while (b > 0)
        {
            b--;
            break;
        }
        a--;
    }
    return a;
}

int WhileNested3(WhileNested self)
{
    int a = 1;
    while (a > 0)
    {
        int b = 1;
        while (b > 0)
        {
            b--;
            break;
        }
        break;
    }
    return a;
}

void Main(WhileNested self)
{
    int ret = 0;
    ret = WhileNested0(self);
    ret = WhileNested1(self);
    ret = WhileNested2(self);
    ret = WhileNested3(self);
}

void main()
{
    WhileNested self;
    WhileNested_DefaultConstructor(self);
    Main(self);
}

