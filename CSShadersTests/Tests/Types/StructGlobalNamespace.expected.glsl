#version 450

struct GlobalNamespaceStruct
{
    int empty_struct_member;
};

void GlobalNamespaceStruct_PreConstructor(GlobalNamespaceStruct self)
{
}

void GlobalNamespaceStruct_DefaultConstructor(GlobalNamespaceStruct self)
{
    GlobalNamespaceStruct_PreConstructor(self);
}

void main()
{
    GlobalNamespaceStruct self;
    GlobalNamespaceStruct_DefaultConstructor(self);
}

