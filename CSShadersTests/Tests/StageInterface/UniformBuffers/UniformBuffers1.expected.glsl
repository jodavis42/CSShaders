#version 450

struct Buffer
{
    float Value;
    float Value2;
    float Value3;
};

struct UniformBuffers1
{
    Buffer Buffer;
};

layout(std140) uniform Buffer_0
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance;

void Buffer_PreConstructor(inout Buffer self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
}

void Buffer_DefaultConstructor(inout Buffer self)
{
    Buffer_PreConstructor(self);
}

void UniformBuffers1_PreConstructor(inout UniformBuffers1 self)
{
    Buffer tempBuffer;
    Buffer_DefaultConstructor(tempBuffer);
    self.Buffer = tempBuffer;
}

void UniformBuffers1_DefaultConstructor(inout UniformBuffers1 self)
{
    UniformBuffers1_PreConstructor(self);
}

void UniformBuffers1_CopyInputs(inout UniformBuffers1 self)
{
    self.Buffer.Value = Buffer_Instance.Value;
    self.Buffer.Value2 = Buffer_Instance.Value2;
    self.Buffer.Value3 = Buffer_Instance.Value3;
}

void Main(UniformBuffers1 self)
{
}

void main()
{
    UniformBuffers1 self;
    UniformBuffers1_DefaultConstructor(self);
    UniformBuffers1_CopyInputs(self);
    Main(self);
}

