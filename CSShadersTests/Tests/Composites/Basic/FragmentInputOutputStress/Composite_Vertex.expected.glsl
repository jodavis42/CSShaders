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

void FragmentInputOutputTest_Vertex_PreConstructor(FragmentInputOutputTest_Vertex self)
{
}

void FragmentInputOutputTest_Vertex_DefaultConstructor(FragmentInputOutputTest_Vertex self)
{
    FragmentInputOutputTest_Vertex_PreConstructor(self);
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value4 = 0.0;
    self.Value5 = 0.0;
    self.Value7 = 0.0;
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
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value3 = 0.0;
    self.Value4 = 0.0;
    self.Value5 = 0.0;
    self.Value6 = 0.0;
    self.Value7 = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Vertex2_PreConstructor(inout Vertex2 self)
{
    self.Value1 = 0.0;
    self.Value2 = 0.0;
    self.Value5 = 0.0;
    self.Value7 = 0.0;
}

void Vertex2_DefaultConstructor(inout Vertex2 self)
{
    Vertex2_PreConstructor(self);
}

void Main(Vertex2 self)
{
}

void Main(FragmentInputOutputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.Value1 = vertex0.Value1;
    vertex1.Value2 = vertex0.Value2;
    vertex1.Value7 = vertex0.Value7;
    Main(vertex1);
    Vertex2 tempVertex2;
    Vertex2_DefaultConstructor(tempVertex2);
    Vertex2 vertex2 = tempVertex2;
    vertex2.Value1 = vertex1.Value1;
    vertex2.Value2 = vertex0.Value2;
    vertex2.Value5 = vertex0.Value5;
    vertex2.Value7 = vertex1.Value7;
    Main(vertex2);
}

void main()
{
    FragmentInputOutputTest_Vertex self;
    FragmentInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
}

