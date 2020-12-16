#version 450

struct CeilTest
{
    int empty_struct_member;
};

void CeilTest_InitGlobals()
{
}

void CeilTest_PreConstructor(CeilTest self)
{
}

void CeilTest_DefaultConstructor(CeilTest self)
{
    CeilTest_PreConstructor(self);
}

void CeilTest_CopyInputs(CeilTest self)
{
}

void Main(CeilTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = ceil(floatVal);
    vector2Val = ceil(vector2Val);
    vector3Val = ceil(vector3Val);
    vector4Val = ceil(vector4Val);
}

void CeilTest_CopyOutputs(CeilTest self)
{
}

void main()
{
    CeilTest_InitGlobals();
    CeilTest self;
    CeilTest_DefaultConstructor(self);
    CeilTest_CopyInputs(self);
    Main(self);
    CeilTest_CopyOutputs(self);
}

