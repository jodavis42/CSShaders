#version 450

struct CoshTest
{
    int empty_struct_member;
};

void CoshTest_PreConstructor(CoshTest self)
{
}

void CoshTest_DefaultConstructor(CoshTest self)
{
    CoshTest_PreConstructor(self);
}

void Main(CoshTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = cosh(floatVal);
    vector2Val = cosh(vector2Val);
    vector3Val = cosh(vector3Val);
    vector4Val = cosh(vector4Val);
}

void main()
{
    CoshTest self;
    CoshTest_DefaultConstructor(self);
    Main(self);
}

