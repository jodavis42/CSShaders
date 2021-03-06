#version 450

struct TestStruct
{
    vec2 V2;
    float Value;
};

struct Vertex
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
};

layout(std140) uniform Vertex_MaterialBuffer_0_0
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
} Vertex_MaterialBuffer_0_0_Instance;

void TestStruct_PreConstructor(inout TestStruct self)
{
    self.Value = 0.0;
}

void TestStruct_DefaultConstructor(inout TestStruct self)
{
    TestStruct_PreConstructor(self);
}

void Vertex_PreConstructor(inout Vertex self)
{
    self.Value = 1.0;
    self.Value2 = 1.0;
    TestStruct tempTestStruct;
    TestStruct_DefaultConstructor(tempTestStruct);
    self.TestStruct = tempTestStruct;
}

void Vertex_DefaultConstructor(inout Vertex self)
{
    Vertex_PreConstructor(self);
}

void Vertex_CopyInputs(inout Vertex self)
{
    self.Value = Vertex_MaterialBuffer_0_0_Instance.Value;
    self.Value2 = Vertex_MaterialBuffer_0_0_Instance.Value2;
    self.V2 = Vertex_MaterialBuffer_0_0_Instance.V2;
    self.TestStruct = Vertex_MaterialBuffer_0_0_Instance.TestStruct;
}

void Main(Vertex self)
{
}

void main()
{
    Vertex self;
    Vertex_DefaultConstructor(self);
    Vertex_CopyInputs(self);
    Main(self);
}

