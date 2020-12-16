#version 450

struct OpSelectTest
{
    int empty_struct_member;
};

void OpSelectTest_InitGlobals()
{
}

void OpSelectTest_PreConstructor(OpSelectTest self)
{
}

void OpSelectTest_DefaultConstructor(OpSelectTest self)
{
    OpSelectTest_PreConstructor(self);
}

void OpSelectTest_CopyInputs(OpSelectTest self)
{
}

void Main(OpSelectTest self)
{
    bool boolVal = false;
    bvec2 bool2Val = bvec2(false);
    bvec3 bool3Val = bvec3(false);
    bvec4 bool4Val = bvec4(false);
    int intVal = 0;
    ivec2 integer2Val = ivec2(0);
    ivec3 integer3Val = ivec3(0);
    ivec4 integer4Val = ivec4(0);
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    vec3 vector3Val = vec3(0.0);
    vec4 vector4Val = vec4(0.0);
    boolVal = boolVal ? boolVal : boolVal;
    bool2Val = mix(bool2Val, bool2Val, bool2Val);
    bool3Val = mix(bool3Val, bool3Val, bool3Val);
    bool4Val = mix(bool4Val, bool4Val, bool4Val);
    intVal = boolVal ? intVal : intVal;
    integer2Val = mix(integer2Val, integer2Val, bool2Val);
    integer3Val = mix(integer3Val, integer3Val, bool3Val);
    integer4Val = mix(integer4Val, integer4Val, bool4Val);
    floatVal = boolVal ? floatVal : floatVal;
    vector2Val = mix(vector2Val, vector2Val, bool2Val);
    vector3Val = mix(vector3Val, vector3Val, bool3Val);
    vector4Val = mix(vector4Val, vector4Val, bool4Val);
}

void OpSelectTest_CopyOutputs(OpSelectTest self)
{
}

void main()
{
    OpSelectTest_InitGlobals();
    OpSelectTest self;
    OpSelectTest_DefaultConstructor(self);
    OpSelectTest_CopyInputs(self);
    Main(self);
    OpSelectTest_CopyOutputs(self);
}

