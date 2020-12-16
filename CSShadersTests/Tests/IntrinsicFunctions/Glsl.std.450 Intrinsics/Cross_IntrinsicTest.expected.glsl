#version 450

struct CrossTest
{
    int empty_struct_member;
};

void CrossTest_InitGlobals()
{
}

void CrossTest_PreConstructor(CrossTest self)
{
}

void CrossTest_DefaultConstructor(CrossTest self)
{
    CrossTest_PreConstructor(self);
}

void CrossTest_CopyInputs(CrossTest self)
{
}

void Main(CrossTest self)
{
    vec3 vector3Val = vec3(0.0);
    vector3Val = cross(vector3Val, vector3Val);
}

void CrossTest_CopyOutputs(CrossTest self)
{
}

void main()
{
    CrossTest_InitGlobals();
    CrossTest self;
    CrossTest_DefaultConstructor(self);
    CrossTest_CopyInputs(self);
    Main(self);
    CrossTest_CopyOutputs(self);
}

