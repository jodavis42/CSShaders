#version 450

struct DoWhileContinue
{
    int empty_struct_member;
};

void DoWhileContinue_PreConstructor(DoWhileContinue self)
{
}

void DoWhileContinue_DefaultConstructor(DoWhileContinue self)
{
    DoWhileContinue_PreConstructor(self);
}

int DoWhileContinue0(DoWhileContinue self)
{
    int a = 1;
    do
    {
    } while (a < 0);
    return a;
}

void Main(DoWhileContinue self)
{
    int ret = DoWhileContinue0(self);
}

void main()
{
    DoWhileContinue self;
    DoWhileContinue_DefaultConstructor(self);
    Main(self);
}

