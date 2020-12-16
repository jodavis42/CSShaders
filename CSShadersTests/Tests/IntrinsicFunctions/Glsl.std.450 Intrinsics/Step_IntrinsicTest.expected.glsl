#version 450

struct StepTest
{
    int empty_struct_member;
};

void StepTest_InitGlobals()
{
}

void StepTest_PreConstructor(StepTest self)
{
}

void StepTest_DefaultConstructor(StepTest self)
{
    StepTest_PreConstructor(self);
}

void StepTest_CopyInputs(StepTest self)
{
}

void Main(StepTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = step(floatVal, floatVal);
    vector2Val = step(vector2Val, vector2Val);
    vector3Val = step(vector3Val, vector3Val);
    vector4Val = step(vector4Val, vector4Val);
}

void StepTest_CopyOutputs(StepTest self)
{
}

void main()
{
    StepTest_InitGlobals();
    StepTest self;
    StepTest_DefaultConstructor(self);
    StepTest_CopyInputs(self);
    Main(self);
    StepTest_CopyOutputs(self);
}

