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

layout(location = 0) out StageInputOutputTest_Vertex_Outputs
{
    float Value1;
    float Value2;
    float Value3;
    float Value5;
    float Value8;
} Out;


void StageInputOutputTest_Vertex_PreConstructor(inout StageInputOutputTest_Vertex self)
{
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value5 = 0.0;
    self.Value8 = 0.0;
}

void StageInputOutputTest_Vertex_DefaultConstructor(inout StageInputOutputTest_Vertex self)
{
    StageInputOutputTest_Vertex_PreConstructor(self);
}

void Vertex_PreConstructor(inout Vertex self)
{
    self.V1 = 0.0;
    self.V23 = 0.0;
    self.V5 = 0.0;
    self.Value7 = 0.0;
    self.V8 = 0.0;
}

void Vertex_DefaultConstructor(inout Vertex self)
{
    Vertex_PreConstructor(self);
}

void Main(Vertex self)
{
}

void Main(inout StageInputOutputTest_Vertex self)
{
    Vertex tempVertex;
    Vertex_DefaultConstructor(tempVertex);
    Vertex vertex = tempVertex;
    Main(vertex);
    self.Value1 = vertex.V1;
    self.Value2 = vertex.V23;
    self.Value3 = vertex.V23;
    self.Value5 = vertex.V5;
    self.Value8 = vertex.V8;
}

void StageInputOutputTest_Vertex_CopyOutputs(StageInputOutputTest_Vertex self)
{
    Out.Value1 = self.Value1;
    Out.Value2 = self.Value2;
    Out.Value3 = self.Value3;
    Out.Value5 = self.Value5;
    Out.Value8 = self.Value8;
}

void main()
{
    StageInputOutputTest_Vertex self;
    StageInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
    StageInputOutputTest_Vertex_CopyOutputs(self);
}

