#version 450

struct FrameData
{
    float FrameTime;
    float LogicTime;
};

struct InputOutputTest_Vertex
{
    FrameData FrameData;
    int VertexIndex;
    vec4 Vertex0_vertex0_CustomVertexProperty;
    vec4 ApiPerspectivePosition;
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
};

struct Vertex0
{
    float FrameTime;
    int VertexIndex;
    vec4 CustomVertexProperty;
};

struct Vertex1
{
    float FrameTime;
    int VertexIndex;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
    vec4 ApiPerspectivePosition;
};

struct InputOutputTest_Pixel
{
    FrameData FrameData;
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
    vec4 Pixel0_pixel0_CustomPixelProperty;
    vec4 FragCoord;
    float FragDepth;
};

struct Pixel0
{
    float LogicTime;
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
    vec4 CustomPixelProperty;
    vec4 FragCoord;
    vec4 PixelFragmentOutput;
    float FragDepth;
};

struct Pixel1
{
    float LogicTime;
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
    vec4 CustomPixelProperty;
    vec4 FragCoord;
    vec4 PixelFragmentOutput;
    float FragDepth;
};

layout(std140) uniform FrameData_0
{
    float FrameTime;
    float LogicTime;
} FrameData_Instance;

layout(std140) uniform InputOutputTest_Vertex_SharedMaterialBuffer
{
    vec4 Vertex0_vertex0_CustomVertexProperty;
} InputOutputTest_Vertex_SharedMaterialBuffer_Instance;

layout(std140) uniform FrameData_1
{
    float FrameTime;
    float LogicTime;
} FrameData_Instance_1;

layout(std140) uniform InputOutputTest_Pixel_SharedMaterialBuffer
{
    vec4 Pixel0_pixel0_CustomPixelProperty;
} InputOutputTest_Pixel_SharedMaterialBuffer_Instance;

layout(location = 0) out InputOutputTest_Vertex_Outputs
{
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
} Out;


void FrameData_PreConstructor(inout FrameData self)
{
    self.FrameTime = 0.0;
    self.LogicTime = 0.0;
}

void FrameData_DefaultConstructor(inout FrameData self)
{
    FrameData_PreConstructor(self);
}

void InputOutputTest_Vertex_PreConstructor(inout InputOutputTest_Vertex self)
{
    FrameData tempFrameData;
    FrameData_DefaultConstructor(tempFrameData);
    self.FrameData = tempFrameData;
    self.VertexIndex = 0;
    self.FrameTime = 0.0;
}

void InputOutputTest_Vertex_DefaultConstructor(inout InputOutputTest_Vertex self)
{
    InputOutputTest_Vertex_PreConstructor(self);
}

void InputOutputTest_Vertex_CopyInputs(inout InputOutputTest_Vertex self)
{
    self.VertexIndex = gl_VertexID;
    self.FrameData.FrameTime = FrameData_Instance.FrameTime;
    self.FrameData.LogicTime = FrameData_Instance.LogicTime;
    self.Vertex0_vertex0_CustomVertexProperty = InputOutputTest_Vertex_SharedMaterialBuffer_Instance.Vertex0_vertex0_CustomVertexProperty;
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.FrameTime = 0.0;
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
    self.FrameTime = 0.0;
    self.VertexIndex = 0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Main(inout InputOutputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    vertex0.FrameTime = self.FrameData.FrameTime;
    vertex0.VertexIndex = self.VertexIndex;
    vertex0.CustomVertexProperty = self.Vertex0_vertex0_CustomVertexProperty;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.FrameTime = vertex0.FrameTime;
    vertex1.VertexIndex = vertex0.VertexIndex;
    vertex1.CustomVertexProperty = vertex0.CustomVertexProperty;
    Main(vertex1);
    self.ApiPerspectivePosition = vertex1.ApiPerspectivePosition;
    self.FrameTime = vertex1.FrameTime;
    self.CustomVertexProperty = vertex1.CustomVertexProperty;
    self.VertexStageOutput = vertex1.VertexStageOutput;
}

void InputOutputTest_Vertex_CopyOutputs(InputOutputTest_Vertex self)
{
    gl_Position = self.ApiPerspectivePosition;
    Out.FrameTime = self.FrameTime;
    Out.CustomVertexProperty = self.CustomVertexProperty;
    Out.VertexStageOutput = self.VertexStageOutput;
}

void main()
{
    InputOutputTest_Vertex self;
    InputOutputTest_Vertex_DefaultConstructor(self);
    InputOutputTest_Vertex_CopyInputs(self);
    Main(self);
    InputOutputTest_Vertex_CopyOutputs(self);
}

