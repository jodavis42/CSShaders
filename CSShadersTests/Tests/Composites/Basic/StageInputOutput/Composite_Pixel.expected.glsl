#version 450

struct StageInputOutputTest_Vertex
{
    float FrameTime;
    vec4 Color;
};

struct Vertex0
{
    float FrameTime;
};

struct Vertex1
{
    vec4 Color;
};

struct StageInputOutputTest_Pixel
{
    float FrameTime;
    vec4 Color;
};

struct Pixel0
{
    float FrameTime;
};

struct Pixel1
{
    vec4 Color;
};

layout(location = 0) in StageInputOutputTest_Pixel_Inputs
{
    float FrameTime;
    vec4 Color;
} In;


void StageInputOutputTest_Pixel_PreConstructor(inout StageInputOutputTest_Pixel self)
{
    self.FrameTime = 0.0;
}

void StageInputOutputTest_Pixel_DefaultConstructor(inout StageInputOutputTest_Pixel self)
{
    StageInputOutputTest_Pixel_PreConstructor(self);
}

void StageInputOutputTest_Pixel_CopyInputs(inout StageInputOutputTest_Pixel self)
{
    self.FrameTime = In.FrameTime;
    self.Color = In.Color;
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.FrameTime = 0.0;
}

void Pixel0_DefaultConstructor(inout Pixel0 self)
{
    Pixel0_PreConstructor(self);
}

void Main(Pixel0 self)
{
}

void Pixel1_PreConstructor(Pixel1 self)
{
}

void Pixel1_DefaultConstructor(Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Main(StageInputOutputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    pixel0.FrameTime = self.FrameTime;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.Color = self.Color;
    Main(pixel1);
}

void main()
{
    StageInputOutputTest_Pixel self;
    StageInputOutputTest_Pixel_DefaultConstructor(self);
    StageInputOutputTest_Pixel_CopyInputs(self);
    Main(self);
}

