#version 450

struct FaceForwardTest
{
    int empty_struct_member;
};

void FaceForwardTest_PreConstructor(FaceForwardTest self)
{
}

void FaceForwardTest_DefaultConstructor(FaceForwardTest self)
{
    FaceForwardTest_PreConstructor(self);
}

void Main(FaceForwardTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val = faceforward(vector2Val, vector2Val, vector2Val);
    vector3Val = faceforward(vector3Val, vector3Val, vector3Val);
    vector4Val = faceforward(vector4Val, vector4Val, vector4Val);
}

void main()
{
    FaceForwardTest self;
    FaceForwardTest_DefaultConstructor(self);
    Main(self);
}

