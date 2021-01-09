#version 450

struct Int2
{
    int X;
    int Y;
};

struct MyInt
{
    int Value;
};

struct CustomCast
{
    int empty_struct_member;
};

void CustomCast_InitGlobals()
{
}

void CustomCast_PreConstructor(CustomCast self)
{
}

void CustomCast_DefaultConstructor(CustomCast self)
{
    CustomCast_PreConstructor(self);
}

void CustomCast_CopyInputs(CustomCast self)
{
}

int op_Implicit(MyInt value)
{
    return value.Value;
}

Int2 op_Implicit_1(MyInt value)
{
    Int2 tempInt2;
    tempInt2.X = value.Value;
    tempInt2.Y = value.Value;
    return tempInt2;
}

void Main(CustomCast self)
{
    MyInt tempMyInt;
    MyInt myInt = tempMyInt;
    int i = op_Implicit(myInt);
    Int2 i2 = op_Implicit_1(myInt);
}

void CustomCast_CopyOutputs(CustomCast self)
{
}

void main()
{
    CustomCast_InitGlobals();
    CustomCast self;
    CustomCast_DefaultConstructor(self);
    CustomCast_CopyInputs(self);
    Main(self);
    CustomCast_CopyOutputs(self);
}

