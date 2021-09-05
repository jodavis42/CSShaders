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

void HardwareBuiltInInputTest_Vertex_PreConstructor(inout HardwareBuiltInInputTest_Vertex self)
{
    self.VertexIndex = 0;
    self.InstanceId = 0;
}

void HardwareBuiltInInputTest_Vertex_DefaultConstructor(inout HardwareBuiltInInputTest_Vertex self)
{
    HardwareBuiltInInputTest_Vertex_PreConstructor(self);
}

void HardwareBuiltInInputTest_Vertex_CopyInputs(inout HardwareBuiltInInputTest_Vertex self)
{
    self.VertexIndex = gl_VertexID;
    self.InstanceId = gl_InstanceID;
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.VertexIndex = 0;
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
    self.InstanceId = 0;
    self.VIndex = 0;
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
    self.VertIndex = 0.0;
    self.InstanceId = 0.0;
}

void Vertex2_DefaultConstructor(inout Vertex2 self)
{
    Vertex2_PreConstructor(self);
}

void Main(Vertex2 self)
{
}

void Main(HardwareBuiltInInputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    vertex0.VertexIndex = self.VertexIndex;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.InstanceId = self.InstanceId;
    vertex1.VIndex = self.VertexIndex;
    Main(vertex1);
    Vertex2 tempVertex2;
    Vertex2_DefaultConstructor(tempVertex2);
    Vertex2 vertex2 = tempVertex2;
    Main(vertex2);
}

void main()
{
    HardwareBuiltInInputTest_Vertex self;
    HardwareBuiltInInputTest_Vertex_DefaultConstructor(self);
    HardwareBuiltInInputTest_Vertex_CopyInputs(self);
    Main(self);
}

