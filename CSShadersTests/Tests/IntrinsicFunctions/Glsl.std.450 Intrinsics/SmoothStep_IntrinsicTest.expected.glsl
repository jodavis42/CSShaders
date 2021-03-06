#version 450

struct SmoothStepTest
{
    int empty_struct_member;
};

void SmoothStepTest_PreConstructor(SmoothStepTest self)
{
}

void SmoothStepTest_DefaultConstructor(SmoothStepTest self)
{
    SmoothStepTest_PreConstructor(self);
}

void Main(SmoothStepTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = smoothstep(floatVal, floatVal, floatVal);
    vector2Val = smoothstep(vector2Val, vector2Val, vector2Val);
    vector3Val = smoothstep(vector3Val, vector3Val, vector3Val);
    vector4Val = smoothstep(vector4Val, vector4Val, vector4Val);
}

void main()
{
    SmoothStepTest self;
    SmoothStepTest_DefaultConstructor(self);
    Main(self);
}

