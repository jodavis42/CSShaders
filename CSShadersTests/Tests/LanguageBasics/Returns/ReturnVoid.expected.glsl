#version 450

struct ReturnVoid
{
    int empty_struct_member;
};

void ReturnVoid_InitGlobals()
{
}

void ReturnVoid_PreConstructor(ReturnVoid self)
{
}

void ReturnVoid_DefaultConstructor(ReturnVoid self)
{
    ReturnVoid_PreConstructor(self);
}

void ReturnVoid_CopyInputs(ReturnVoid self)
{
}

void VoidReturn(ReturnVoid self)
{
}

void Main(ReturnVoid self)
{
    VoidReturn(self);
}

void ReturnVoid_CopyOutputs(ReturnVoid self)
{
}

void main()
{
    ReturnVoid_InitGlobals();
    ReturnVoid self;
    ReturnVoid_DefaultConstructor(self);
    ReturnVoid_CopyInputs(self);
    Main(self);
    ReturnVoid_CopyOutputs(self);
}

