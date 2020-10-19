#version 450

struct EmptyStruct
{
    int empty_struct_member;
};

void PreConstructor_EmptyStruct(EmptyStruct self)
{
}

void DefaultConstructor_EmptyStruct(EmptyStruct self)
{
    PreConstructor_EmptyStruct(self);
}

void main()
{
    EmptyStruct self;
    DefaultConstructor_EmptyStruct(self);
}

