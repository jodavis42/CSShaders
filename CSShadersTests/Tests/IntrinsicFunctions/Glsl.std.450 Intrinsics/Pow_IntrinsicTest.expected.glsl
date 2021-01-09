#version 450

struct PowTest
{
    int empty_struct_member;
};

void PowTest_InitGlobals()
{
}

void PowTest_PreConstructor(PowTest self)
{
}

void PowTest_DefaultConstructor(PowTest self)
{
    PowTest_PreConstructor(self);
}

void PowTest_CopyInputs(PowTest self)
{
}

void Main(PowTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = pow(floatVal, floatVal);
    vector2Val = pow(vector2Val, vector2Val);
    vector3Val = pow(vector3Val, vector3Val);
    vector4Val = pow(vector4Val, vector4Val);
}

void PowTest_CopyOutputs(PowTest self)
{
}

void main()
{
    PowTest_InitGlobals();
    PowTest self;
    PowTest_DefaultConstructor(self);
    PowTest_CopyInputs(self);
    Main(self);
    PowTest_CopyOutputs(self);
}
