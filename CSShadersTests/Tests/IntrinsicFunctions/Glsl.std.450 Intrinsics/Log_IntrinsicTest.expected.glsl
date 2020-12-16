#version 450

struct LogTest
{
    int empty_struct_member;
};

void LogTest_InitGlobals()
{
}

void LogTest_PreConstructor(LogTest self)
{
}

void LogTest_DefaultConstructor(LogTest self)
{
    LogTest_PreConstructor(self);
}

void LogTest_CopyInputs(LogTest self)
{
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

void LogTest_CopyOutputs(LogTest self)
{
}

void main()
{
    LogTest_InitGlobals();
    LogTest self;
    LogTest_DefaultConstructor(self);
    LogTest_CopyInputs(self);
    Main(self);
    LogTest_CopyOutputs(self);
}

