#version 450

struct FloorTest
{
    int empty_struct_member;
};

void FloorTest_InitGlobals()
{
}

void FloorTest_PreConstructor(FloorTest self)
{
}

void FloorTest_DefaultConstructor(FloorTest self)
{
    FloorTest_PreConstructor(self);
}

void FloorTest_CopyInputs(FloorTest self)
{
}

void Main(FloorTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = floor(floatVal);
    vector2Val = floor(vector2Val);
    vector3Val = floor(vector3Val);
    vector4Val = floor(vector4Val);
}

void FloorTest_CopyOutputs(FloorTest self)
{
}

void main()
{
    FloorTest_InitGlobals();
    FloorTest self;
    FloorTest_DefaultConstructor(self);
    FloorTest_CopyInputs(self);
    Main(self);
    FloorTest_CopyOutputs(self);
}

