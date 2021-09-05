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

void PropertyTest_Pixel_PreConstructor(inout PropertyTest_Pixel self)
{
    self.Pixel0_pixel0_Value = 0.0;
    self.Pixel0_pixel0_Value2 = 0.0;
    self.Pixel1_pixel1_Value = 0.0;
    self.Pixel1_pixel1_MyValue = 0.0;
}

void PropertyTest_Pixel_DefaultConstructor(inout PropertyTest_Pixel self)
{
    PropertyTest_Pixel_PreConstructor(self);
}

void PropertyTest_Pixel_CopyInputs(inout PropertyTest_Pixel self)
{
    self.Pixel0_pixel0_Value = PropertyTest_Pixel_SharedMaterialBuffer_Instance.Pixel0_pixel0_Value;
    self.Pixel0_pixel0_Value2 = PropertyTest_Pixel_SharedMaterialBuffer_Instance.Pixel0_pixel0_Value2;
    self.Pixel1_pixel1_Value = PropertyTest_Pixel_SharedMaterialBuffer_Instance.Pixel1_pixel1_Value;
    self.Pixel1_pixel1_MyValue = PropertyTest_Pixel_SharedMaterialBuffer_Instance.Pixel1_pixel1_MyValue;
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.Value = 0.0;
    self.Value2 = 0.0;
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
    self.Value = 0.0;
    self.Value3 = 0.0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Main(PropertyTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    pixel0.Value = self.Pixel0_pixel0_Value;
    pixel0.Value2 = self.Pixel0_pixel0_Value2;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.Value = self.Pixel1_pixel1_Value;
    pixel1.Value3 = self.Pixel1_pixel1_MyValue;
    Main(pixel1);
}

void main()
{
    PropertyTest_Pixel self;
    PropertyTest_Pixel_DefaultConstructor(self);
    PropertyTest_Pixel_CopyInputs(self);
    Main(self);
}

