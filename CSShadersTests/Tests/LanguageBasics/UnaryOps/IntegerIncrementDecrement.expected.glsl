#version 450

struct IntegerIncementDecrement
{
    int empty_struct_member;
};

void IntegerIncementDecrement_PreConstructor(IntegerIncementDecrement self)
{
}

void IntegerIncementDecrement_DefaultConstructor(IntegerIncementDecrement self)
{
    IntegerIncementDecrement_PreConstructor(self);
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

void main()
{
    IntegerIncementDecrement self;
    IntegerIncementDecrement_DefaultConstructor(self);
    Main(self);
}

