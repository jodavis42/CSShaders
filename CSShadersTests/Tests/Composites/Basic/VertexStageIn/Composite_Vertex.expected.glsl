#version 450

struct StageInputOutputTest_Vertex
{
    vec2 Uv;
    vec3 LocalPosition;
};

struct Vertex
{
    vec2 Uv;
    vec3 LocalPosition;
};

struct StageInputOutputTest_Pixel
{
    int empty_struct_member;
};

layout(location = 0) in vec2 Uv;
layout(location = 2) in vec3 LocalPosition;

void StageInputOutputTest_Vertex_PreConstructor(StageInputOutputTest_Vertex self)
{
}

void StageInputOutputTest_Vertex_DefaultConstructor(StageInputOutputTest_Vertex self)
{
    StageInputOutputTest_Vertex_PreConstructor(self);
}

void StageInputOutputTest_Vertex_CopyInputs(inout StageInputOutputTest_Vertex self)
{
    self.Uv = Uv;
    self.LocalPosition = LocalPosition;
}

void Vertex_PreConstructor(Vertex self)
{
}

void Vertex_DefaultConstructor(Vertex self)
{
    Vertex_PreConstructor(self);
}

void Main(Vertex self)
{
}

void Main(StageInputOutputTest_Vertex self)
{
    Vertex tempVertex;
    Vertex_DefaultConstructor(tempVertex);
    Vertex vertex = tempVertex;
    vertex.Uv = self.Uv;
    vertex.LocalPosition = self.LocalPosition;
    Main(vertex);
}

void main()
{
    StageInputOutputTest_Vertex self;
    StageInputOutputTest_Vertex_DefaultConstructor(self);
    StageInputOutputTest_Vertex_CopyInputs(self);
    Main(self);
}

