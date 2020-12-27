#version 450

struct OpImageFetchTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;
uniform sampler2D SPIRV_Cross_CombinedFloatImage2dValSPIRV_Cross_DummySampler;

void OpImageFetchTest_InitGlobals()
{
}

void OpImageFetchTest_PreConstructor(OpImageFetchTest self)
{
}

void OpImageFetchTest_DefaultConstructor(OpImageFetchTest self)
{
    OpImageFetchTest_PreConstructor(self);
}

void OpImageFetchTest_CopyInputs(OpImageFetchTest self)
{
}

void Main(OpImageFetchTest self)
{
    vec4 vector4Val = vec4(0.0);
    ivec2 integer2Val = ivec2(0);
    int intVal = 0;
    vector4Val = texelFetch(SPIRV_Cross_CombinedFloatImage2dValSPIRV_Cross_DummySampler, integer2Val, intVal);
    vector4Val = texelFetch(FloatSampledImage2dVal, integer2Val, intVal);
}

void OpImageFetchTest_CopyOutputs(OpImageFetchTest self)
{
}

void main()
{
    OpImageFetchTest_InitGlobals();
    OpImageFetchTest self;
    OpImageFetchTest_DefaultConstructor(self);
    OpImageFetchTest_CopyInputs(self);
    Main(self);
    OpImageFetchTest_CopyOutputs(self);
}

