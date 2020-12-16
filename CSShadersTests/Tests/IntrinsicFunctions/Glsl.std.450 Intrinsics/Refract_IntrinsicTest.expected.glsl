#version 450

struct RefractTest
{
    int empty_struct_member;
};

void RefractTest_InitGlobals()
{
}

void RefractTest_PreConstructor(RefractTest self)
{
}

void RefractTest_DefaultConstructor(RefractTest self)
{
    RefractTest_PreConstructor(self);
}

void RefractTest_CopyInputs(RefractTest self)
{
}

void Main(RefractTest self)
{
    vec2 vector2Val = vec2(0.0);
    float floatVal = 0.0;
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val = refract(vector2Val, vector2Val, floatVal);
    vector3Val = refract(vector3Val, vector3Val, floatVal);
    vector4Val = refract(vector4Val, vector4Val, floatVal);
}

void RefractTest_CopyOutputs(RefractTest self)
{
}

void main()
{
    RefractTest_InitGlobals();
    RefractTest self;
    RefractTest_DefaultConstructor(self);
    RefractTest_CopyInputs(self);
    Main(self);
    RefractTest_CopyOutputs(self);
}

