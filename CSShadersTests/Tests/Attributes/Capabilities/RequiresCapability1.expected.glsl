#version 450

struct RequiresCapability1
{
    int empty_struct_member;
};

void RequiresCapability1_InitGlobals()
{
}

void RequiresCapability1_PreConstructor(RequiresCapability1 self)
{
}

void RequiresCapability1_DefaultConstructor(RequiresCapability1 self)
{
    RequiresCapability1_PreConstructor(self);
}

void RequiresCapability1_CopyInputs(RequiresCapability1 self)
{
}

void Main(RequiresCapability1 self)
{
}

void RequiresCapability1_CopyOutputs(RequiresCapability1 self)
{
}

void main()
{
    RequiresCapability1_InitGlobals();
    RequiresCapability1 self;
    RequiresCapability1_DefaultConstructor(self);
    RequiresCapability1_CopyInputs(self);
    Main(self);
    RequiresCapability1_CopyOutputs(self);
}

