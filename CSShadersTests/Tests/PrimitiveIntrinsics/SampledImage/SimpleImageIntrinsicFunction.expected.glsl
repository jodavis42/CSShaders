#version 450

struct SimpleImageIntrinsicFunction
{
    int empty_struct_member;
};

uniform sampler2D SimpleSampledFloatImage2d;

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
    vec4 value = texture(SimpleSampledFloatImage2d, uvs);
}

void main()
{
    SimpleImageIntrinsicFunction self;
    SimpleImageIntrinsicFunction_DefaultConstructor(self);
    Main(self);
}

