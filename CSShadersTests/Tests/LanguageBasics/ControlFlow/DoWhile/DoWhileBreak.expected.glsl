#version 450

struct DoWhileBreak
{
    int empty_struct_member;
};

void DoWhileBreak_PreConstructor(DoWhileBreak self)
{
}

void DoWhileBreak_DefaultConstructor(DoWhileBreak self)
{
    DoWhileBreak_PreConstructor(self);
}

int DoWhileBreak0(DoWhileBreak self)
{
    int a = 1;
    do
    {
        break;
    } while (a < 0);
    return a;
}

void Main(DoWhileBreak self)
{
    int ret = DoWhileBreak0(self);
}

void main()
{
    DoWhileBreak self;
    DoWhileBreak_DefaultConstructor(self);
    Main(self);
}

