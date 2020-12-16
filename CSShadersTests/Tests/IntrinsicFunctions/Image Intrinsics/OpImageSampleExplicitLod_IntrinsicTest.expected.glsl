#version 450

struct OpImageSampleExplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;

void OpImageSampleExplicitLodTest_InitGlobals()
{
}

void OpImageSampleExplicitLodTest_PreConstructor(OpImageSampleExplicitLodTest self)
{
}

void OpImageSampleExplicitLodTest_DefaultConstructor(OpImageSampleExplicitLodTest self)
{
    OpImageSampleExplicitLodTest_PreConstructor(self);
}

void OpImageSampleExplicitLodTest_CopyInputs(OpImageSampleExplicitLodTest self)
{
}

void Main(OpImageSampleExplicitLodTest self)
{
    vec4 vector4Val = vec4(0.0);
    vec2 vector2Val = vec2(0.0);
    float floatVal = 0.0;
    vector4Val = textureLod(FloatSampledImage2dVal, vector2Val, floatVal);
}

void OpImageSampleExplicitLodTest_CopyOutputs(OpImageSampleExplicitLodTest self)
{
}

void main()
{
    OpImageSampleExplicitLodTest_InitGlobals();
    OpImageSampleExplicitLodTest self;
    OpImageSampleExplicitLodTest_DefaultConstructor(self);
    OpImageSampleExplicitLodTest_CopyInputs(self);
    Main(self);
    OpImageSampleExplicitLodTest_CopyOutputs(self);
}

