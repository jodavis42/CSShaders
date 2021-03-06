#version 450

struct Unsigned
{
    int empty_struct_member;
};

void Unsigned_PreConstructor(Unsigned self)
{
}

void Unsigned_DefaultConstructor(Unsigned self)
{
    Unsigned_PreConstructor(self);
}

void Main(Unsigned self)
{
    int u = 0;
    int r = 0;
    u++;
    u++;
    r = u;
    u++;
    int _45 = u;
    u = _45 + 1;
    r = _45;
    u--;
    u--;
    r = u;
    u--;
    int _60 = u;
    u = _60 - 1;
    r = _60;
}

void main()
{
    Unsigned self;
    Unsigned_DefaultConstructor(self);
    Main(self);
}

