#version 450

struct ForIf
{
    int empty_struct_member;
};

void ForIf_PreConstructor(ForIf self)
{
}

void ForIf_DefaultConstructor(ForIf self)
{
    ForIf_PreConstructor(self);
}

int ForIf0(ForIf self)
{
    int result = 0;
    for (int i = 0; i < 0; i--)
    {
        if (i > 0)
        {
            continue;
        }
        result++;
    }
    return result;
}

int ForIf1(ForIf self)
{
    int result = 0;
    for (int i = 0; i < 0; i--)
    {
        if (i > 0)
        {
            break;
        }
        result++;
    }
    return result;
}

int ForIf2(ForIf self)
{
    int result = 0;
    for (int i = 0; i < 0; i--)
    {
        if (i > 0)
        {
            continue;
        }
        else
        {
            if (i < 0)
            {
                break;
            }
            else
            {
                i = 0;
            }
        }
        result++;
    }
    return result;
}

void Main(ForIf self)
{
    int ret = 0;
    ret = ForIf0(self);
    ret = ForIf1(self);
    ret = ForIf2(self);
}

void main()
{
    ForIf self;
    ForIf_DefaultConstructor(self);
    Main(self);
}

