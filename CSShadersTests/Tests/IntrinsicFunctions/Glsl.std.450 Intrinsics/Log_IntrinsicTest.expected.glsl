#version 450

struct LogTest
{
    int empty_struct_member;
};

void LogTest_PreConstructor(LogTest self)
{
}

void LogTest_DefaultConstructor(LogTest self)
{
    LogTest_PreConstructor(self);
}

void Main(LogTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = log(floatVal);
    vector2Val = log(vector2Val);
    vector3Val = log(vector3Val);
    vector4Val = log(vector4Val);
}

void main()
{
    LogTest self;
    LogTest_DefaultConstructor(self);
    Main(self);
}

