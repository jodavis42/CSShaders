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

void SimpleImageIntrinsicFunction_PreConstructor(SimpleImageIntrinsicFunction self)
{
}

void SimpleImageIntrinsicFunction_DefaultConstructor(SimpleImageIntrinsicFunction self)
{
    SimpleImageIntrinsicFunction_PreConstructor(self);
}

void Main(SimpleImageIntrinsicFunction self)
{
    vec2 uvs = vec2(0.0);
    vec4 value = texture(SPIRV_Cross_CombinedSimpleFloatImage2dSimpleSampler, uvs);
}

void main()
{
    SimpleImageIntrinsicFunction self;
    SimpleImageIntrinsicFunction_DefaultConstructor(self);
    Main(self);
}

