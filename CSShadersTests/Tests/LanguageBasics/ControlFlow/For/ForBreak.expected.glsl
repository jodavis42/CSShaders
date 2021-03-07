#version 450

struct ForBreak
{
    int empty_struct_member;
};

void ForBreak_PreConstructor(ForBreak self)
{
}

void ForBreak_DefaultConstructor(ForBreak self)
{
    ForBreak_PreConstructor(self);
}

int ForBreak0(ForBreak self, int count)
{
    int result = 0;
    for (int i = 1; i < count; i++)
    {
        result++;
        break;
    }
    return result;
}

int ForBreak1(ForBreak self, int count)
{
    int result = 0;
    for (int i = 1; !(i < count); i++)
    {
        break;
    }
    return result;
}

void Main(ForBreak self)
{
    int ret = 0;
    ret = ForBreak0(self, 10);
    ret = ForBreak1(self, 10);
}

void main()
{
    ForBreak self;
    ForBreak_DefaultConstructor(self);
    Main(self);
}

