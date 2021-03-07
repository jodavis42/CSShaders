#version 450

struct WhileBreak
{
    int empty_struct_member;
};

void WhileBreak_PreConstructor(WhileBreak self)
{
}

void WhileBreak_DefaultConstructor(WhileBreak self)
{
    WhileBreak_PreConstructor(self);
}

int WhileBreak0(WhileBreak self)
{
    int a = 1;
    while (!(a < 0))
    {
        break;
    }
    return a;
}

void Main(WhileBreak self)
{
    int ret = WhileBreak0(self);
}

void main()
{
    WhileBreak self;
    WhileBreak_DefaultConstructor(self);
    Main(self);
}

