#version 450

struct WhileContinue
{
    int empty_struct_member;
};

void WhileContinue_PreConstructor(WhileContinue self)
{
}

void WhileContinue_DefaultConstructor(WhileContinue self)
{
    WhileContinue_PreConstructor(self);
}

int WhileContinue0(WhileContinue self)
{
    int a = 1;
    while (a < 0)
    {
    }
    return a;
}

void Main(WhileContinue self)
{
    int ret = WhileContinue0(self);
}

void main()
{
    WhileContinue self;
    WhileContinue_DefaultConstructor(self);
    Main(self);
}

