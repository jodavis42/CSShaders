#version 450

struct FragmentInputOutputTest_Vertex
{
    int empty_struct_member;
};

struct Vertex0
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct Vertex1
{
    float Scale;
    vec2 Uv;
};

struct Vertex2
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct FragmentInputOutputTest_Pixel
{
    int empty_struct_member;
};

struct Pixel0
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct Pixel1
{
    float Scale;
    vec2 Uv;
};

struct Pixel2
{
    float Scale;
    vec2 Uv;
    vec4 Color;
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
    self.Scale = 0.0;
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
    self.Scale = 0.0;
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
    self.Scale = 0.0;
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
    pixel1.Scale = pixel0.Scale;
    Main(pixel1);
    Pixel2 tempPixel2;
    Pixel2_DefaultConstructor(tempPixel2);
    Pixel2 pixel2 = tempPixel2;
    pixel2.Scale = pixel1.Scale;
    pixel2.Uv = pixel1.Uv;
    pixel2.Color = pixel0.Color;
    Main(pixel2);
}

void main()
{
    FragmentInputOutputTest_Pixel self;
    FragmentInputOutputTest_Pixel_DefaultConstructor(self);
    Main(self);
}

