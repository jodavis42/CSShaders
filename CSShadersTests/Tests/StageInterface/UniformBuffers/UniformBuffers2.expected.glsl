#version 450

struct Buffer1
{
    float Value;
    float Value2;
    float Value3;
};

struct Buffer2
{
    float Value;
    float Value2;
    float Value3;
};

struct UniformBuffers2
{
    Buffer1 Buffer1;
    Buffer2 Buffer2;
};

layout(std140) uniform Buffer1_0
{
    float Value;
    float Value2;
    float Value3;
} Buffer1_Instance;

layout(std140) uniform Buffer2_0
{
    float Value;
    float Value2;
    float Value3;
} Buffer2_Instance;

void Buffer1_PreConstructor(inout Buffer1 self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
}

void Buffer1_DefaultConstructor(inout Buffer1 self)
{
    Buffer1_PreConstructor(self);
}

void Buffer2_PreConstructor(inout Buffer2 self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    self.Value3 = 1.0;
}

void Buffer2_DefaultConstructor(inout Buffer2 self)
{
    Buffer2_PreConstructor(self);
}

void UniformBuffers2_PreConstructor(inout UniformBuffers2 self)
{
    Buffer1 tempBuffer1;
    Buffer1_DefaultConstructor(tempBuffer1);
    self.Buffer1 = tempBuffer1;
    Buffer2 tempBuffer2;
    Buffer2_DefaultConstructor(tempBuffer2);
    self.Buffer2 = tempBuffer2;
}

void UniformBuffers2_DefaultConstructor(inout UniformBuffers2 self)
{
    UniformBuffers2_PreConstructor(self);
}

void UniformBuffers2_CopyInputs(inout UniformBuffers2 self)
{
    self.Buffer1.Value = Buffer1_Instance.Value;
    self.Buffer1.Value2 = Buffer1_Instance.Value2;
    self.Buffer1.Value3 = Buffer1_Instance.Value3;
    self.Buffer2.Value = Buffer2_Instance.Value;
    self.Buffer2.Value2 = Buffer2_Instance.Value2;
    self.Buffer2.Value3 = Buffer2_Instance.Value3;
}

void Main(UniformBuffers2 self)
{
}

void main()
{
    UniformBuffers2 self;
    UniformBuffers2_DefaultConstructor(self);
    UniformBuffers2_CopyInputs(self);
    Main(self);
}

