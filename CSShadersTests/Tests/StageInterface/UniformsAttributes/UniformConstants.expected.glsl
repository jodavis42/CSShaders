#version 450

struct DescriptorSetBindingIds
{
    int empty_struct_member;
};

layout(binding = 0) uniform sampler2D SampledImage0;
layout(binding = 0) uniform sampler2D SampledImage1;
layout(binding = 1) uniform sampler2D SampledImage2;
layout(binding = 1) uniform sampler2D SampledImage3;

void DescriptorSetBindingIds_InitGlobals()
{
}

void DescriptorSetBindingIds_PreConstructor(DescriptorSetBindingIds self)
{
}

void DescriptorSetBindingIds_DefaultConstructor(DescriptorSetBindingIds self)
{
    DescriptorSetBindingIds_PreConstructor(self);
}

void DescriptorSetBindingIds_CopyInputs(DescriptorSetBindingIds self)
{
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

