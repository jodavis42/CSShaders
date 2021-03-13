#version 450

struct WhileIf
{
    int empty_struct_member;
};

void WhileIf_PreConstructor(WhileIf self)
{
}

void WhileIf_DefaultConstructor(WhileIf self)
{
    WhileIf_PreConstructor(self);
}

int WhileIf0(WhileIf self)
{
    int a = 1;
    while (a < 0)
    {
        if (a > 0)
        {
            continue;
        }
        a--;
    }
    return a;
}

int WhileIf1(WhileIf self)
{
    int a = 1;
    while (a < 0)
    {
        if (a > 0)
        {
            break;
        }
        a--;
    }
    return a;
}

int WhileIf2(WhileIf self)
{
    int a = 1;
    while (a < 0)
    {
        if (a > 0)
        {
            continue;
        }
        else
        {
            if (a < 0)
            {
                break;
            }
            else
            {
                a = 0;
            }
        }
        a--;
    }
    return a;
}

void Main(WhileIf self)
{
    int ret = 0;
    ret = WhileIf0(self);
    ret = WhileIf1(self);
    ret = WhileIf2(self);
}

void main()
{
    WhileIf self;
    WhileIf_DefaultConstructor(self);
    Main(self);
}

