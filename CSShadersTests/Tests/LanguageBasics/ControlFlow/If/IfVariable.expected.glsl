#version 450

struct IfVariable
{
    int empty_struct_member;
};

void IfVariable_PreConstructor(IfVariable self)
{
}

void IfVariable_DefaultConstructor(IfVariable self)
{
    IfVariable_PreConstructor(self);
}

int IfVariable0(IfVariable self)
{
    bool condition = true;
    int value = 1;
    if (condition)
    {
        value = 1;
    }
    else
    {
        if (!condition)
        {
            value = 2;
        }
    }
    return value;
}

void Main(IfVariable self)
{
    int ret = 0;
    ret = IfVariable0(self);
}

void main()
{
    IfVariable self;
    IfVariable_DefaultConstructor(self);
    Main(self);
}

