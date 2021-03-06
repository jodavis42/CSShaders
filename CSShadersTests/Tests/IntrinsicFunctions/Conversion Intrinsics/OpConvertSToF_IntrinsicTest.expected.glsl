#version 450

struct OpConvertSToFTest
{
    int empty_struct_member;
};

void OpConvertSToFTest_PreConstructor(OpConvertSToFTest self)
{
}

void OpConvertSToFTest_DefaultConstructor(OpConvertSToFTest self)
{
    OpConvertSToFTest_PreConstructor(self);
}

void Main(OpConvertSToFTest self)
{
    float floatVal = 0.0;
    int intVal = 0;
    vec2 vector2Val = vec2(0.0);
    ivec2 integer2Val = ivec2(0);
    vec3 vector3Val = vec3(0.0);
    ivec3 integer3Val = ivec3(0);
    vec4 vector4Val = vec4(0.0);
    ivec4 integer4Val = ivec4(0);
    floatVal = float(intVal);
    vector2Val = vec2(integer2Val);
    vector3Val = vec3(integer3Val);
    vector4Val = vec4(integer4Val);
}

void main()
{
    OpConvertSToFTest self;
    OpConvertSToFTest_DefaultConstructor(self);
    Main(self);
}

