#version 450

struct ReturnPrimitive
{
    int empty_struct_member;
};

void ReturnPrimitive_PreConstructor(ReturnPrimitive self)
{
}

void ReturnPrimitive_DefaultConstructor(ReturnPrimitive self)
{
    ReturnPrimitive_PreConstructor(self);
}

int IntReturn(ReturnPrimitive self)
{
    return 1;
}

void Main(ReturnPrimitive self)
{
    int i = IntReturn(self);
}

void main()
{
    ReturnPrimitive self;
    ReturnPrimitive_DefaultConstructor(self);
    Main(self);
}

