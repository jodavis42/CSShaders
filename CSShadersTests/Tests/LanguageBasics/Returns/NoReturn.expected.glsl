#version 450

struct NoReturn
{
    int empty_struct_member;
};

void NoReturn_InitGlobals()
{
}

void NoReturn_PreConstructor(NoReturn self)
{
}

void NoReturn_DefaultConstructor(NoReturn self)
{
    NoReturn_PreConstructor(self);
}

void NoReturn_CopyInputs(NoReturn self)
{
}

void VoidReturn(NoReturn self)
{
}

void Main(NoReturn self)
{
    VoidReturn(self);
}

void NoReturn_CopyOutputs(NoReturn self)
{
}

void main()
{
    NoReturn_InitGlobals();
    NoReturn self;
    NoReturn_DefaultConstructor(self);
    NoReturn_CopyInputs(self);
    Main(self);
    NoReturn_CopyOutputs(self);
}

