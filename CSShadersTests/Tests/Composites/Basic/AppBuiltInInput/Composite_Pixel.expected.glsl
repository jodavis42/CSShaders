#version 450

struct FrameData
{
    float FrameTime;
    float LogicTime;
};

struct CameraData
{
    float NearPlane;
    float FarPlane;
    vec2 ViewportSize;
};

struct TransformData
{
    mat4 LocalToWorld;
    mat4 WorldToView;
    mat4 ViewToPerspective;
    mat4 LocalToPerspective;
};

struct AppBuiltInInputTest_Vertex
{
    FrameData FrameData;
    CameraData CameraData;
    TransformData TransformData;
};

struct Vertex0
{
    float FrameTime;
    float LogicTime;
};

struct Vertex1
{
    float Time;
    vec2 ViewportSize;
    mat4 LocalToWorld;
};

struct AppBuiltInInputTest_Pixel
{
    FrameData FrameData;
    CameraData CameraData;
    TransformData TransformData;
};

struct Pixel0
{
    float FrameTime;
    float LogicTime;
};

struct Pixel1
{
    float Time;
    vec2 ViewportSize;
    mat4 LocalToWorld;
};

layout(std140) uniform FrameData_0
{
    float FrameTime;
    float LogicTime;
} FrameData_Instance;

layout(std140) uniform CameraData_0
{
    float NearPlane;
    float FarPlane;
    vec2 ViewportSize;
} CameraData_Instance;

layout(std140) uniform TransformData_0
{
    mat4 LocalToWorld;
    mat4 WorldToView;
    mat4 ViewToPerspective;
    mat4 LocalToPerspective;
} TransformData_Instance;

layout(std140) uniform FrameData_1
{
    float FrameTime;
    float LogicTime;
} FrameData_Instance_1;

layout(std140) uniform CameraData_1
{
    float NearPlane;
    float FarPlane;
    vec2 ViewportSize;
} CameraData_Instance_1;

layout(std140) uniform TransformData_1
{
    mat4 LocalToWorld;
    mat4 WorldToView;
    mat4 ViewToPerspective;
    mat4 LocalToPerspective;
} TransformData_Instance_1;

void FrameData_PreConstructor(inout FrameData self)
{
    self.FrameTime = 0.0;
    self.LogicTime = 0.0;
}

void FrameData_DefaultConstructor(inout FrameData self)
{
    FrameData_PreConstructor(self);
}

void CameraData_PreConstructor(inout CameraData self)
{
    self.NearPlane = 0.0;
    self.FarPlane = 0.0;
}

void CameraData_DefaultConstructor(inout CameraData self)
{
    CameraData_PreConstructor(self);
}

void TransformData_PreConstructor(TransformData self)
{
}

void TransformData_DefaultConstructor(TransformData self)
{
    TransformData_PreConstructor(self);
}

void AppBuiltInInputTest_Pixel_PreConstructor(inout AppBuiltInInputTest_Pixel self)
{
    FrameData tempFrameData;
    FrameData_DefaultConstructor(tempFrameData);
    self.FrameData = tempFrameData;
    CameraData tempCameraData;
    CameraData_DefaultConstructor(tempCameraData);
    self.CameraData = tempCameraData;
    TransformData tempTransformData;
    TransformData_DefaultConstructor(tempTransformData);
    self.TransformData = tempTransformData;
}

void AppBuiltInInputTest_Pixel_DefaultConstructor(inout AppBuiltInInputTest_Pixel self)
{
    AppBuiltInInputTest_Pixel_PreConstructor(self);
}

void AppBuiltInInputTest_Pixel_CopyInputs(inout AppBuiltInInputTest_Pixel self)
{
    self.FrameData.FrameTime = FrameData_Instance_1.FrameTime;
    self.FrameData.LogicTime = FrameData_Instance_1.LogicTime;
    self.CameraData.NearPlane = CameraData_Instance_1.NearPlane;
    self.CameraData.FarPlane = CameraData_Instance_1.FarPlane;
    self.CameraData.ViewportSize = CameraData_Instance_1.ViewportSize;
    self.TransformData.LocalToWorld = TransformData_Instance_1.LocalToWorld;
    self.TransformData.WorldToView = TransformData_Instance_1.WorldToView;
    self.TransformData.ViewToPerspective = TransformData_Instance_1.ViewToPerspective;
    self.TransformData.LocalToPerspective = TransformData_Instance_1.LocalToPerspective;
}

void Pixel0_PreConstructor(inout Pixel0 self)
{
    self.FrameTime = 0.0;
    self.LogicTime = 0.0;
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
    self.Time = 0.0;
}

void Pixel1_DefaultConstructor(inout Pixel1 self)
{
    Pixel1_PreConstructor(self);
}

void Main(Pixel1 self)
{
}

void Main(AppBuiltInInputTest_Pixel self)
{
    Pixel0 tempPixel0;
    Pixel0_DefaultConstructor(tempPixel0);
    Pixel0 pixel0 = tempPixel0;
    pixel0.FrameTime = self.FrameData.FrameTime;
    pixel0.LogicTime = self.FrameData.LogicTime;
    Main(pixel0);
    Pixel1 tempPixel1;
    Pixel1_DefaultConstructor(tempPixel1);
    Pixel1 pixel1 = tempPixel1;
    pixel1.Time = self.FrameData.FrameTime;
    pixel1.ViewportSize = self.CameraData.ViewportSize;
    pixel1.LocalToWorld = self.TransformData.LocalToWorld;
    Main(pixel1);
}

void main()
{
    AppBuiltInInputTest_Pixel self;
    AppBuiltInInputTest_Pixel_DefaultConstructor(self);
    AppBuiltInInputTest_Pixel_CopyInputs(self);
    Main(self);
}

