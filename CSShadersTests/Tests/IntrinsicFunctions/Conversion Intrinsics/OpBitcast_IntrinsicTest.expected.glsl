#version 450

struct OpBitcastTest
{
    int empty_struct_member;
};

void OpBitcastTest_InitGlobals()
{
}

void OpBitcastTest_PreConstructor(OpBitcastTest self)
{
}

void OpBitcastTest_DefaultConstructor(OpBitcastTest self)
{
    OpBitcastTest_PreConstructor(self);
}

void OpBitcastTest_CopyInputs(OpBitcastTest self)
{
}

void Main(OpBitcastTest self)
{
    float floatVal = 0.0;
    int intVal = 0;
    vec2 vector2Val = vec2(0.0);
    ivec2 integer2Val = ivec2(0);
    vec3 vector3Val = vec3(0.0);
    ivec3 integer3Val = ivec3(0);
    vec4 vector4Val = vec4(0.0);
    ivec4 integer4Val = ivec4(0);
    floatVal = intBitsToFloat(intVal);
    vector2Val = intBitsToFloat(integer2Val);
    vector3Val = intBitsToFloat(integer3Val);
    vector4Val = intBitsToFloat(integer4Val);
}

void OpBitcastTest_CopyOutputs(OpBitcastTest self)
{
}

void main()
{
    OpBitcastTest_InitGlobals();
    OpBitcastTest self;
    OpBitcastTest_DefaultConstructor(self);
    OpBitcastTest_CopyInputs(self);
    Main(self);
    OpBitcastTest_CopyOutputs(self);
}

