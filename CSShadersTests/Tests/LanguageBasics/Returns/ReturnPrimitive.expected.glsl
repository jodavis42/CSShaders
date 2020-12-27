#version 450

struct ReturnPrimitive
{
    int empty_struct_member;
};

void ReturnPrimitive_InitGlobals()
{
}

void ReturnPrimitive_PreConstructor(ReturnPrimitive self)
{
}

void ReturnPrimitive_DefaultConstructor(ReturnPrimitive self)
{
    ReturnPrimitive_PreConstructor(self);
}

void ReturnPrimitive_CopyInputs(ReturnPrimitive self)
{
}

int IntReturn(ReturnPrimitive self)
{
    return 1;
}

void Main(ReturnPrimitive self)
{
    int i = IntReturn(self);
}

void ReturnPrimitive_CopyOutputs(ReturnPrimitive self)
{
}

void main()
{
    ReturnPrimitive_InitGlobals();
    ReturnPrimitive self;
    ReturnPrimitive_DefaultConstructor(self);
    ReturnPrimitive_CopyInputs(self);
    Main(self);
    ReturnPrimitive_CopyOutputs(self);
}

