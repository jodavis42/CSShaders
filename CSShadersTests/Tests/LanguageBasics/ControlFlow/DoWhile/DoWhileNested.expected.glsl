#version 450

struct DoWhileNested
{
    int empty_struct_member;
};

void DoWhileNested_PreConstructor(DoWhileNested self)
{
}

void DoWhileNested_DefaultConstructor(DoWhileNested self)
{
    DoWhileNested_PreConstructor(self);
}

int DoWhileNested0(DoWhileNested self)
{
    int a = 1;
    do
    {
        int b = 1;
        do
        {
            b--;
        } while (b > 0);
        a--;
    } while (a > 0);
    return a;
}

int DoWhileNested1(DoWhileNested self)
{
    int a = 1;
    do
    {
        int b = 1;
        do
        {
        } while (b > 0);
        a--;
    } while (a > 0);
    return a;
}

int DoWhileNested2(DoWhileNested self)
{
    int a = 1;
    do
    {
        int b = 1;
        do
        {
            break;
        } while (b > 0);
        a--;
    } while (a > 0);
    return a;
}

int DoWhileNested3(DoWhileNested self)
{
    int a = 1;
    do
    {
        int b = 1;
        do
        {
            break;
        } while (b > 0);
        break;
    } while (a > 0);
    return a;
}

void Main(DoWhileNested self)
{
    int ret = 0;
    ret = DoWhileNested0(self);
    ret = DoWhileNested1(self);
    ret = DoWhileNested2(self);
    ret = DoWhileNested3(self);
}

void main()
{
    DoWhileNested self;
    DoWhileNested_DefaultConstructor(self);
    Main(self);
}

