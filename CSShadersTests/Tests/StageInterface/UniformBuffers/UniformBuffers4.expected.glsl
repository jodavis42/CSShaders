#version 450

struct Buffer
{
    float Value;
    float Value2;
    float Value3;
};

struct UniformBuffers4
{
    Buffer Buffer01;
    Buffer Buffer10;
    Buffer Buffer00;
    Buffer Buffer02;
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

layout(std140) uniform Buffer_3
{
    float Value;
    float Value2;
    float Value3;
} Buffer_Instance_3;

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

void UniformBuffers4_PreConstructor(inout UniformBuffers4 self)
{
    Buffer tempBuffer;
    Buffer_DefaultConstructor(tempBuffer);
    self.Buffer01 = tempBuffer;
    Buffer tempBuffer_1;
    Buffer_DefaultConstructor(tempBuffer_1);
    self.Buffer10 = tempBuffer_1;
    Buffer tempBuffer_2;
    Buffer_DefaultConstructor(tempBuffer_2);
    self.Buffer00 = tempBuffer_2;
    Buffer tempBuffer_3;
    Buffer_DefaultConstructor(tempBuffer_3);
    self.Buffer02 = tempBuffer_3;
}

void UniformBuffers4_DefaultConstructor(inout UniformBuffers4 self)
{
    UniformBuffers4_PreConstructor(self);
}

void UniformBuffers4_CopyInputs(inout UniformBuffers4 self)
{
    self.Buffer01.Value = Buffer_Instance.Value;
    self.Buffer01.Value2 = Buffer_Instance.Value2;
    self.Buffer01.Value3 = Buffer_Instance.Value3;
    self.Buffer10.Value = Buffer_Instance_1.Value;
    self.Buffer10.Value2 = Buffer_Instance_1.Value2;
    self.Buffer10.Value3 = Buffer_Instance_1.Value3;
    self.Buffer00.Value = Buffer_Instance_2.Value;
    self.Buffer00.Value2 = Buffer_Instance_2.Value2;
    self.Buffer00.Value3 = Buffer_Instance_2.Value3;
    self.Buffer02.Value = Buffer_Instance_3.Value;
    self.Buffer02.Value2 = Buffer_Instance_3.Value2;
    self.Buffer02.Value3 = Buffer_Instance_3.Value3;
}

void Main(UniformBuffers4 self)
{
}

void main()
{
    UniformBuffers4 self;
    UniformBuffers4_DefaultConstructor(self);
    UniformBuffers4_CopyInputs(self);
    Main(self);
}

