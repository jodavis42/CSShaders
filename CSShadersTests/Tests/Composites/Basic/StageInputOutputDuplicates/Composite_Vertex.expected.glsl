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

layout(location = 0) out StageInputOutputTest_Vertex_Outputs
{
    float FrameTime;
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

void Vertex1_PreConstructor(inout Vertex1 self)
{
    self.FrameTime = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
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
    self.FrameTime = vertex1.FrameTime;
}

void StageInputOutputTest_Vertex_CopyOutputs(StageInputOutputTest_Vertex self)
{
    Out.FrameTime = self.FrameTime;
}

void main()
{
    StageInputOutputTest_Vertex self;
    StageInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
    StageInputOutputTest_Vertex_CopyOutputs(self);
}

