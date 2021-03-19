#version 450

struct Buffer
{
    float Value;
    float Value2;
    float Value3;
};

struct UniformBuffers5
{
    Buffer Buffer;
    float Value;
    float Value2;
    float Value3;
};

layout(std140) uniform Buffer_0
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance;

layout(std140) uniform UniformBuffers5_SharedMaterialBuffer
{
    float Value;
    float Value2;
    float Value3;
} UniformBuffers5_SharedMaterialBuffer_Instance;

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

void UniformBuffers5_PreConstructor(inout UniformBuffers5 self)
{
    Buffer tempBuffer;
    Buffer_DefaultConstructor(tempBuffer);
    self.Buffer = tempBuffer;
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
}

void UniformBuffers5_DefaultConstructor(inout UniformBuffers5 self)
{
    UniformBuffers5_PreConstructor(self);
}

void UniformBuffers5_CopyInputs(inout UniformBuffers5 self)
{
    self.Buffer.Value = Buffer_Instance.Value;
    self.Buffer.Value2 = Buffer_Instance.Value2;
    self.Buffer.Value3 = Buffer_Instance.Value3;
    self.Value = UniformBuffers5_SharedMaterialBuffer_Instance.Value;
    self.Value2 = UniformBuffers5_SharedMaterialBuffer_Instance.Value2;
    self.Value3 = UniformBuffers5_SharedMaterialBuffer_Instance.Value3;
}

void Main(UniformBuffers5 self)
{
}

void main()
{
    UniformBuffers5 self;
    UniformBuffers5_DefaultConstructor(self);
    UniformBuffers5_CopyInputs(self);
    Main(self);
}

