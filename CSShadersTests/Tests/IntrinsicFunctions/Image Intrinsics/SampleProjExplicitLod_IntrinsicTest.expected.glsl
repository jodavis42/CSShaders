#version 450

struct OpImageSampleProjExplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;
uniform sampler2D SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleProjExplicitLodTest_InitGlobals()
{
}

void OpImageSampleProjExplicitLodTest_PreConstructor(OpImageSampleProjExplicitLodTest self)
{
}

void OpImageSampleProjExplicitLodTest_DefaultConstructor(OpImageSampleProjExplicitLodTest self)
{
    OpImageSampleProjExplicitLodTest_PreConstructor(self);
}

void OpImageSampleProjExplicitLodTest_CopyInputs(OpImageSampleProjExplicitLodTest self)
{
}

void Main(OpImageSampleProjExplicitLodTest self)
{
    vec4 vector4Val = vec4(0.0);
    vec3 vector3Val = vec3(0.0);
    float floatVal = 0.0;
    vector4Val = textureProjLod(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vector3Val, floatVal);
    vector4Val = textureProjLod(FloatSampledImage2dVal, vector3Val, floatVal);
}

void OpImageSampleProjExplicitLodTest_CopyOutputs(OpImageSampleProjExplicitLodTest self)
{
}

void main()
{
    OpImageSampleProjExplicitLodTest_InitGlobals();
    OpImageSampleProjExplicitLodTest self;
    OpImageSampleProjExplicitLodTest_DefaultConstructor(self);
    OpImageSampleProjExplicitLodTest_CopyInputs(self);
    Main(self);
    OpImageSampleProjExplicitLodTest_CopyOutputs(self);
}

