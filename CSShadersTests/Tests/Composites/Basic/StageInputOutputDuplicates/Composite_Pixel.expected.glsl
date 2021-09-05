#version 450

struct StageInputOutputTest_Vertex
{
    float FrameTime;
};

struct Vertex0
{
    float FrameTime;
};

struct Vertex1
{
    float FrameTime;
};

struct StageInputOutputTest_Pixel
{
    float FrameTime;
};

struct Pixel0
{
    float FrameTime;
};

struct Pixel1
{
    float FrameTime;
};

layout(location = 0) in StageInputOutputTest_Pixel_Inputs
{
    float FrameTime;
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

void Pixel1_PreConstructor(inout Pixel1 self)
{
    self.FrameTime = 0.0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
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
    pixel1.FrameTime = self.FrameTime;
    Main(pixel1);
}

void main()
{
    StageInputOutputTest_Pixel self;
    StageInputOutputTest_Pixel_DefaultConstructor(self);
    StageInputOutputTest_Pixel_CopyInputs(self);
    Main(self);
}

