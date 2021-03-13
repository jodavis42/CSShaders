#version 450

struct IfElseChainBasic
{
    int empty_struct_member;
};

void IfElseChainBasic_PreConstructor(IfElseChainBasic self)
{
}

void IfElseChainBasic_DefaultConstructor(IfElseChainBasic self)
{
    IfElseChainBasic_PreConstructor(self);
}

int IfElseChain0(IfElseChainBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    else
    {
        if (false)
        {
            a = 2;
        }
    }
    a++;
    return a;
}

int IfElseChain1(IfElseChainBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    else
    {
        if (false)
        {
            a = 2;
        }
        else
        {
            a = 3;
        }
    }
    a++;
    return a;
}

int IfElseChain2(IfElseChainBasic self)
{
    int a = 0;
    if (true)
    {
        a = 1;
    }
    else
    {
        if (false)
        {
            a = 2;
        }
        else
        {
            if (a < 3)
            {
                a = 3;
            }
            else
            {
                a = 4;
            }
        }
    }
    a++;
    return a;
}

void Main(IfElseChainBasic self)
{
    int ret = 0;
    ret = IfElseChain0(self);
    ret = IfElseChain1(self);
    ret = IfElseChain2(self);
}

void main()
{
    IfElseChainBasic self;
    IfElseChainBasic_DefaultConstructor(self);
    Main(self);
}

