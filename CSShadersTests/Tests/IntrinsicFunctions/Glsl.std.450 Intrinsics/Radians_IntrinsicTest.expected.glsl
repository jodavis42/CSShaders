#version 450

struct RadiansTest
{
    int empty_struct_member;
};

void RadiansTest_PreConstructor(RadiansTest self)
{
}

void RadiansTest_DefaultConstructor(RadiansTest self)
{
    RadiansTest_PreConstructor(self);
}

void Main(RadiansTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = radians(floatVal);
    vector2Val = radians(vector2Val);
    vector3Val = radians(vector3Val);
    vector4Val = radians(vector4Val);
}

void main()
{
    RadiansTest self;
    RadiansTest_DefaultConstructor(self);
    Main(self);
}

