#version 450

struct StageInputOutputTest_Vertex
{
    float Value1;
    float Value2;
    float Value3;
    float Value5;
    float Value8;
};

struct Vertex
{
    float V1;
    float V23;
    float V5;
    vec2 V6;
    float Value7;
    float V8;
};

struct StageInputOutputTest_Pixel
{
    float Value1;
    float Value2;
    float Value3;
    float Value5;
    float Value8;
};

struct Pixel
{
    float P1;
    float P2;
    float P3;
    float V45;
    float P6;
    float Value7;
    float P8;
};

layout(location = 0) in StageInputOutputTest_Pixel_Inputs
{
    float Value1;
    float Value2;
    float Value3;
    float Value5;
    float Value8;
} In;


void StageInputOutputTest_Pixel_PreConstructor(inout StageInputOutputTest_Pixel self)
{
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value5 = 0.0;
    self.Value8 = 0.0;
}

void StageInputOutputTest_Pixel_DefaultConstructor(inout StageInputOutputTest_Pixel self)
{
    StageInputOutputTest_Pixel_PreConstructor(self);
}

void StageInputOutputTest_Pixel_CopyInputs(inout StageInputOutputTest_Pixel self)
{
    self.Value1 = In.Value1;
    self.Value2 = In.Value2;
    self.Value3 = In.Value3;
    self.Value5 = In.Value5;
    self.Value8 = In.Value8;
}

void Pixel_PreConstructor(inout Pixel self)
{
    self.P1 = 0.0;
    self.P2 = 0.0;
    self.P3 = 0.0;
    self.V45 = 0.0;
    self.P6 = 0.0;
    self.Value7 = 0.0;
    self.P8 = 0.0;
}

void Pixel_DefaultConstructor(inout Pixel self)
{
    Pixel_PreConstructor(self);
}

void Main(Pixel self)
{
}

void Main(StageInputOutputTest_Pixel self)
{
    Pixel tempPixel;
    Pixel_DefaultConstructor(tempPixel);
    Pixel pixel = tempPixel;
    pixel.P1 = self.Value1;
    pixel.P2 = self.Value2;
    pixel.P3 = self.Value3;
    pixel.V45 = self.Value5;
    pixel.P8 = self.Value8;
    Main(pixel);
}

void main()
{
    StageInputOutputTest_Pixel self;
    StageInputOutputTest_Pixel_DefaultConstructor(self);
    StageInputOutputTest_Pixel_CopyInputs(self);
    Main(self);
}

