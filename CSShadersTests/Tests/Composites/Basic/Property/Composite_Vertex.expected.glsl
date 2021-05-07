#version 450

struct PropertyTest_Vertex
{
    float Vertex0_vertex0_Value;
    float Vertex0_vertex0_Value2;
    float Vertex1_vertex1_Value;
    float Vertex1_vertex1_MyValue;
};

struct Vertex0
{
    float Value;
    float Value2;
};

struct Vertex1
{
    float Value;
    float Value3;
};

struct PropertyTest_Pixel
{
    float Pixel0_pixel0_Value;
    float Pixel0_pixel0_Value2;
    float Pixel1_pixel1_Value;
    float Pixel1_pixel1_MyValue;
};

struct Pixel0
{
    float Value;
    float Value2;
};

struct Pixel1
{
    float Value;
    float Value3;
};

layout(std140) uniform PropertyTest_Vertex_SharedMaterialBuffer
{
    float Vertex0_vertex0_Value;
    float Vertex0_vertex0_Value2;
    float Vertex1_vertex1_Value;
    float Vertex1_vertex1_MyValue;
} PropertyTest_Vertex_SharedMaterialBuffer_Instance;

layout(std140) uniform PropertyTest_Pixel_SharedMaterialBuffer
{
    float Pixel0_pixel0_Value;
    float Pixel0_pixel0_Value2;
    float Pixel1_pixel1_Value;
    float Pixel1_pixel1_MyValue;
} PropertyTest_Pixel_SharedMaterialBuffer_Instance;

void PropertyTest_Vertex_PreConstructor(inout PropertyTest_Vertex self)
{
    self.Vertex0_vertex0_Value = 0.0;
    self.Vertex0_vertex0_Value2 = 0.0;
    self.Vertex1_vertex1_Value = 0.0;
    self.Vertex1_vertex1_MyValue = 0.0;
}

void PropertyTest_Vertex_DefaultConstructor(inout PropertyTest_Vertex self)
{
    PropertyTest_Vertex_PreConstructor(self);
}

void PropertyTest_Vertex_CopyInputs(inout PropertyTest_Vertex self)
{
    self.Vertex0_vertex0_Value = PropertyTest_Vertex_SharedMaterialBuffer_Instance.Vertex0_vertex0_Value;
    self.Vertex0_vertex0_Value2 = PropertyTest_Vertex_SharedMaterialBuffer_Instance.Vertex0_vertex0_Value2;
    self.Vertex1_vertex1_Value = PropertyTest_Vertex_SharedMaterialBuffer_Instance.Vertex1_vertex1_Value;
    self.Vertex1_vertex1_MyValue = PropertyTest_Vertex_SharedMaterialBuffer_Instance.Vertex1_vertex1_MyValue;
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.Value = 0.0;
    self.Value2 = 0.0;
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
    self.Value = 0.0;
    self.Value3 = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Main(PropertyTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    vertex0.Value = self.Vertex0_vertex0_Value;
    vertex0.Value2 = self.Vertex0_vertex0_Value2;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.Value = self.Vertex1_vertex1_Value;
    vertex1.Value3 = self.Vertex1_vertex1_MyValue;
    Main(vertex1);
}

void main()
{
    PropertyTest_Vertex self;
    PropertyTest_Vertex_DefaultConstructor(self);
    PropertyTest_Vertex_CopyInputs(self);
    Main(self);
}

