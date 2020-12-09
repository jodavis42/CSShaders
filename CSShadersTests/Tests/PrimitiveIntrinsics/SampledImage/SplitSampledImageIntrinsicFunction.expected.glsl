#version 450

struct TestIntrinsics
{
    int empty_struct_member;
};

struct SimpleImageIntrinsicFunction
{
    int empty_struct_member;
};

uniform sampler2D SPIRV_Cross_CombinedSimpleFloatImage2dSimpleSampler;

void SimpleImageIntrinsicFunction_InitGlobals()
{
}

void SimpleImageIntrinsicFunction_PreConstructor(SimpleImageIntrinsicFunction self)
{
}

void SimpleImageIntrinsicFunction_DefaultConstructor(SimpleImageIntrinsicFunction self)
{
    SimpleImageIntrinsicFunction_PreConstructor(self);
}

void SimpleImageIntrinsicFunction_CopyInputs(SimpleImageIntrinsicFunction self)
{
}

void Main(SimpleImageIntrinsicFunction self)
{
    vec2 uvs = vec2(0.0);
    vec4 value = texture(SPIRV_Cross_CombinedSimpleFloatImage2dSimpleSampler, uvs);
}

void SimpleImageIntrinsicFunction_CopyOutputs(SimpleImageIntrinsicFunction self)
{
}

void main()
{
    SimpleImageIntrinsicFunction_InitGlobals();
    SimpleImageIntrinsicFunction self;
    SimpleImageIntrinsicFunction_DefaultConstructor(self);
    SimpleImageIntrinsicFunction_CopyInputs(self);
    Main(self);
    SimpleImageIntrinsicFunction_CopyOutputs(self);
}

