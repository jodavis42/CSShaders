#version 450

struct OpFRemTest
{
    int empty_struct_member;
};

void OpFRemTest_InitGlobals()
{
}

void OpFRemTest_PreConstructor(OpFRemTest self)
{
}

void OpFRemTest_DefaultConstructor(OpFRemTest self)
{
    OpFRemTest_PreConstructor(self);
}

void OpFRemTest_CopyInputs(OpFRemTest self)
{
}

void Main(OpFRemTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal -= floatVal * trunc(floatVal / floatVal);
    vector2Val -= vector2Val * trunc(vector2Val / vector2Val);
    vector3Val -= vector3Val * trunc(vector3Val / vector3Val);
    vector4Val -= vector4Val * trunc(vector4Val / vector4Val);
}

void OpFRemTest_CopyOutputs(OpFRemTest self)
{
}

void main()
{
    OpFRemTest_InitGlobals();
    OpFRemTest self;
    OpFRemTest_DefaultConstructor(self);
    OpFRemTest_CopyInputs(self);
    Main(self);
    OpFRemTest_CopyOutputs(self);
}

