#version 450

struct OpImageSampleProjDrefExplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2DShadow FloatSampledImage2dVal;
uniform sampler2DShadow SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleProjDrefExplicitLodTest_PreConstructor(OpImageSampleProjDrefExplicitLodTest self)
{
}

void OpImageSampleProjDrefExplicitLodTest_DefaultConstructor(OpImageSampleProjDrefExplicitLodTest self)
{
    OpImageSampleProjDrefExplicitLodTest_PreConstructor(self);
}

void Main(OpImageSampleProjDrefExplicitLodTest self)
{
    float floatVal = 0.0;
    vec3 vector3Val = vec3(0.0);
    floatVal = textureProjLod(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vec4(vector3Val.xy, floatVal, vector3Val.z), floatVal);
    floatVal = textureProjLod(FloatSampledImage2dVal, vec4(vector3Val.xy, floatVal, vector3Val.z), floatVal);
}

void main()
{
    OpImageSampleProjDrefExplicitLodTest self;
    OpImageSampleProjDrefExplicitLodTest_DefaultConstructor(self);
    Main(self);
}

