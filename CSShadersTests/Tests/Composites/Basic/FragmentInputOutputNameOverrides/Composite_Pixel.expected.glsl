#version 450

struct FragmentInputOutputTest_Vertex
{
    int empty_struct_member;
};

struct Vertex1
{
    float V1Value1;
    float V1Value2;
    float V1Value34;
    vec2 V1Value6;
    vec2 Value7;
    float V1Value8;
};

struct Vertex2
{
    float V2Value1;
    float V2Value2;
    float V2Value3;
    float V2Value6;
    vec2 Value7;
    float V2Value8;
};

struct Vertex3
{
    float V3Value1;
    float V3Value2;
    float V3Value4;
    float V3Value8;
};

struct FragmentInputOutputTest_Pixel
{
    int empty_struct_member;
};

struct Pixel1
{
    float V1Value1;
    float V1Value2;
    float V1Value34;
    vec2 V1Value6;
    vec2 Value7;
    float V1Value8;
};

struct Pixel2
{
    float V2Value1;
    float V2Value2;
    float V2Value3;
    float V2Value6;
    vec2 Value7;
    float V2Value8;
};

struct Pixel3
{
    float V3Value1;
    float V3Value2;
    float V3Value4;
    float V3Value8;
};

void FragmentInputOutputTest_Pixel_PreConstructor(FragmentInputOutputTest_Pixel self)
{
}

void FragmentInputOutputTest_Pixel_DefaultConstructor(FragmentInputOutputTest_Pixel self)
{
    FragmentInputOutputTest_Pixel_PreConstructor(self);
}

void Pixel1_PreConstructor(inout Pixel1 self)
{
    self.V1Value1 = 0.0;
    self.V1Value2 = 0.0;
    self.V1Value34 = 0.0;
    self.V1Value8 = 0.0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Pixel2_PreConstructor(inout Pixel2 self)
{
    self.V2Value1 = 0.0;
    self.V2Value2 = 0.0;
    self.V2Value3 = 0.0;
    self.V2Value6 = 0.0;
    self.V2Value8 = 0.0;
}

void Pixel2_DefaultConstructor(inout Pixel2 self)
{
    Pixel2_PreConstructor(self);
}

void Main(Pixel2 self)
{
}

void Pixel3_PreConstructor(inout Pixel3 self)
{
    self.V3Value1 = 0.0;
    self.V3Value2 = 0.0;
    self.V3Value4 = 0.0;
    self.V3Value8 = 0.0;
}

void Pixel3_DefaultConstructor(inout Pixel3 self)
{
    Pixel3_PreConstructor(self);
}

void Main(Pixel3 self)
{
}

void Main(FragmentInputOutputTest_Pixel self)
{
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    Main(pixel1);
    Pixel2 tempPixel2;
    Pixel2_DefaultConstructor(tempPixel2);
    Pixel2 pixel2 = tempPixel2;
    pixel2.V2Value1 = pixel1.V1Value1;
    pixel2.V2Value2 = pixel1.V1Value2;
    pixel2.V2Value3 = pixel1.V1Value34;
    pixel2.V2Value8 = pixel1.V1Value8;
    Main(pixel2);
    Pixel3 tempPixel3;
    Pixel3_DefaultConstructor(tempPixel3);
    Pixel3 pixel3 = tempPixel3;
    pixel3.V3Value1 = pixel2.V2Value1;
    pixel3.V3Value2 = pixel1.V1Value2;
    pixel3.V3Value4 = pixel1.V1Value34;
    pixel3.V3Value8 = pixel2.V2Value8;
    Main(pixel3);
}

void main()
{
    FragmentInputOutputTest_Pixel self;
    FragmentInputOutputTest_Pixel_DefaultConstructor(self);
    Main(self);
}

