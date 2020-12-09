#version 450

struct TestIntrinsics
{
    int empty_struct_member;
};

struct SinExtensionIntrinsic
{
    int empty_struct_member;
};

void SinExtensionIntrinsic_InitGlobals()
{
}

void SinExtensionIntrinsic_PreConstructor(SinExtensionIntrinsic self)
{
}

void SinExtensionIntrinsic_DefaultConstructor(SinExtensionIntrinsic self)
{
    SinExtensionIntrinsic_PreConstructor(self);
}

void SinExtensionIntrinsic_CopyInputs(SinExtensionIntrinsic self)
{
}

void Main(SinExtensionIntrinsic self)
{
    float floatResult = sin(1.0);
    vec3 float3Reslt = sin(vec3(0.0));
}

void SinExtensionIntrinsic_CopyOutputs(SinExtensionIntrinsic self)
{
}

void main()
{
    SinExtensionIntrinsic_InitGlobals();
    SinExtensionIntrinsic self;
    SinExtensionIntrinsic_DefaultConstructor(self);
    SinExtensionIntrinsic_CopyInputs(self);
    Main(self);
    SinExtensionIntrinsic_CopyOutputs(self);
}

