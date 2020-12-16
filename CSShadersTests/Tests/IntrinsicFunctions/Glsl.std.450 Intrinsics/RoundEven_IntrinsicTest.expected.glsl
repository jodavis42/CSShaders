#version 450

struct RoundEvenTest
{
    int empty_struct_member;
};

void RoundEvenTest_InitGlobals()
{
}

void RoundEvenTest_PreConstructor(RoundEvenTest self)
{
}

void RoundEvenTest_DefaultConstructor(RoundEvenTest self)
{
    RoundEvenTest_PreConstructor(self);
}

void RoundEvenTest_CopyInputs(RoundEvenTest self)
{
}

void Main(RoundEvenTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = roundEven(floatVal);
    vector2Val = roundEven(vector2Val);
    vector3Val = roundEven(vector3Val);
    vector4Val = roundEven(vector4Val);
}

void RoundEvenTest_CopyOutputs(RoundEvenTest self)
{
}

void main()
{
    RoundEvenTest_InitGlobals();
    RoundEvenTest self;
    RoundEvenTest_DefaultConstructor(self);
    RoundEvenTest_CopyInputs(self);
    Main(self);
    RoundEvenTest_CopyOutputs(self);
}

