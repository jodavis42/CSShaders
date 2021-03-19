#version 450

struct Float2x2Buffer0
{
    mat2 Float2x22_0;
    mat2 Float2x22_1;
};

struct Float2x2Buffer1
{
    float Value0;
    mat2 Float2x2;
};

struct Float2x2Buffer2
{
    float Value0;
    float Value1;
    mat2 Float2x2;
};

struct UniformBufferFloat2x2Layouts
{
    Float2x2Buffer0 Float2x2Buffer0;
    Float2x2Buffer1 Float2x2Buffer1;
    Float2x2Buffer2 Float2x2Buffer2;
};

layout(std140) uniform Float2x2Buffer0_0
{
    mat2 Float2x22_0;
    mat2 Float2x22_1;
} Float2x2Buffer0_Instance;

layout(std140) uniform Float2x2Buffer1_0
{
    layout(offset = 0) float Value0;
    layout(offset = 32) mat2 Float2x2;
} Float2x2Buffer1_Instance;

layout(std140) uniform Float2x2Buffer2_0
{
    layout(offset = 0) float Value0;
    layout(offset = 4) float Value1;
    layout(offset = 32) mat2 Float2x2;
} Float2x2Buffer2_Instance;

void Float2x2Buffer0_PreConstructor(Float2x2Buffer0 self)
{
}

void Float2x2Buffer0_DefaultConstructor(Float2x2Buffer0 self)
{
    Float2x2Buffer0_PreConstructor(self);
}

void Float2x2Buffer1_PreConstructor(inout Float2x2Buffer1 self)
{
    self.Value0 = 0.0;
}

void Float2x2Buffer1_DefaultConstructor(inout Float2x2Buffer1 self)
{
    Float2x2Buffer1_PreConstructor(self);
}

void Float2x2Buffer2_PreConstructor(inout Float2x2Buffer2 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
}

void Float2x2Buffer2_DefaultConstructor(inout Float2x2Buffer2 self)
{
    Float2x2Buffer2_PreConstructor(self);
}

void UniformBufferFloat2x2Layouts_PreConstructor(inout UniformBufferFloat2x2Layouts self)
{
    Float2x2Buffer0 tempFloat2x2Buffer0;
    Float2x2Buffer0_DefaultConstructor(tempFloat2x2Buffer0);
    self.Float2x2Buffer0 = tempFloat2x2Buffer0;
    Float2x2Buffer1 tempFloat2x2Buffer1;
    Float2x2Buffer1_DefaultConstructor(tempFloat2x2Buffer1);
    self.Float2x2Buffer1 = tempFloat2x2Buffer1;
    Float2x2Buffer2 tempFloat2x2Buffer2;
    Float2x2Buffer2_DefaultConstructor(tempFloat2x2Buffer2);
    self.Float2x2Buffer2 = tempFloat2x2Buffer2;
}

void UniformBufferFloat2x2Layouts_DefaultConstructor(inout UniformBufferFloat2x2Layouts self)
{
    UniformBufferFloat2x2Layouts_PreConstructor(self);
}

void UniformBufferFloat2x2Layouts_CopyInputs(inout UniformBufferFloat2x2Layouts self)
{
    self.Float2x2Buffer0.Float2x22_0 = Float2x2Buffer0_Instance.Float2x22_0;
    self.Float2x2Buffer0.Float2x22_1 = Float2x2Buffer0_Instance.Float2x22_1;
    self.Float2x2Buffer1.Value0 = Float2x2Buffer1_Instance.Value0;
    self.Float2x2Buffer1.Float2x2 = Float2x2Buffer1_Instance.Float2x2;
    self.Float2x2Buffer2.Value0 = Float2x2Buffer2_Instance.Value0;
    self.Float2x2Buffer2.Value1 = Float2x2Buffer2_Instance.Value1;
    self.Float2x2Buffer2.Float2x2 = Float2x2Buffer2_Instance.Float2x2;
}

void Main(UniformBufferFloat2x2Layouts self)
{
}

void main()
{
    UniformBufferFloat2x2Layouts self;
    UniformBufferFloat2x2Layouts_DefaultConstructor(self);
    UniformBufferFloat2x2Layouts_CopyInputs(self);
    Main(self);
}

