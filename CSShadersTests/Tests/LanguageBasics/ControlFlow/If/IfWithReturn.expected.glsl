#version 450

struct IfWithReturn
{
    int empty_struct_member;
};

void IfWithReturn_PreConstructor(IfWithReturn self)
{
}

void IfWithReturn_DefaultConstructor(IfWithReturn self)
{
    IfWithReturn_PreConstructor(self);
}

int IfWithReturn0(IfWithReturn self)
{
    int a = 0;
    return a;
}

void Main(IfWithReturn self)
{
    int ret = 0;
    ret = IfWithReturn0(self);
}

void main()
{
    IfWithReturn self;
    IfWithReturn_DefaultConstructor(self);
    Main(self);
}

