#version 450

struct FloatBinaryOps
{
    int empty_struct_member;
};

void FloatBinaryOps_PreConstructor(FloatBinaryOps self)
{
}

void FloatBinaryOps_DefaultConstructor(FloatBinaryOps self)
{
    FloatBinaryOps_PreConstructor(self);
}

void Main(FloatBinaryOps self)
{
    float f = 0.0;
    float r = 0.0;
    r = f + f;
    r = f - f;
    r = f * f;
    r = mod(f, f);
    bool b = false;
    b = f < f;
    b = f <= f;
    b = f > f;
    b = f >= f;
    b = f == f;
    b = f != f;
}

void main()
{
    FloatBinaryOps self;
    FloatBinaryOps_DefaultConstructor(self);
    Main(self);
}

