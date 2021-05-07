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

layout(location = 0) out StageInputOutputTest_Vertex_Outputs
{
    float FrameTime;
    vec4 Color;
} Out;


void StageInputOutputTest_Vertex_PreConstructor(inout StageInputOutputTest_Vertex self)
{
    self.FrameTime = 0.0;
}

void StageInputOutputTest_Vertex_DefaultConstructor(inout StageInputOutputTest_Vertex self)
{
    StageInputOutputTest_Vertex_PreConstructor(self);
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.FrameTime = 0.0;
}

void Vertex0_DefaultConstructor(inout Vertex0 self)
{
    Vertex0_PreConstructor(self);
}

void Main(Vertex0 self)
{
}

void Vertex1_PreConstructor(Vertex1 self)
{
}

void Vertex1_DefaultConstructor(Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Main(inout StageInputOutputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    Main(vertex1);
    self.FrameTime = vertex0.FrameTime;
    self.Color = vertex1.Color;
}

void StageInputOutputTest_Vertex_CopyOutputs(StageInputOutputTest_Vertex self)
{
    Out.FrameTime = self.FrameTime;
    Out.Color = self.Color;
}

void main()
{
    StageInputOutputTest_Vertex self;
    StageInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
    StageInputOutputTest_Vertex_CopyOutputs(self);
}

