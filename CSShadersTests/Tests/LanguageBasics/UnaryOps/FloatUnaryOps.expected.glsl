#version 450

struct FloatUnaryOps
{
    int empty_struct_member;
};

void FloatUnaryOps_PreConstructor(FloatUnaryOps self)
{
}

void FloatUnaryOps_DefaultConstructor(FloatUnaryOps self)
{
    FloatUnaryOps_PreConstructor(self);
}

void Main(FloatUnaryOps self)
{
    float f = 0.0;
    float r = -f;
}

void main()
{
    FloatUnaryOps self;
    FloatUnaryOps_DefaultConstructor(self);
    Main(self);
}

