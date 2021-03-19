#version 450

struct FloatBuffer
{
    float Value;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
};

struct UniformBufferLayouts1
{
    FloatBuffer FloatBuffer;
};

layout(std140) uniform FloatBuffer_0
{
    float Value;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
} FloatBuffer_Instance;

void FloatBuffer_PreConstructor(inout FloatBuffer self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
    self.Value4 = 1.0;
    self.Value5 = 1.0;
}

void FloatBuffer_DefaultConstructor(inout FloatBuffer self)
{
    FloatBuffer_PreConstructor(self);
}

void UniformBufferLayouts1_PreConstructor(inout UniformBufferLayouts1 self)
{
    FloatBuffer tempFloatBuffer;
    FloatBuffer_DefaultConstructor(tempFloatBuffer);
    self.FloatBuffer = tempFloatBuffer;
}

void UniformBufferLayouts1_DefaultConstructor(inout UniformBufferLayouts1 self)
{
    UniformBufferLayouts1_PreConstructor(self);
}

void UniformBufferLayouts1_CopyInputs(inout UniformBufferLayouts1 self)
{
    self.FloatBuffer.Value = FloatBuffer_Instance.Value;
    self.FloatBuffer.Value2 = FloatBuffer_Instance.Value2;
    self.FloatBuffer.Value3 = FloatBuffer_Instance.Value3;
    self.FloatBuffer.Value4 = FloatBuffer_Instance.Value4;
    self.FloatBuffer.Value5 = FloatBuffer_Instance.Value5;
}

void Main(UniformBufferLayouts1 self)
{
}

void main()
{
    UniformBufferLayouts1 self;
    UniformBufferLayouts1_DefaultConstructor(self);
    UniformBufferLayouts1_CopyInputs(self);
    Main(self);
}

