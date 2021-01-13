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

void MyInt_PreConstructor(inout MyInt self)
{
    self.Value = 0;
}

void MyInt_DefaultConstructor(inout MyInt self)
{
    MyInt_PreConstructor(self);
}

int op_Implicit(MyInt value)
{
    return value.Value;
}

void Int2_PreConstructor(inout Int2 self)
{
    self.X = 0;
    self.Y = 0;
}

void Int2_DefaultConstructor(inout Int2 self)
{
    Int2_PreConstructor(self);
}

Int2 op_Implicit_1(MyInt value)
{
    Int2 tempInt2;
    Int2_DefaultConstructor(tempInt2);
    tempInt2.X = value.Value;
    tempInt2.Y = value.Value;
    return tempInt2;
}

void Main(CustomCast self)
{
    MyInt tempMyInt;
    MyInt_DefaultConstructor(tempMyInt);
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

