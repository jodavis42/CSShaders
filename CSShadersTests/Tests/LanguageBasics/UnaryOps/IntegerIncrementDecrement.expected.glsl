#version 450

struct IntegerIncementDecrement
{
    int empty_struct_member;
};

void IntegerIncementDecrement_InitGlobals()
{
}

void IntegerIncementDecrement_PreConstructor(IntegerIncementDecrement self)
{
}

void IntegerIncementDecrement_DefaultConstructor(IntegerIncementDecrement self)
{
    IntegerIncementDecrement_PreConstructor(self);
}

void IntegerIncementDecrement_CopyInputs(IntegerIncementDecrement self)
{
}

void Main(IntegerIncementDecrement self)
{
    int i = 0;
    int r = 0;
    i++;
    i++;
    r = i;
    i++;
    int _45 = i;
    i = _45 + 1;
    r = _45;
    i--;
    i--;
    r = i;
    i--;
    int _60 = i;
    i = _60 - 1;
    r = _60;
}

void IntegerIncementDecrement_CopyOutputs(IntegerIncementDecrement self)
{
}

void main()
{
    IntegerIncementDecrement_InitGlobals();
    IntegerIncementDecrement self;
    IntegerIncementDecrement_DefaultConstructor(self);
    IntegerIncementDecrement_CopyInputs(self);
    Main(self);
    IntegerIncementDecrement_CopyOutputs(self);
}

