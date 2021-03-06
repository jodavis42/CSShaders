#version 450

struct OpImageSampleExplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;
uniform sampler2D SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleExplicitLodTest_PreConstructor(OpImageSampleExplicitLodTest self)
{
}

void OpImageSampleExplicitLodTest_DefaultConstructor(OpImageSampleExplicitLodTest self)
{
    OpImageSampleExplicitLodTest_PreConstructor(self);
}

void Main(OpImageSampleExplicitLodTest self)
{
    vec4 vector4Val = vec4(0.0);
    vec2 vector2Val = vec2(0.0);
    float floatVal = 0.0;
    vector4Val = textureLod(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vector2Val, floatVal);
    vector4Val = textureLod(FloatSampledImage2dVal, vector2Val, floatVal);
}

void main()
{
    OpImageSampleExplicitLodTest self;
    OpImageSampleExplicitLodTest_DefaultConstructor(self);
    Main(self);
}

