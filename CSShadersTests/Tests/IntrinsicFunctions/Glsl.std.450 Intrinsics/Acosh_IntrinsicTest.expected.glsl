#version 450

struct AcoshTest
{
    int empty_struct_member;
};

void AcoshTest_InitGlobals()
{
}

void AcoshTest_PreConstructor(AcoshTest self)
{
}

void AcoshTest_DefaultConstructor(AcoshTest self)
{
    AcoshTest_PreConstructor(self);
}

void AcoshTest_CopyInputs(AcoshTest self)
{
}

void Main(AcoshTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = acosh(floatVal);
    vector2Val = acosh(vector2Val);
    vector3Val = acosh(vector3Val);
    vector4Val = acosh(vector4Val);
}

void AcoshTest_CopyOutputs(AcoshTest self)
{
}

void main()
{
    AcoshTest_InitGlobals();
    AcoshTest self;
    AcoshTest_DefaultConstructor(self);
    AcoshTest_CopyInputs(self);
    Main(self);
    AcoshTest_CopyOutputs(self);
}

