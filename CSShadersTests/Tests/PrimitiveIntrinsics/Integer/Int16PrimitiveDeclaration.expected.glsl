#version 450
#if defined(GL_AMD_gpu_shader_int16)
#extension GL_AMD_gpu_shader_int16 : require
#else
#error No extension available for Int16.
#endif

struct Int16PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void Int16PrimitiveDeclarationTest_InitGlobals()
{
}

void Int16PrimitiveDeclarationTest_PreConstructor(Int16PrimitiveDeclarationTest self)
{
}

void Int16PrimitiveDeclarationTest_DefaultConstructor(Int16PrimitiveDeclarationTest self)
{
    Int16PrimitiveDeclarationTest_PreConstructor(self);
}

void Int16PrimitiveDeclarationTest_CopyInputs(Int16PrimitiveDeclarationTest self)
{
}

void Main(Int16PrimitiveDeclarationTest self)
{
    int16_t int16Val = 0s;
}

void Int16PrimitiveDeclarationTest_CopyOutputs(Int16PrimitiveDeclarationTest self)
{
}

void main()
{
    Int16PrimitiveDeclarationTest_InitGlobals();
    Int16PrimitiveDeclarationTest self;
    Int16PrimitiveDeclarationTest_DefaultConstructor(self);
    Int16PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    Int16PrimitiveDeclarationTest_CopyOutputs(self);
}

