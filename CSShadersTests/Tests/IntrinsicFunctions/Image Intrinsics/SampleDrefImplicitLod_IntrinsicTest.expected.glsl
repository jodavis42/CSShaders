#version 450

struct OpImageSampleDrefImplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2DShadow FloatSampledImage2dVal;
uniform sampler2DShadow SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleDrefImplicitLodTest_PreConstructor(OpImageSampleDrefImplicitLodTest self)
{
}

void OpImageSampleDrefImplicitLodTest_DefaultConstructor(OpImageSampleDrefImplicitLodTest self)
{
    OpImageSampleDrefImplicitLodTest_PreConstructor(self);
}

void Main(OpImageSampleDrefImplicitLodTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    floatVal = texture(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vec3(vector2Val, floatVal));
    floatVal = texture(FloatSampledImage2dVal, vec3(vector2Val, floatVal));
}

void main()
{
    OpImageSampleDrefImplicitLodTest self;
    OpImageSampleDrefImplicitLodTest_DefaultConstructor(self);
    Main(self);
}

