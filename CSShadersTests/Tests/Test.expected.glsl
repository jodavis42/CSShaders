#version 450

struct MyFragment
{
    int Data;
};

void PreConstructor_MyFragment(inout MyFragment self)
{
    self.Data = 0;
}

void DefaultConstructor_MyFragment(inout MyFragment self)
{
    PreConstructor_MyFragment(self);
}

void main()
{
    MyFragment self;
    DefaultConstructor_MyFragment(self);
}

