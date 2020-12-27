#version 450

struct OpImageSampleDrefImplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2DShadow FloatSampledImage2dVal;
uniform sampler2DShadow SPIRV_Cross_CombinedFloatImage2dValSamplerVal;

void OpImageSampleDrefImplicitLodTest_InitGlobals()
{
}

void OpImageSampleDrefImplicitLodTest_PreConstructor(OpImageSampleDrefImplicitLodTest self)
{
}

void OpImageSampleDrefImplicitLodTest_DefaultConstructor(OpImageSampleDrefImplicitLodTest self)
{
    OpImageSampleDrefImplicitLodTest_PreConstructor(self);
}

void OpImageSampleDrefImplicitLodTest_CopyInputs(OpImageSampleDrefImplicitLodTest self)
{
}

void Main(OpImageSampleDrefImplicitLodTest self)
{
    float floatVal = 0.0;
    vec2 vector2Val = vec2(0.0);
    floatVal = texture(SPIRV_Cross_CombinedFloatImage2dValSamplerVal, vec3(vector2Val, floatVal));
    floatVal = texture(FloatSampledImage2dVal, vec3(vector2Val, floatVal));
}

void OpImageSampleDrefImplicitLodTest_CopyOutputs(OpImageSampleDrefImplicitLodTest self)
{
}

void main()
{
    OpImageSampleDrefImplicitLodTest_InitGlobals();
    OpImageSampleDrefImplicitLodTest self;
    OpImageSampleDrefImplicitLodTest_DefaultConstructor(self);
    OpImageSampleDrefImplicitLodTest_CopyInputs(self);
    Main(self);
    OpImageSampleDrefImplicitLodTest_CopyOutputs(self);
}

