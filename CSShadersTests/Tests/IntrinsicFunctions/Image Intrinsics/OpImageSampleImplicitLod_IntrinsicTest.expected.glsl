#version 450

struct OpImageSampleImplicitLodTest
{
    int empty_struct_member;
};

uniform sampler2D FloatSampledImage2dVal;

void OpImageSampleImplicitLodTest_InitGlobals()
{
}

void OpImageSampleImplicitLodTest_PreConstructor(OpImageSampleImplicitLodTest self)
{
}

void OpImageSampleImplicitLodTest_DefaultConstructor(OpImageSampleImplicitLodTest self)
{
    OpImageSampleImplicitLodTest_PreConstructor(self);
}

void OpImageSampleImplicitLodTest_CopyInputs(OpImageSampleImplicitLodTest self)
{
}

void Main(OpImageSampleImplicitLodTest self)
{
    vec4 vector4Val = vec4(0.0);
    vec2 vector2Val = vec2(0.0);
    vector4Val = texture(FloatSampledImage2dVal, vector2Val);
}

void OpImageSampleImplicitLodTest_CopyOutputs(OpImageSampleImplicitLodTest self)
{
}

void main()
{
    OpImageSampleImplicitLodTest_InitGlobals();
    OpImageSampleImplicitLodTest self;
    OpImageSampleImplicitLodTest_DefaultConstructor(self);
    OpImageSampleImplicitLodTest_CopyInputs(self);
    Main(self);
    OpImageSampleImplicitLodTest_CopyOutputs(self);
}

