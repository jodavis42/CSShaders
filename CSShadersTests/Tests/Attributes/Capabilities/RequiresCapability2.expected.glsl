#version 450

struct RequiresCapability2
{
    int empty_struct_member;
};

void RequiresCapability2_InitGlobals()
{
}

void RequiresCapability2_PreConstructor(RequiresCapability2 self)
{
}

void RequiresCapability2_DefaultConstructor(RequiresCapability2 self)
{
    RequiresCapability2_PreConstructor(self);
}

void RequiresCapability2_CopyInputs(RequiresCapability2 self)
{
}

void Main(RequiresCapability2 self)
{
}

void RequiresCapability2_CopyOutputs(RequiresCapability2 self)
{
}

void main()
{
    RequiresCapability2_InitGlobals();
    RequiresCapability2 self;
    RequiresCapability2_DefaultConstructor(self);
    RequiresCapability2_CopyInputs(self);
    Main(self);
    RequiresCapability2_CopyOutputs(self);
}

