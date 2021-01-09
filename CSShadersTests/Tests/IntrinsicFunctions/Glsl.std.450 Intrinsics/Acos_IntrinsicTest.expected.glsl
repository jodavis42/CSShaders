#version 450

struct AcosTest
{
    int empty_struct_member;
};

void AcosTest_InitGlobals()
{
}

void AcosTest_PreConstructor(AcosTest self)
{
}

void AcosTest_DefaultConstructor(AcosTest self)
{
    AcosTest_PreConstructor(self);
}

void AcosTest_CopyInputs(AcosTest self)
{
}

void Main(AcosTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = acos(floatVal);
    vector2Val = acos(vector2Val);
    vector3Val = acos(vector3Val);
    vector4Val = acos(vector4Val);
}

void AcosTest_CopyOutputs(AcosTest self)
{
}

void main()
{
    AcosTest_InitGlobals();
    AcosTest self;
    AcosTest_DefaultConstructor(self);
    AcosTest_CopyInputs(self);
    Main(self);
    AcosTest_CopyOutputs(self);
}
