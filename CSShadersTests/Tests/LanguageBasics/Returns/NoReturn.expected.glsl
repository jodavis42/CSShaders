#version 450

struct NoReturn
{
    int empty_struct_member;
};

void NoReturn_PreConstructor(NoReturn self)
{
}

void NoReturn_DefaultConstructor(NoReturn self)
{
    NoReturn_PreConstructor(self);
}

void VoidReturn(NoReturn self)
{
}

void Main(NoReturn self)
{
    VoidReturn(self);
}

void main()
{
    NoReturn self;
    NoReturn_DefaultConstructor(self);
    Main(self);
}

