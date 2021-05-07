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

void FragmentInputOutputTest_Vertex_PreConstructor(FragmentInputOutputTest_Vertex self)
{
}

void FragmentInputOutputTest_Vertex_DefaultConstructor(FragmentInputOutputTest_Vertex self)
{
    FragmentInputOutputTest_Vertex_PreConstructor(self);
}

void Vertex1_PreConstructor(inout Vertex1 self)
{
    self.V1Value1 = 0.0;
    self.V1Value2 = 0.0;
    self.V1Value34 = 0.0;
    self.V1Value8 = 0.0;
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
    self.V2Value1 = 0.0;
    self.V2Value2 = 0.0;
    self.V2Value3 = 0.0;
    self.V2Value6 = 0.0;
    self.V2Value8 = 0.0;
}

void Vertex2_DefaultConstructor(inout Vertex2 self)
{
    Vertex2_PreConstructor(self);
}

void Main(Vertex2 self)
{
}

void Vertex3_PreConstructor(inout Vertex3 self)
{
    self.V3Value1 = 0.0;
    self.V3Value2 = 0.0;
    self.V3Value4 = 0.0;
    self.V3Value8 = 0.0;
}

void Vertex3_DefaultConstructor(inout Vertex3 self)
{
    Vertex3_PreConstructor(self);
}

void Main(Vertex3 self)
{
}

void Main(FragmentInputOutputTest_Vertex self)
{
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    Main(vertex1);
    Vertex2 tempVertex2;
    Vertex2_DefaultConstructor(tempVertex2);
    Vertex2 vertex2 = tempVertex2;
    vertex2.V2Value1 = vertex1.V1Value1;
    vertex2.V2Value2 = vertex1.V1Value2;
    vertex2.V2Value3 = vertex1.V1Value34;
    vertex2.V2Value8 = vertex1.V1Value8;
    Main(vertex2);
    Vertex3 tempVertex3;
    Vertex3_DefaultConstructor(tempVertex3);
    Vertex3 vertex3 = tempVertex3;
    vertex3.V3Value1 = vertex2.V2Value1;
    vertex3.V3Value2 = vertex1.V1Value2;
    vertex3.V3Value4 = vertex1.V1Value34;
    vertex3.V3Value8 = vertex2.V2Value8;
    Main(vertex3);
}

void main()
{
    FragmentInputOutputTest_Vertex self;
    FragmentInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
}

