#version 450

struct Atan2Test
{
    int empty_struct_member;
};

void Atan2Test_PreConstructor(Atan2Test self)
{
}

void Atan2Test_DefaultConstructor(Atan2Test self)
{
    Atan2Test_PreConstructor(self);
}

void Main(Atan2Test self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = atan(floatVal, floatVal);
    vector2Val = atan(vector2Val, vector2Val);
    vector3Val = atan(vector3Val, vector3Val);
    vector4Val = atan(vector4Val, vector4Val);
}

void main()
{
    Atan2Test self;
    Atan2Test_DefaultConstructor(self);
    Main(self);
}

