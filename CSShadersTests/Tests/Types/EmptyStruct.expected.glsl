#version 450

struct EmptyStruct
{
    int empty_struct_member;
};

void EmptyStruct_PreConstructor(EmptyStruct self)
{
}

void EmptyStruct_DefaultConstructor(EmptyStruct self)
{
    EmptyStruct_PreConstructor(self);
}

void main()
{
    EmptyStruct self;
    EmptyStruct_DefaultConstructor(self);
}

