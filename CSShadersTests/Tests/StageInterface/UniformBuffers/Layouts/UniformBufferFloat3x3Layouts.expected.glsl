#version 450

struct Float3x3Buffer0
{
    mat3 Float3x3_0;
    mat3 Float3x3_1;
};

struct Float3x3Buffer1
{
    float Value0;
    mat3 Float3x3;
};

struct UniformBufferFloat3x3Layouts
{
    Float3x3Buffer0 Float3x3Buffer0;
    Float3x3Buffer1 Float3x3Buffer1;
};

layout(std140) uniform Float3x3Buffer0_0
{
    layout(offset = 0) mat3 Float3x3_0;
    layout(offset = 64) mat3 Float3x3_1;
} Float3x3Buffer0_Instance;

layout(std140) uniform Float3x3Buffer1_0
{
    layout(offset = 0) float Value0;
    layout(offset = 64) mat3 Float3x3;
} Float3x3Buffer1_Instance;

void Float3x3Buffer0_PreConstructor(Float3x3Buffer0 self)
{
}

void Float3x3Buffer0_DefaultConstructor(Float3x3Buffer0 self)
{
    Float3x3Buffer0_PreConstructor(self);
}

void Float3x3Buffer1_PreConstructor(inout Float3x3Buffer1 self)
{
    self.Value0 = 0.0;
}

void Float3x3Buffer1_DefaultConstructor(inout Float3x3Buffer1 self)
{
    Float3x3Buffer1_PreConstructor(self);
}

void UniformBufferFloat3x3Layouts_PreConstructor(inout UniformBufferFloat3x3Layouts self)
{
    Float3x3Buffer0 tempFloat3x3Buffer0;
    Float3x3Buffer0_DefaultConstructor(tempFloat3x3Buffer0);
    self.Float3x3Buffer0 = tempFloat3x3Buffer0;
    Float3x3Buffer1 tempFloat3x3Buffer1;
    Float3x3Buffer1_DefaultConstructor(tempFloat3x3Buffer1);
    self.Float3x3Buffer1 = tempFloat3x3Buffer1;
}

void UniformBufferFloat3x3Layouts_DefaultConstructor(inout UniformBufferFloat3x3Layouts self)
{
    UniformBufferFloat3x3Layouts_PreConstructor(self);
}

void UniformBufferFloat3x3Layouts_CopyInputs(inout UniformBufferFloat3x3Layouts self)
{
    self.Float3x3Buffer0.Float3x3_0 = Float3x3Buffer0_Instance.Float3x3_0;
    self.Float3x3Buffer0.Float3x3_1 = Float3x3Buffer0_Instance.Float3x3_1;
    self.Float3x3Buffer1.Value0 = Float3x3Buffer1_Instance.Value0;
    self.Float3x3Buffer1.Float3x3 = Float3x3Buffer1_Instance.Float3x3;
}

void Main(UniformBufferFloat3x3Layouts self)
{
}

void main()
{
    UniformBufferFloat3x3Layouts self;
    UniformBufferFloat3x3Layouts_DefaultConstructor(self);
    UniformBufferFloat3x3Layouts_CopyInputs(self);
    Main(self);
}

