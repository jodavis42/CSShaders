#version 450

struct WhileTest
{
    int empty_struct_member;
};

void WhileTest_PreConstructor(WhileTest self)
{
}

void WhileTest_DefaultConstructor(WhileTest self)
{
    WhileTest_PreConstructor(self);
}

int While0(WhileTest self)
{
    int a = 1;
    while (a < 0)
    {
        a--;
    }
    return a;
}

void Main(WhileTest self)
{
    int ret = While0(self);
}

void main()
{
    WhileTest self;
    WhileTest_DefaultConstructor(self);
    Main(self);
}

