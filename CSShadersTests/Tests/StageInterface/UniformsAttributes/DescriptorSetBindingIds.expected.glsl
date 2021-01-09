#version 450

struct DescriptorSetBindingIds
{
    float Value;
    float Value2;
    float Value3;
};

layout(std140) uniform DescriptorSetBindingIds_MaterialBuffer_1_0
{
    float Value;
} DescriptorSetBindingIds_MaterialBuffer_1_0_Instance;

layout(std140) uniform DescriptorSetBindingIds_MaterialBuffer_0_1
{
    float Value2;
} DescriptorSetBindingIds_MaterialBuffer_0_1_Instance;

layout(std140) uniform DescriptorSetBindingIds_MaterialBuffer_1_1
{
    float Value3;
} DescriptorSetBindingIds_MaterialBuffer_1_1_Instance;

void DescriptorSetBindingIds_InitGlobals()
{
}

void DescriptorSetBindingIds_PreConstructor(inout DescriptorSetBindingIds self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
}

void DescriptorSetBindingIds_DefaultConstructor(inout DescriptorSetBindingIds self)
{
    DescriptorSetBindingIds_PreConstructor(self);
}

void DescriptorSetBindingIds_CopyInputs(inout DescriptorSetBindingIds self)
{
    self.Value = DescriptorSetBindingIds_MaterialBuffer_1_0_Instance.Value;
    self.Value2 = DescriptorSetBindingIds_MaterialBuffer_0_1_Instance.Value2;
    self.Value3 = DescriptorSetBindingIds_MaterialBuffer_1_1_Instance.Value3;
}

void Main(DescriptorSetBindingIds self)
{
}

void DescriptorSetBindingIds_CopyOutputs(DescriptorSetBindingIds self)
{
}

void main()
{
    DescriptorSetBindingIds_InitGlobals();
    DescriptorSetBindingIds self;
    DescriptorSetBindingIds_DefaultConstructor(self);
    DescriptorSetBindingIds_CopyInputs(self);
    Main(self);
    DescriptorSetBindingIds_CopyOutputs(self);
}

