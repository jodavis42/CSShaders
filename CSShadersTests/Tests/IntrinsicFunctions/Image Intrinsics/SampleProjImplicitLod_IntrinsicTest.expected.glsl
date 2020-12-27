#version 450

struct OpImageSampleProjImplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;
uniform sampler2D SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleProjImplicitLodTest_InitGlobals()
{
}

void OpImageSampleProjImplicitLodTest_PreConstructor(OpImageSampleProjImplicitLodTest self)
{
}

void OpImageSampleProjImplicitLodTest_DefaultConstructor(OpImageSampleProjImplicitLodTest self)
{
    OpImageSampleProjImplicitLodTest_PreConstructor(self);
}

void OpImageSampleProjImplicitLodTest_CopyInputs(OpImageSampleProjImplicitLodTest self)
{
}

void Main(OpImageSampleProjImplicitLodTest self)
{
    vec4 vector4Val = vec4(0.0);
    vec3 vector3Val = vec3(0.0);
    vector4Val = textureProj(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vector3Val);
    vector4Val = textureProj(FloatSampledImage2dVal, vector3Val);
}

void OpImageSampleProjImplicitLodTest_CopyOutputs(OpImageSampleProjImplicitLodTest self)
{
}

void main()
{
    OpImageSampleProjImplicitLodTest_InitGlobals();
    OpImageSampleProjImplicitLodTest self;
    OpImageSampleProjImplicitLodTest_DefaultConstructor(self);
    OpImageSampleProjImplicitLodTest_CopyInputs(self);
    Main(self);
    OpImageSampleProjImplicitLodTest_CopyOutputs(self);
}

