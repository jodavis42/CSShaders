#version 450

struct Buffer
{
    float Value;
    float Value2;
    float Value3;
};

struct UniformBuffers3
{
    Buffer Buffer00;
    Buffer Buffer01;
    Buffer Buffer21;
};

layout(std140) uniform Buffer_0
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance;

layout(std140) uniform Buffer_1
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance_1;

layout(std140) uniform Buffer_2
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance_2;

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

void UniformBuffers3_PreConstructor(inout UniformBuffers3 self)
{
    Buffer tempBuffer;
    Buffer_DefaultConstructor(tempBuffer);
    self.Buffer00 = tempBuffer;
    Buffer tempBuffer_1;
    Buffer_DefaultConstructor(tempBuffer_1);
    self.Buffer01 = tempBuffer_1;
    Buffer tempBuffer_2;
    Buffer_DefaultConstructor(tempBuffer_2);
    self.Buffer21 = tempBuffer_2;
}

void UniformBuffers3_DefaultConstructor(inout UniformBuffers3 self)
{
    UniformBuffers3_PreConstructor(self);
}

void UniformBuffers3_CopyInputs(inout UniformBuffers3 self)
{
    self.Buffer00.Value = Buffer_Instance.Value;
    self.Buffer00.Value2 = Buffer_Instance.Value2;
    self.Buffer00.Value3 = Buffer_Instance.Value3;
    self.Buffer21.Value = Buffer_Instance_1.Value;
    self.Buffer21.Value2 = Buffer_Instance_1.Value2;
    self.Buffer21.Value3 = Buffer_Instance_1.Value3;
    self.Buffer01.Value = Buffer_Instance_2.Value;
    self.Buffer01.Value2 = Buffer_Instance_2.Value2;
    self.Buffer01.Value3 = Buffer_Instance_2.Value3;
}

void Main(UniformBuffers3 self)
{
}

void main()
{
    UniformBuffers3 self;
    UniformBuffers3_DefaultConstructor(self);
    UniformBuffers3_CopyInputs(self);
    Main(self);
}

