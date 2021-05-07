#version 450

struct HardwareBuiltInInputTest_Vertex
{
    int VertexIndex;
    int InstanceId;
};

struct Vertex0
{
    int VertexIndex;
};

struct Vertex1
{
    int InstanceId;
    int VIndex;
};

struct Vertex2
{
    float VertIndex;
    float InstanceId;
};

struct HardwareBuiltInInputTest_Pixel
{
    vec4 FragCoord;
    vec2 PointCoord;
};

struct Pixel0
{
    vec4 FragCoord;
};

struct Pixel1
{
    vec2 PointCoord;
    vec4 fragCoord;
};

struct Pixel2
{
    float FragCoord;
    float PointCoord;
};

void HardwareBuiltInInputTest_Pixel_PreConstructor(HardwareBuiltInInputTest_Pixel self)
{
}

void HardwareBuiltInInputTest_Pixel_DefaultConstructor(HardwareBuiltInInputTest_Pixel self)
{
    HardwareBuiltInInputTest_Pixel_PreConstructor(self);
}

void HardwareBuiltInInputTest_Pixel_CopyInputs(inout HardwareBuiltInInputTest_Pixel self)
{
    self.FragCoord = gl_FragCoord;
    self.PointCoord = gl_PointCoord;
}

void Pixel0_PreConstructor(Pixel0 self)
{
}

void Pixel0_DefaultConstructor(Pixel0 self)
{
    Pixel0_PreConstructor(self);
}

void Main(Pixel0 self)
{
}

void Pixel1_PreConstructor(Pixel1 self)
{
}

void Pixel1_DefaultConstructor(Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Pixel2_PreConstructor(inout Pixel2 self)
{
    self.FragCoord = 0.0;
    self.PointCoord = 0.0;
}

void Pixel2_DefaultConstructor(inout Pixel2 self)
{
    Pixel2_PreConstructor(self);
}

void Main(Pixel2 self)
{
}

void Main(HardwareBuiltInInputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    pixel0.FragCoord = self.FragCoord;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.PointCoord = self.PointCoord;
    pixel1.fragCoord = self.FragCoord;
    Main(pixel1);
    Pixel2 tempPixel2;
    Pixel2_DefaultConstructor(tempPixel2);
    Pixel2 pixel2 = tempPixel2;
    Main(pixel2);
}

void main()
{
    HardwareBuiltInInputTest_Pixel self;
    HardwareBuiltInInputTest_Pixel_DefaultConstructor(self);
    HardwareBuiltInInputTest_Pixel_CopyInputs(self);
    Main(self);
}

