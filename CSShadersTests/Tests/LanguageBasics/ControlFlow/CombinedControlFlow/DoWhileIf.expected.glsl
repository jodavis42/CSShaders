#version 450

struct DoWhileIf
{
    int empty_struct_member;
};

void DoWhileIf_PreConstructor(DoWhileIf self)
{
}

void DoWhileIf_DefaultConstructor(DoWhileIf self)
{
    DoWhileIf_PreConstructor(self);
}

int DoWhileIf0(DoWhileIf self)
{
    int a = 1;
    do
    {
        if (a > 0)
        {
            continue;
        }
        a--;
    } while (a < 0);
    return a;
}

int DoWhileIf1(DoWhileIf self)
{
    int a = 1;
    do
    {
        if (a > 0)
        {
            break;
        }
        a--;
    } while (a < 0);
    return a;
}

int DoWhileIf2(DoWhileIf self)
{
    int a = 1;
    do
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
    } while (a < 0);
    return a;
}

void Main(DoWhileIf self)
{
    int ret = 0;
    ret = DoWhileIf0(self);
    ret = DoWhileIf1(self);
    ret = DoWhileIf2(self);
}

void main()
{
    DoWhileIf self;
    DoWhileIf_DefaultConstructor(self);
    Main(self);
}

