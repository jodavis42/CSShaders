#version 450

struct TestIntrinsics
{
    int empty_struct_member;
};

struct ComplexImageIntrinsicFunction
{
    int empty_struct_member;
};

uniform sampler2D SimpleSampledFloatImage2d;

void ComplexImageIntrinsicFunction_PreConstructor(ComplexImageIntrinsicFunction self)
{
}

void ComplexImageIntrinsicFunction_DefaultConstructor(ComplexImageIntrinsicFunction self)
{
    ComplexImageIntrinsicFunction_PreConstructor(self);
}

void Main(ComplexImageIntrinsicFunction self)
{
    vec2 uvs = vec2(0.0);
    float lod = 1.0;
    vec4 value = textureLod(SimpleSampledFloatImage2d, uvs, lod);
}

void main()
{
    ComplexImageIntrinsicFunction self;
    ComplexImageIntrinsicFunction_DefaultConstructor(self);
    Main(self);
}

