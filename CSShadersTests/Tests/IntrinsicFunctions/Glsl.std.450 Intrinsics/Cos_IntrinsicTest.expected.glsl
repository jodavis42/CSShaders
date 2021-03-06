#version 450

struct CosTest
{
    int empty_struct_member;
};

void CosTest_PreConstructor(CosTest self)
{
}

void CosTest_DefaultConstructor(CosTest self)
{
    CosTest_PreConstructor(self);
}

void Main(CosTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = cos(floatVal);
    vector2Val = cos(vector2Val);
    vector3Val = cos(vector3Val);
    vector4Val = cos(vector4Val);
}

void main()
{
    CosTest self;
    CosTest_DefaultConstructor(self);
    Main(self);
}

