#version 450

struct CrossTest
{
    int empty_struct_member;
};

void CrossTest_PreConstructor(CrossTest self)
{
}

void CrossTest_DefaultConstructor(CrossTest self)
{
    CrossTest_PreConstructor(self);
}

void Main(CrossTest self)
{
    vec3 vector3Val = vec3(0.0);
    vector3Val = cross(vector3Val, vector3Val);
}

void main()
{
    CrossTest self;
    CrossTest_DefaultConstructor(self);
    Main(self);
}

