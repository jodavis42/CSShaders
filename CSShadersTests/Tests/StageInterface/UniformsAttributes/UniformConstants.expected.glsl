#version 450

struct DescriptorSetBindingIds
{
    int empty_struct_member;
};

layout(binding = 0) uniform sampler2D SampledImage0;
layout(binding = 0) uniform sampler2D SampledImage1;
layout(binding = 1) uniform sampler2D SampledImage2;
layout(binding = 1) uniform sampler2D SampledImage3;

void DescriptorSetBindingIds_PreConstructor(DescriptorSetBindingIds self)
{
}

void DescriptorSetBindingIds_DefaultConstructor(DescriptorSetBindingIds self)
{
    DescriptorSetBindingIds_PreConstructor(self);
}

void Main(DescriptorSetBindingIds self)
{
}

void main()
{
    DescriptorSetBindingIds self;
    DescriptorSetBindingIds_DefaultConstructor(self);
    Main(self);
}

