#version 450

struct PrimitiveCasts
{
    int empty_struct_member;
};

void PrimitiveCasts_PreConstructor(PrimitiveCasts self)
{
}

void PrimitiveCasts_DefaultConstructor(PrimitiveCasts self)
{
    PrimitiveCasts_PreConstructor(self);
}

void Main(PrimitiveCasts self)
{
    bool b = false;
    int i = 0;
    uint u = 0u;
    float f = 0.0;
    b = false;
    b = b;
    i = 0;
    i = i;
    i = int(0u);
    i = int(u);
    i = int(0.0);
    i = int(f);
    u = uint(0);
    u = uint(i);
    u = 0u;
    u = u;
    u = uint(0.0);
    u = uint(f);
    f = float(0);
    f = float(i);
    f = float(int(0u));
    f = float(int(u));
    f = 0.0;
    f = f;
}

void main()
{
    PrimitiveCasts self;
    PrimitiveCasts_DefaultConstructor(self);
    Main(self);
}

