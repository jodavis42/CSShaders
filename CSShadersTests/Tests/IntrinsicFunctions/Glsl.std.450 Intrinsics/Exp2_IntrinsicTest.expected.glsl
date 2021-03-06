#version 450

struct Exp2Test
{
    int empty_struct_member;
};

void Exp2Test_PreConstructor(Exp2Test self)
{
}

void Exp2Test_DefaultConstructor(Exp2Test self)
{
    Exp2Test_PreConstructor(self);
}

void Main(Exp2Test self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = exp2(floatVal);
    vector2Val = exp2(vector2Val);
    vector3Val = exp2(vector3Val);
    vector4Val = exp2(vector4Val);
}

void main()
{
    Exp2Test self;
    Exp2Test_DefaultConstructor(self);
    Main(self);
}

