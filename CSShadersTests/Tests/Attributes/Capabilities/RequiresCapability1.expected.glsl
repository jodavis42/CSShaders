#version 450

struct RequiresCapability1
{
    int empty_struct_member;
};

void RequiresCapability1_PreConstructor(RequiresCapability1 self)
{
}

void RequiresCapability1_DefaultConstructor(RequiresCapability1 self)
{
    RequiresCapability1_PreConstructor(self);
}

void Main(RequiresCapability1 self)
{
}

void main()
{
    RequiresCapability1 self;
    RequiresCapability1_DefaultConstructor(self);
    Main(self);
}

