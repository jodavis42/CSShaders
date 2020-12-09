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

void ComplexImageIntrinsicFunction_InitGlobals()
{
}

void ComplexImageIntrinsicFunction_PreConstructor(ComplexImageIntrinsicFunction self)
{
}

void ComplexImageIntrinsicFunction_DefaultConstructor(ComplexImageIntrinsicFunction self)
{
    ComplexImageIntrinsicFunction_PreConstructor(self);
}

void ComplexImageIntrinsicFunction_CopyInputs(ComplexImageIntrinsicFunction self)
{
}

void Main(ComplexImageIntrinsicFunction self)
{
    vec2 uvs = vec2(0.0);
    float lod = 1.0;
    vec4 value = textureLod(SimpleSampledFloatImage2d, uvs, lod);
}

void ComplexImageIntrinsicFunction_CopyOutputs(ComplexImageIntrinsicFunction self)
{
}

void main()
{
    ComplexImageIntrinsicFunction_InitGlobals();
    ComplexImageIntrinsicFunction self;
    ComplexImageIntrinsicFunction_DefaultConstructor(self);
    ComplexImageIntrinsicFunction_CopyInputs(self);
    Main(self);
    ComplexImageIntrinsicFunction_CopyOutputs(self);
}

