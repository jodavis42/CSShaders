#version 450

struct HardwareBuiltInOutputTest_Vertex
{
    vec4 ApiPerspectivePosition;
    float PointSize;
};

struct Vertex0
{
    vec4 ApiPerspectivePosition;
};

struct Vertex1
{
    float PointSize;
};

struct Vertex2
{
    vec3 ApiPerspectivePosition;
    int PSize;
};

struct HardwareBuiltInOutputTest_Pixel
{
    float FragDepth;
};

struct Pixel0
{
    float FragDepth;
};

struct Pixel1
{
    int FDepth;
};

void HardwareBuiltInOutputTest_Pixel_PreConstructor(inout HardwareBuiltInOutputTest_Pixel self)
{
    self.FragDepth = 0.0;
}

void HardwareBuiltInOutputTest_Pixel_DefaultConstructor(inout HardwareBuiltInOutputTest_Pixel self)
{
    HardwareBuiltInOutputTest_Pixel_PreConstructor(self);
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.FragDepth = 0.0;
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
    self.FDepth = 0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Main(inout HardwareBuiltInOutputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    Main(pixel1);
    self.FragDepth = pixel0.FragDepth;
}

void HardwareBuiltInOutputTest_Pixel_CopyOutputs(HardwareBuiltInOutputTest_Pixel self)
{
    gl_FragDepth = self.FragDepth;
}

void main()
{
    HardwareBuiltInOutputTest_Pixel self;
    HardwareBuiltInOutputTest_Pixel_DefaultConstructor(self);
    Main(self);
    HardwareBuiltInOutputTest_Pixel_CopyOutputs(self);
}

