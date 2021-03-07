#version 450

struct ForBasic2
{
    int empty_struct_member;
};

void ForBasic2_PreConstructor(ForBasic2 self)
{
}

void ForBasic2_DefaultConstructor(ForBasic2 self)
{
    ForBasic2_PreConstructor(self);
}

int For0(ForBasic2 self)
{
    int count = 10;
    for (int i = 0; i < count; i++)
    {
    }
    return 0;
}

int For1(ForBasic2 self)
{
    int result = 1;
    for (int count = 10, i = 0; i < count; i++, count--)
    {
        result *= (i + 1);
    }
    return result;
}

void Main(ForBasic2 self)
{
    int ret = 0;
    ret = For0(self);
    ret = For1(self);
}

void main()
{
    ForBasic2 self;
    ForBasic2_DefaultConstructor(self);
    Main(self);
}

