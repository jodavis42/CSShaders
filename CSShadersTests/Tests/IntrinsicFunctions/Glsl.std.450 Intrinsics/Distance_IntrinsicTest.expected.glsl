#version 450

struct DistanceTest
{
    int empty_struct_member;
};

void DistanceTest_PreConstructor(DistanceTest self)
{
}

void DistanceTest_DefaultConstructor(DistanceTest self)
{
    DistanceTest_PreConstructor(self);
}

void Main(DistanceTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = distance(vector2Val, vector2Val);
    floatVal = distance(vector3Val, vector3Val);
    floatVal = distance(vector4Val, vector4Val);
}

void main()
{
    DistanceTest self;
    DistanceTest_DefaultConstructor(self);
    Main(self);
}

