#version 450

struct SinhTest
{
    int empty_struct_member;
};

void SinhTest_InitGlobals()
{
}

void SinhTest_PreConstructor(SinhTest self)
{
}

void SinhTest_DefaultConstructor(SinhTest self)
{
    SinhTest_PreConstructor(self);
}

void SinhTest_CopyInputs(SinhTest self)
{
}

void Main(SinhTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = sinh(floatVal);
    vector2Val = sinh(vector2Val);
    vector3Val = sinh(vector3Val);
    vector4Val = sinh(vector4Val);
}

void SinhTest_CopyOutputs(SinhTest self)
{
}

void main()
{
    SinhTest_InitGlobals();
    SinhTest self;
    SinhTest_DefaultConstructor(self);
    SinhTest_CopyInputs(self);
    Main(self);
    SinhTest_CopyOutputs(self);
}

