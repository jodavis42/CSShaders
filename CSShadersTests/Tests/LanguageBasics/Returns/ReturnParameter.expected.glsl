#version 450

struct ReturnParameter
{
    int empty_struct_member;
};

void ReturnParameter_PreConstructor(ReturnParameter self)
{
}

void ReturnParameter_DefaultConstructor(ReturnParameter self)
{
    ReturnParameter_PreConstructor(self);
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

void main()
{
    ReturnParameter self;
    ReturnParameter_DefaultConstructor(self);
    Main(self);
}

