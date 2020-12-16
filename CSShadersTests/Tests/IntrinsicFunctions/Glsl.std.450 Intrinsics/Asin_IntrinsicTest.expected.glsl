#version 450

struct AsinTest
{
    int empty_struct_member;
};

void AsinTest_InitGlobals()
{
}

void AsinTest_PreConstructor(AsinTest self)
{
}

void AsinTest_DefaultConstructor(AsinTest self)
{
    AsinTest_PreConstructor(self);
}

void AsinTest_CopyInputs(AsinTest self)
{
}

void Main(AsinTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = asin(floatVal);
    vector2Val = asin(vector2Val);
    vector3Val = asin(vector3Val);
    vector4Val = asin(vector4Val);
}

void AsinTest_CopyOutputs(AsinTest self)
{
}

void main()
{
    AsinTest_InitGlobals();
    AsinTest self;
    AsinTest_DefaultConstructor(self);
    AsinTest_CopyInputs(self);
    Main(self);
    AsinTest_CopyOutputs(self);
}

