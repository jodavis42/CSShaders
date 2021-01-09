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

struct Pixel_Outputs
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
};

layout(location = 0) out Pixel_Outputs Out;

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

void Pixel_CopyInputs(Pixel self)
{
}

void Main(Pixel self)
{
}

void Pixel_CopyOutputs(Pixel self)
{
    Out.Value = self.Value;
    Out.Value2 = self.Value2;
    Out.V2 = self.V2;
    Out.TestStruct = self.TestStruct;
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

