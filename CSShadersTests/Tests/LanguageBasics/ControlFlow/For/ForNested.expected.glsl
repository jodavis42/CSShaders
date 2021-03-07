#version 450

struct ForNested
{
    int empty_struct_member;
};

void ForNested_PreConstructor(ForNested self)
{
}

void ForNested_DefaultConstructor(ForNested self)
{
    ForNested_PreConstructor(self);
}

int ForNested0(ForNested self)
{
    int result = 1;
    for (int i = 1; i > 0; i--)
    {
        for (int j = 1; j > 0; j--)
        {
            result += 2;
        }
        result++;
    }
    return result;
}

int ForNested1(ForNested self)
{
    int result = 1;
    for (int i = 1; i > 0; i--)
    {
        for (int j = 1; j > 0; j--)
        {
            result += 2;
        }
        result++;
    }
    return result;
}

int ForNested2(ForNested self)
{
    int result = 1;
    for (int i = 1; i > 0; i--)
    {
        for (int j = 1; j > 0; j--)
        {
            result += 2;
            break;
        }
        result++;
    }
    return result;
}

int ForNested3(ForNested self)
{
    int result = 1;
    for (int i = 1; i > 0; i--)
    {
        for (int j = 1; j > 0; j--)
        {
            result += 2;
            break;
        }
        result++;
        break;
    }
    return result;
}

void Main(ForNested self)
{
    int ret = 0;
    ret = ForNested0(self);
    ret = ForNested1(self);
    ret = ForNested2(self);
    ret = ForNested3(self);
}

void main()
{
    ForNested self;
    ForNested_DefaultConstructor(self);
    Main(self);
}

