#version 450

struct Log2Test
{
    int empty_struct_member;
};

void Log2Test_InitGlobals()
{
}

void Log2Test_PreConstructor(Log2Test self)
{
}

void Log2Test_DefaultConstructor(Log2Test self)
{
    Log2Test_PreConstructor(self);
}

void Log2Test_CopyInputs(Log2Test self)
{
}

void Main(Log2Test self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    floatVal = log2(floatVal);
    vector2Val = log2(vector2Val);
    vector3Val = log2(vector3Val);
    vector4Val = log2(vector4Val);
}

void Log2Test_CopyOutputs(Log2Test self)
{
}

void main()
{
    Log2Test_InitGlobals();
    Log2Test self;
    Log2Test_DefaultConstructor(self);
    Log2Test_CopyInputs(self);
    Main(self);
    Log2Test_CopyOutputs(self);
}

