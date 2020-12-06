#version 450

struct EmptyStruct
{
    int empty_struct_member;
};

void EmptyStruct_InitGlobals()
{
}

void EmptyStruct_PreConstructor(EmptyStruct self)
{
}

void EmptyStruct_DefaultConstructor(EmptyStruct self)
{
    EmptyStruct_PreConstructor(self);
}

void EmptyStruct_CopyInputs(EmptyStruct self)
{
}

void EmptyStruct_CopyOutputs(EmptyStruct self)
{
}

void main()
{
    EmptyStruct_InitGlobals();
    EmptyStruct self;
    EmptyStruct_DefaultConstructor(self);
    EmptyStruct_CopyInputs(self);
    EmptyStruct_CopyOutputs(self);
}

