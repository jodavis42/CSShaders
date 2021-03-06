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

layout(location = 0) in float Value;
layout(location = 1) in float Value2;
layout(location = 2) in vec2 V2;
layout(location = 3) in TestStruct TestStruct_1;

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
    self.Value = Value;
    self.Value2 = Value2;
    self.V2 = V2;
    self.TestStruct = TestStruct_1;
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

