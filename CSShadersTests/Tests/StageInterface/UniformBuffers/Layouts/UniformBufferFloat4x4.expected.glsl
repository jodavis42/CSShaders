#version 450

struct Float4x4Buffer0
{
    mat4 Float4x4_0;
    mat4 Float4x4_1;
};

struct Float4x4Buffer1
{
    float Value0;
    mat4 Float4x4;
};

struct UniformBufferFloat4x4Layouts
{
    Float4x4Buffer0 Float4x4Buffer0;
    Float4x4Buffer1 Float4x4Buffer1;
};

layout(std140) uniform Float4x4Buffer0_0
{
    mat4 Float4x4_0;
    mat4 Float4x4_1;
} Float4x4Buffer0_Instance;

layout(std140) uniform Float4x4Buffer1_0
{
    layout(offset = 0) float Value0;
    layout(offset = 64) mat4 Float4x4;
} Float4x4Buffer1_Instance;

void Float4x4Buffer0_PreConstructor(Float4x4Buffer0 self)
{
}

void Float4x4Buffer0_DefaultConstructor(Float4x4Buffer0 self)
{
    Float4x4Buffer0_PreConstructor(self);
}

void Float4x4Buffer1_PreConstructor(inout Float4x4Buffer1 self)
{
    self.Value0 = 0.0;
}

void Float4x4Buffer1_DefaultConstructor(inout Float4x4Buffer1 self)
{
    Float4x4Buffer1_PreConstructor(self);
}

void UniformBufferFloat4x4Layouts_PreConstructor(inout UniformBufferFloat4x4Layouts self)
{
    Float4x4Buffer0 tempFloat4x4Buffer0;
    Float4x4Buffer0_DefaultConstructor(tempFloat4x4Buffer0);
    self.Float4x4Buffer0 = tempFloat4x4Buffer0;
    Float4x4Buffer1 tempFloat4x4Buffer1;
    Float4x4Buffer1_DefaultConstructor(tempFloat4x4Buffer1);
    self.Float4x4Buffer1 = tempFloat4x4Buffer1;
}

void UniformBufferFloat4x4Layouts_DefaultConstructor(inout UniformBufferFloat4x4Layouts self)
{
    UniformBufferFloat4x4Layouts_PreConstructor(self);
}

void UniformBufferFloat4x4Layouts_CopyInputs(inout UniformBufferFloat4x4Layouts self)
{
    self.Float4x4Buffer0.Float4x4_0 = Float4x4Buffer0_Instance.Float4x4_0;
    self.Float4x4Buffer0.Float4x4_1 = Float4x4Buffer0_Instance.Float4x4_1;
    self.Float4x4Buffer1.Value0 = Float4x4Buffer1_Instance.Value0;
    self.Float4x4Buffer1.Float4x4 = Float4x4Buffer1_Instance.Float4x4;
}

void Main(UniformBufferFloat4x4Layouts self)
{
}

void main()
{
    UniformBufferFloat4x4Layouts self;
    UniformBufferFloat4x4Layouts_DefaultConstructor(self);
    UniformBufferFloat4x4Layouts_CopyInputs(self);
    Main(self);
}

