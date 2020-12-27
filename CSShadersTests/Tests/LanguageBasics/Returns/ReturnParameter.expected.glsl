#version 450

struct ReturnParameter
{
    int empty_struct_member;
};

void ReturnParameter_InitGlobals()
{
}

void ReturnParameter_PreConstructor(ReturnParameter self)
{
}

void ReturnParameter_DefaultConstructor(ReturnParameter self)
{
    ReturnParameter_PreConstructor(self);
}

void ReturnParameter_CopyInputs(ReturnParameter self)
{
}

int ReturnParam(ReturnParameter self, int value)
{
    return value;
}

void Main(ReturnParameter self)
{
    int param = 1;
    int i = ReturnParam(self, param);
}

void ReturnParameter_CopyOutputs(ReturnParameter self)
{
}

void main()
{
    ReturnParameter_InitGlobals();
    ReturnParameter self;
    ReturnParameter_DefaultConstructor(self);
    ReturnParameter_CopyInputs(self);
    Main(self);
    ReturnParameter_CopyOutputs(self);
}

