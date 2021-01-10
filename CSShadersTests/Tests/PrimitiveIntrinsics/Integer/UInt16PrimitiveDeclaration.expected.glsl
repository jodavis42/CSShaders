#version 450
#if defined(GL_AMD_gpu_shader_int16)
#extension GL_AMD_gpu_shader_int16 : require
#else
#error No extension available for Int16.
#endif

struct UInt16PrimitiveDeclarationTest
{
    int empty_struct_member;
};

void UInt16PrimitiveDeclarationTest_InitGlobals()
{
}

void UInt16PrimitiveDeclarationTest_PreConstructor(UInt16PrimitiveDeclarationTest self)
{
}

void UInt16PrimitiveDeclarationTest_DefaultConstructor(UInt16PrimitiveDeclarationTest self)
{
    UInt16PrimitiveDeclarationTest_PreConstructor(self);
}

void UInt16PrimitiveDeclarationTest_CopyInputs(UInt16PrimitiveDeclarationTest self)
{
}

void Main(UInt16PrimitiveDeclarationTest self)
{
    uint16_t uint16Val = 0us;
}

void UInt16PrimitiveDeclarationTest_CopyOutputs(UInt16PrimitiveDeclarationTest self)
{
}

void main()
{
    UInt16PrimitiveDeclarationTest_InitGlobals();
    UInt16PrimitiveDeclarationTest self;
    UInt16PrimitiveDeclarationTest_DefaultConstructor(self);
    UInt16PrimitiveDeclarationTest_CopyInputs(self);
    Main(self);
    UInt16PrimitiveDeclarationTest_CopyOutputs(self);
}

