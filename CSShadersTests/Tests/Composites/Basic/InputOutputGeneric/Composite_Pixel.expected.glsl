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

layout(location = 0) in InputOutputTest_Pixel_Inputs
{
    float FrameTime;
    vec4 CustomVertexProperty;
    vec4 VertexStageOutput;
} In;


void FrameData_PreConstructor(inout FrameData self)
{
    self.FrameTime = 0.0;
    self.LogicTime = 0.0;
}

void FrameData_DefaultConstructor(inout FrameData self)
{
    FrameData_PreConstructor(self);
}

void InputOutputTest_Pixel_PreConstructor(inout InputOutputTest_Pixel self)
{
    FrameData tempFrameData;
    FrameData_DefaultConstructor(tempFrameData);
    self.FrameData = tempFrameData;
    self.FrameTime = 0.0;
    self.FragDepth = 0.0;
}

void InputOutputTest_Pixel_DefaultConstructor(inout InputOutputTest_Pixel self)
{
    InputOutputTest_Pixel_PreConstructor(self);
}

void InputOutputTest_Pixel_CopyInputs(inout InputOutputTest_Pixel self)
{
    self.FragCoord = gl_FragCoord;
    self.FrameTime = In.FrameTime;
    self.CustomVertexProperty = In.CustomVertexProperty;
    self.VertexStageOutput = In.VertexStageOutput;
    self.FrameData.FrameTime = FrameData_Instance_1.FrameTime;
    self.FrameData.LogicTime = FrameData_Instance_1.LogicTime;
    self.Pixel0_pixel0_CustomPixelProperty = InputOutputTest_Pixel_SharedMaterialBuffer_Instance.Pixel0_pixel0_CustomPixelProperty;
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.LogicTime = 0.0;
    self.FrameTime = 0.0;
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
    self.LogicTime = 0.0;
    self.FrameTime = 0.0;
    self.FragDepth = 0.0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Main(inout InputOutputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    pixel0.LogicTime = self.FrameData.LogicTime;
    pixel0.FrameTime = self.FrameTime;
    pixel0.CustomVertexProperty = self.CustomVertexProperty;
    pixel0.VertexStageOutput = self.VertexStageOutput;
    pixel0.CustomPixelProperty = self.Pixel0_pixel0_CustomPixelProperty;
    pixel0.FragCoord = self.FragCoord;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.LogicTime = pixel0.LogicTime;
    pixel1.FrameTime = pixel0.FrameTime;
    pixel1.CustomVertexProperty = pixel0.CustomVertexProperty;
    pixel1.VertexStageOutput = pixel0.VertexStageOutput;
    pixel1.CustomPixelProperty = pixel0.CustomPixelProperty;
    pixel1.FragCoord = pixel0.FragCoord;
    pixel1.PixelFragmentOutput = pixel0.PixelFragmentOutput;
    pixel1.FragDepth = pixel0.FragDepth;
    Main(pixel1);
    self.FragDepth = pixel1.FragDepth;
}

void InputOutputTest_Pixel_CopyOutputs(InputOutputTest_Pixel self)
{
    gl_FragDepth = self.FragDepth;
}

void main()
{
    InputOutputTest_Pixel self;
    InputOutputTest_Pixel_DefaultConstructor(self);
    InputOutputTest_Pixel_CopyInputs(self);
    Main(self);
    InputOutputTest_Pixel_CopyOutputs(self);
}

