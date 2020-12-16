#version 450

struct TruncTest
{
    int empty_struct_member;
};

void TruncTest_InitGlobals()
{
}

void TruncTest_PreConstructor(TruncTest self)
{
}

void TruncTest_DefaultConstructor(TruncTest self)
{
    TruncTest_PreConstructor(self);
}

void TruncTest_CopyInputs(TruncTest self)
{
}

void Main(TruncTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = trunc(floatVal);
    vector2Val = trunc(vector2Val);
    vector3Val = trunc(vector3Val);
    vector4Val = trunc(vector4Val);
}

void TruncTest_CopyOutputs(TruncTest self)
{
}

void main()
{
    TruncTest_InitGlobals();
    TruncTest self;
    TruncTest_DefaultConstructor(self);
    TruncTest_CopyInputs(self);
    Main(self);
    TruncTest_CopyOutputs(self);
}

