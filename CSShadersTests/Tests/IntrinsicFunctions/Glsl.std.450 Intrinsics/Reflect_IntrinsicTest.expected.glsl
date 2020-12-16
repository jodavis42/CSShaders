#version 450

struct ReflectTest
{
    int empty_struct_member;
};

void ReflectTest_InitGlobals()
{
}

void ReflectTest_PreConstructor(ReflectTest self)
{
}

void ReflectTest_DefaultConstructor(ReflectTest self)
{
    ReflectTest_PreConstructor(self);
}

void ReflectTest_CopyInputs(ReflectTest self)
{
}

void Main(ReflectTest self)
{
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    vector2Val = reflect(vector2Val, vector2Val);
    vector3Val = reflect(vector3Val, vector3Val);
    vector4Val = reflect(vector4Val, vector4Val);
}

void ReflectTest_CopyOutputs(ReflectTest self)
{
}

void main()
{
    ReflectTest_InitGlobals();
    ReflectTest self;
    ReflectTest_DefaultConstructor(self);
    ReflectTest_CopyInputs(self);
    Main(self);
    ReflectTest_CopyOutputs(self);
}

