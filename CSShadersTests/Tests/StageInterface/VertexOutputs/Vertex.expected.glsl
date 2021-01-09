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

layout(location = 0) out Vertex_Outputs
{
    float Value;
    float Value2;
    vec2 V2;
    TestStruct TestStruct;
} Out;


void Vertex_InitGlobals()
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

void Vertex_CopyInputs(Vertex self)
{
}

void Main(Vertex self)
{
}

void Vertex_CopyOutputs(Vertex self)
{
    Out.Value = self.Value;
    Out.Value2 = self.Value2;
    Out.V2 = self.V2;
    Out.TestStruct = self.TestStruct;
}

void main()
{
    Vertex_InitGlobals();
    Vertex self;
    Vertex_DefaultConstructor(self);
    Vertex_CopyInputs(self);
    Main(self);
    Vertex_CopyOutputs(self);
}

