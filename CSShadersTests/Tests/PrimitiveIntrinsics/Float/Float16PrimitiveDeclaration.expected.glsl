#version 450
#if defined(GL_AMD_gpu_shader_half_float)
#extension GL_AMD_gpu_shader_half_float : require
#elif defined(GL_NV_gpu_shader5)
#extension GL_NV_gpu_shader5 : require
#else
#error No extension available for FP16.
#endif

struct Float16PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Float16PrimitiveDeclarationTest_InitGlobals()
{
}

void Float16PrimitiveDeclarationTest_PreConstructor(Float16PrimitiveDeclarationTest self)
{
}

void Float16PrimitiveDeclarationTest_DefaultConstructor(Float16PrimitiveDeclarationTest self)
{
    Float16PrimitiveDeclarationTest_PreConstructor(self);
}

void Float16PrimitiveDeclarationTest_CopyInputs(Float16PrimitiveDeclarationTest self)
{
}

void Main(Float16PrimitiveDeclarationTest self)
{
    float16_t float16Val = float16_t(0.0);
}

void Float16PrimitiveDeclarationTest_CopyOutputs(Float16PrimitiveDeclarationTest self)
{
}

void main()
{
    Float16PrimitiveDeclarationTest_InitGlobals();
    Float16PrimitiveDeclarationTest self;
    Float16PrimitiveDeclarationTest_DefaultConstructor(self);
    Float16PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    Float16PrimitiveDeclarationTest_CopyOutputs(self);
}

