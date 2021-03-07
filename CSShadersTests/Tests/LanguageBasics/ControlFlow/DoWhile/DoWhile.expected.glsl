#version 450

struct DoWhile
{
    int empty_struct_member;
};

void DoWhile_PreConstructor(DoWhile self)
{
}

void DoWhile_DefaultConstructor(DoWhile self)
{
    DoWhile_PreConstructor(self);
}

int DoWhile0(DoWhile self)
{
    int a = 1;
    do
    {
        a--;
    } while (a < 0);
    return a;
}

void Main(DoWhile self)
{
    int ret = DoWhile0(self);
}

void main()
{
    DoWhile self;
    DoWhile_DefaultConstructor(self);
    Main(self);
}

