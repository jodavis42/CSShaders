#version 450

struct RequiresCapability2
{
    int empty_struct_member;
};

void RequiresCapability2_PreConstructor(RequiresCapability2 self)
{
}

void RequiresCapability2_DefaultConstructor(RequiresCapability2 self)
{
    RequiresCapability2_PreConstructor(self);
}

void Main(RequiresCapability2 self)
{
}

void main()
{
    RequiresCapability2 self;
    RequiresCapability2_DefaultConstructor(self);
    Main(self);
}

