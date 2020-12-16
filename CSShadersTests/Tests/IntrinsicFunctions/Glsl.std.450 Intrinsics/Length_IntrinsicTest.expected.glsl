#version 450

struct LengthTest
{
    int empty_struct_member;
};

void LengthTest_InitGlobals()
{
}

void LengthTest_PreConstructor(LengthTest self)
{
}

void LengthTest_DefaultConstructor(LengthTest self)
{
    LengthTest_PreConstructor(self);
}

void LengthTest_CopyInputs(LengthTest self)
{
}

void Main(LengthTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = length(vector2Val);
    floatVal = length(vector3Val);
    floatVal = length(vector4Val);
}

void LengthTest_CopyOutputs(LengthTest self)
{
}

void main()
{
    LengthTest_InitGlobals();
    LengthTest self;
    LengthTest_DefaultConstructor(self);
    LengthTest_CopyInputs(self);
    Main(self);
    LengthTest_CopyOutputs(self);
}

