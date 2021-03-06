#version 450

struct ReturnVoid
{
    int empty_struct_member;
};

void ReturnVoid_PreConstructor(ReturnVoid self)
{
}

void ReturnVoid_DefaultConstructor(ReturnVoid self)
{
    ReturnVoid_PreConstructor(self);
}

void VoidReturn(ReturnVoid self)
{
}

void Main(ReturnVoid self)
{
    VoidReturn(self);
}

void main()
{
    ReturnVoid self;
    ReturnVoid_DefaultConstructor(self);
    Main(self);
}

