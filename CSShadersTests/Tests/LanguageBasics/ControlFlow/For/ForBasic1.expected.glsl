#version 450

struct ForBasic1
{
    int empty_struct_member;
};

void ForBasic1_PreConstructor(ForBasic1 self)
{
}

void ForBasic1_DefaultConstructor(ForBasic1 self)
{
    ForBasic1_PreConstructor(self);
}

int For0(ForBasic1 self)
{
    int count = 10;
    int result = 1;
    for (int i = 0; i < count; i++)
    {
        result += i;
    }
    return result;
}

int For1(ForBasic1 self)
{
    int count = 10;
    int result = 1;
    int i = 1;
    for (;;)
    {
        result += i;
        i++;
        continue;
    }
    return result;
}

int For2(ForBasic1 self)
{
    int count = 10;
    int result = 1;
    int i = 0;
    i = 1;
    while (i < count)
    {
        result += i;
        i++;
    }
    return result;
}

int For3(ForBasic1 self)
{
    int count = 10;
    int result = 1;
    int i = 0;
    for (;;)
    {
        result += i;
        i++;
        continue;
    }
    return result;
}

void Main(ForBasic1 self)
{
    int ret = 0;
    ret = For0(self);
    ret = For1(self);
    ret = For2(self);
    ret = For3(self);
}

void main()
{
    ForBasic1 self;
    ForBasic1_DefaultConstructor(self);
    Main(self);
}

