#version 450

struct FragmentInputOutputTest_Vertex
{
    int empty_struct_member;
};

struct Vertex0
{
    float Value1;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
    vec2 Value6;
    float Value7;
};

struct Vertex1
{
    float Value1;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
    float Value6;
    float Value7;
};

struct Vertex2
{
    float Value1;
    float Value2;
    float Value5;
    float Value7;
};

struct FragmentInputOutputTest_Pixel
{
    int empty_struct_member;
};

struct Pixel0
{
    float Value1;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
    vec2 Value6;
    float Value7;
};

struct Pixel1
{
    float Value1;
    float Value2;
    float Value3;
    float Value4;
    float Value5;
    float Value6;
    float Value7;
};

struct Pixel2
{
    float Value1;
    float Value2;
    float Value5;
    float Value7;
};

void FragmentInputOutputTest_Pixel_PreConstructor(FragmentInputOutputTest_Pixel self)
{
}

void FragmentInputOutputTest_Pixel_DefaultConstructor(FragmentInputOutputTest_Pixel self)
{
    FragmentInputOutputTest_Pixel_PreConstructor(self);
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value4 = 0.0;
    self.Value5 = 0.0;
    self.Value7 = 0.0;
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
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value4 = 0.0;
    self.Value5 = 0.0;
    self.Value6 = 0.0;
    self.Value7 = 0.0;
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
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value5 = 0.0;
    self.Value7 = 0.0;
}

void Pixel2_DefaultConstructor(inout Pixel2 self)
{
    Pixel2_PreConstructor(self);
}

void Main(Pixel2 self)
{
}

void Main(FragmentInputOutputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.Value1 = pixel0.Value1;
    pixel1.Value2 = pixel0.Value2;
    pixel1.Value7 = pixel0.Value7;
    Main(pixel1);
    Pixel2 tempPixel2;
    Pixel2_DefaultConstructor(tempPixel2);
    Pixel2 pixel2 = tempPixel2;
    pixel2.Value1 = pixel1.Value1;
    pixel2.Value2 = pixel0.Value2;
    pixel2.Value5 = pixel0.Value5;
    pixel2.Value7 = pixel1.Value7;
    Main(pixel2);
}

void main()
{
    FragmentInputOutputTest_Pixel self;
    FragmentInputOutputTest_Pixel_DefaultConstructor(self);
    Main(self);
}

