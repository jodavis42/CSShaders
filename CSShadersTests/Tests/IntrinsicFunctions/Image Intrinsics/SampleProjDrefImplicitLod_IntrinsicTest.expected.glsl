#version 450

struct OpImageSampleProjDrefImplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2DShadow FloatSampledImage2dVal;
uniform sampler2DShadow SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleProjDrefImplicitLodTest_PreConstructor(OpImageSampleProjDrefImplicitLodTest self)
{
}

void OpImageSampleProjDrefImplicitLodTest_DefaultConstructor(OpImageSampleProjDrefImplicitLodTest self)
{
    OpImageSampleProjDrefImplicitLodTest_PreConstructor(self);
}

void Main(OpImageSampleProjDrefImplicitLodTest self)
{
    float floatVal = 0.0;
    vec3 vector3Val = vec3(0.0);
    floatVal = textureProj(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vec4(vector3Val.xy, floatVal, vector3Val.z));
    floatVal = textureProj(FloatSampledImage2dVal, vec4(vector3Val.xy, floatVal, vector3Val.z));
}

void main()
{
    OpImageSampleProjDrefImplicitLodTest self;
    OpImageSampleProjDrefImplicitLodTest_DefaultConstructor(self);
    Main(self);
}

