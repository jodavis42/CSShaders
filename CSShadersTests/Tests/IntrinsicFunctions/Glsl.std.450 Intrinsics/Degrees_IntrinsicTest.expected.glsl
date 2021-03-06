#version 450

struct DegreesTest
{
    int empty_struct_member;
};

void DegreesTest_PreConstructor(DegreesTest self)
{
}

void DegreesTest_DefaultConstructor(DegreesTest self)
{
    DegreesTest_PreConstructor(self);
}

void Main(DegreesTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = degrees(floatVal);
    vector2Val = degrees(vector2Val);
    vector3Val = degrees(vector3Val);
    vector4Val = degrees(vector4Val);
}

void main()
{
    DegreesTest self;
    DegreesTest_DefaultConstructor(self);
    Main(self);
}

