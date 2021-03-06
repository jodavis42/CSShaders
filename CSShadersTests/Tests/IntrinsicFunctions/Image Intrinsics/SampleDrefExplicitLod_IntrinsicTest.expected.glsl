#version 450

struct OpImageSampleDrefExplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2DShadow FloatSampledImage2dVal;
uniform sampler2DShadow SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleDrefExplicitLodTest_PreConstructor(OpImageSampleDrefExplicitLodTest self)
{
}

void OpImageSampleDrefExplicitLodTest_DefaultConstructor(OpImageSampleDrefExplicitLodTest self)
{
    OpImageSampleDrefExplicitLodTest_PreConstructor(self);
}

void Main(OpImageSampleDrefExplicitLodTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    floatVal = textureLod(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vec3(vector2Val, floatVal), floatVal);
    floatVal = textureLod(FloatSampledImage2dVal, vec3(vector2Val, floatVal), floatVal);
}

void main()
{
    OpImageSampleDrefExplicitLodTest self;
    OpImageSampleDrefExplicitLodTest_DefaultConstructor(self);
    Main(self);
}

