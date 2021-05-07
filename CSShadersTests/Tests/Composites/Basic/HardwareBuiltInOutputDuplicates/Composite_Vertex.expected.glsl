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
    vec4 ApiPerspectivePosition;
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
    float FragDepth;
};

void HardwareBuiltInOutputTest_Vertex_PreConstructor(inout HardwareBuiltInOutputTest_Vertex self)
{
    self.PointSize = 0.0;
}

void HardwareBuiltInOutputTest_Vertex_DefaultConstructor(inout HardwareBuiltInOutputTest_Vertex self)
{
    HardwareBuiltInOutputTest_Vertex_PreConstructor(self);
}

void Vertex0_PreConstructor(Vertex0 self)
{
}

void Vertex0_DefaultConstructor(Vertex0 self)
{
    Vertex0_PreConstructor(self);
}

void Main(Vertex0 self)
{
}

void Vertex1_PreConstructor(inout Vertex1 self)
{
    self.PointSize = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Vertex2_PreConstructor(Vertex2 self)
{
}

void Vertex2_DefaultConstructor(Vertex2 self)
{
    Vertex2_PreConstructor(self);
}

void Main(Vertex2 self)
{
}

void Main(inout HardwareBuiltInOutputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    Main(vertex1);
    Vertex2 tempVertex2;
    Vertex2_DefaultConstructor(tempVertex2);
    Vertex2 vertex2 = tempVertex2;
    Main(vertex2);
    self.PointSize = vertex1.PointSize;
    self.ApiPerspectivePosition = vertex2.ApiPerspectivePosition;
}

void HardwareBuiltInOutputTest_Vertex_CopyOutputs(HardwareBuiltInOutputTest_Vertex self)
{
    gl_Position = self.ApiPerspectivePosition;
    gl_PointSize = self.PointSize;
}

void main()
{
    HardwareBuiltInOutputTest_Vertex self;
    HardwareBuiltInOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
    HardwareBuiltInOutputTest_Vertex_CopyOutputs(self);
}

