#version 450

struct TestStruct
{
    vec2 V2;
    float Value;
};

struct Pixel
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
};

layout(std140) uniform Pixel_MaterialBuffer_0_0
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
} Pixel_MaterialBuffer_0_0_Instance;

void Pixel_InitGlobals()
{
}

void TestStruct_PreConstructor(inout TestStruct self)
{
    self.Value = 0.0;
}

void TestStruct_DefaultConstructor(inout TestStruct self)
{
    TestStruct_PreConstructor(self);
}

void Pixel_PreConstructor(inout Pixel self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    TestStruct tempTestStruct;
    TestStruct_DefaultConstructor(tempTestStruct);
    self.TestStruct = tempTestStruct;
}

void Pixel_DefaultConstructor(inout Pixel self)
{
    Pixel_PreConstructor(self);
}

void Pixel_CopyInputs(inout Pixel self)
{
    self.Value = Pixel_MaterialBuffer_0_0_Instance.Value;
    self.Value2 = Pixel_MaterialBuffer_0_0_Instance.Value2;
    self.V2 = Pixel_MaterialBuffer_0_0_Instance.V2;
    self.TestStruct = Pixel_MaterialBuffer_0_0_Instance.TestStruct;
}

void Main(Pixel self)
{
}

void Pixel_CopyOutputs(Pixel self)
{
}

void main()
{
    Pixel_InitGlobals();
    Pixel self;
    Pixel_DefaultConstructor(self);
    Pixel_CopyInputs(self);
    Main(self);
    Pixel_CopyOutputs(self);
}

