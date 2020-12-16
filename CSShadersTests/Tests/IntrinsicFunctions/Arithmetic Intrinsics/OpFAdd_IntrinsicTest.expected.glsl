#version 450

struct OpFAddTest
{
    int empty_struct_member;
};

void OpFAddTest_InitGlobals()
{
}

void OpFAddTest_PreConstructor(OpFAddTest self)
{
}

void OpFAddTest_DefaultConstructor(OpFAddTest self)
{
    OpFAddTest_PreConstructor(self);
}

void OpFAddTest_CopyInputs(OpFAddTest self)
{
}

void Main(OpFAddTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal += floatVal;
    vector2Val += vector2Val;
    vector3Val += vector3Val;
    vector4Val += vector4Val;
}

void OpFAddTest_CopyOutputs(OpFAddTest self)
{
}

void main()
{
    OpFAddTest_InitGlobals();
    OpFAddTest self;
    OpFAddTest_DefaultConstructor(self);
    OpFAddTest_CopyInputs(self);
    Main(self);
    OpFAddTest_CopyOutputs(self);
}

