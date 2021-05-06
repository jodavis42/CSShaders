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

layout(location = 0) out float Value;
layout(location = 1) out float Value2;
layout(location = 2) out vec2 V2;
layout(location = 3) out TestStruct TestStruct_1;

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

void Main(Pixel self)
{
}

void Pixel_CopyOutputs(Pixel self)
{
    Value = self.Value;
    Value2 = self.Value2;
    V2 = self.V2;
    TestStruct_1 = self.TestStruct;
}

void main()
{
    Pixel self;
    Pixel_DefaultConstructor(self);
    Main(self);
    Pixel_CopyOutputs(self);
}

