#version 450

struct OpConvertFToSTest
{
    int empty_struct_member;
};

void OpConvertFToSTest_PreConstructor(OpConvertFToSTest self)
{
}

void OpConvertFToSTest_DefaultConstructor(OpConvertFToSTest self)
{
    OpConvertFToSTest_PreConstructor(self);
}

void Main(OpConvertFToSTest self)
{
    int intVal = 0;
    float floatVal = 0.0;
    ivec2 integer2Val = ivec2(0);
    vec2 vector2Val = vec2(0.0);
    ivec3 integer3Val = ivec3(0);
    vec3 vector3Val = vec3(0.0);
    ivec4 integer4Val = ivec4(0);
    vec4 vector4Val = vec4(0.0);
    intVal = int(floatVal);
    integer2Val = ivec2(vector2Val);
    integer3Val = ivec3(vector3Val);
    integer4Val = ivec4(vector4Val);
}

void main()
{
    OpConvertFToSTest self;
    OpConvertFToSTest_DefaultConstructor(self);
    Main(self);
}

