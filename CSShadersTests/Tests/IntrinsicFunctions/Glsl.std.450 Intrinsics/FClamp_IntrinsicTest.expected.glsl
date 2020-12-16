#version 450

struct FClampTest
{
    int empty_struct_member;
};

void FClampTest_InitGlobals()
{
}

void FClampTest_PreConstructor(FClampTest self)
{
}

void FClampTest_DefaultConstructor(FClampTest self)
{
    FClampTest_PreConstructor(self);
}

void FClampTest_CopyInputs(FClampTest self)
{
}

void Main(FClampTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = clamp(floatVal, floatVal, floatVal);
    vector2Val = clamp(vector2Val, vector2Val, vector2Val);
    vector3Val = clamp(vector3Val, vector3Val, vector3Val);
    vector4Val = clamp(vector4Val, vector4Val, vector4Val);
}

void FClampTest_CopyOutputs(FClampTest self)
{
}

void main()
{
    FClampTest_InitGlobals();
    FClampTest self;
    FClampTest_DefaultConstructor(self);
    FClampTest_CopyInputs(self);
    Main(self);
    FClampTest_CopyOutputs(self);
}

