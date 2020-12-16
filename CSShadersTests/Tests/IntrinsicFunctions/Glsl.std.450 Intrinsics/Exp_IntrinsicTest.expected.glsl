#version 450

struct ExpTest
{
    int empty_struct_member;
};

void ExpTest_InitGlobals()
{
}

void ExpTest_PreConstructor(ExpTest self)
{
}

void ExpTest_DefaultConstructor(ExpTest self)
{
    ExpTest_PreConstructor(self);
}

void ExpTest_CopyInputs(ExpTest self)
{
}

void Main(ExpTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = exp(floatVal);
    vector2Val = exp(vector2Val);
    vector3Val = exp(vector3Val);
    vector4Val = exp(vector4Val);
}

void ExpTest_CopyOutputs(ExpTest self)
{
}

void main()
{
    ExpTest_InitGlobals();
    ExpTest self;
    ExpTest_DefaultConstructor(self);
    ExpTest_CopyInputs(self);
    Main(self);
    ExpTest_CopyOutputs(self);
}

