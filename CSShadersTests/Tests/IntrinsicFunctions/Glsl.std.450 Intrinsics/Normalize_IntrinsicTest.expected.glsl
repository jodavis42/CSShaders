#version 450

struct NormalizeTest
{
    int empty_struct_member;
};

void NormalizeTest_PreConstructor(NormalizeTest self)
{
}

void NormalizeTest_DefaultConstructor(NormalizeTest self)
{
    NormalizeTest_PreConstructor(self);
}

void Main(NormalizeTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val = normalize(vector2Val);
    vector3Val = normalize(vector3Val);
    vector4Val = normalize(vector4Val);
}

void main()
{
    NormalizeTest self;
    NormalizeTest_DefaultConstructor(self);
    Main(self);
}

