#version 450

struct FloatIncementDecrement
{
    int empty_struct_member;
};

void FloatIncementDecrement_PreConstructor(FloatIncementDecrement self)
{
}

void FloatIncementDecrement_DefaultConstructor(FloatIncementDecrement self)
{
    FloatIncementDecrement_PreConstructor(self);
}

void Main(FloatIncementDecrement self)
{
    float f = 0.0;
    float r = 0.0;
    f += 1.0;
    f += 1.0;
    r = f;
    f += 1.0;
    float _45 = f;
    f = _45 + 1.0;
    r = _45;
    f -= 1.0;
    f -= 1.0;
    r = f;
    f -= 1.0;
    float _60 = f;
    f = _60 - 1.0;
    r = _60;
}

void main()
{
    FloatIncementDecrement self;
    FloatIncementDecrement_DefaultConstructor(self);
    Main(self);
}

