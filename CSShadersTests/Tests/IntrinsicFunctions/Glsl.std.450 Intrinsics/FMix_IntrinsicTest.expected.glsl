#version 450

struct FMixTest
{
    int empty_struct_member;
};

void FMixTest_PreConstructor(FMixTest self)
{
}

void FMixTest_DefaultConstructor(FMixTest self)
{
    FMixTest_PreConstructor(self);
}

void Main(FMixTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = mix(floatVal, floatVal, floatVal);
    vector2Val = mix(vector2Val, vector2Val, vector2Val);
    vector3Val = mix(vector3Val, vector3Val, vector3Val);
    vector4Val = mix(vector4Val, vector4Val, vector4Val);
}

void main()
{
    FMixTest self;
    FMixTest_DefaultConstructor(self);
    Main(self);
}

