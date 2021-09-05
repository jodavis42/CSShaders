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

void AppBuiltInInputTest_Vertex_PreConstructor(inout AppBuiltInInputTest_Vertex self)
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

void AppBuiltInInputTest_Vertex_DefaultConstructor(inout AppBuiltInInputTest_Vertex self)
{
    AppBuiltInInputTest_Vertex_PreConstructor(self);
}

void AppBuiltInInputTest_Vertex_CopyInputs(inout AppBuiltInInputTest_Vertex self)
{
    self.FrameData.FrameTime = FrameData_Instance.FrameTime;
    self.FrameData.LogicTime = FrameData_Instance.LogicTime;
    self.CameraData.NearPlane = CameraData_Instance.NearPlane;
    self.CameraData.FarPlane = CameraData_Instance.FarPlane;
    self.CameraData.ViewportSize = CameraData_Instance.ViewportSize;
    self.TransformData.LocalToWorld = TransformData_Instance.LocalToWorld;
    self.TransformData.WorldToView = TransformData_Instance.WorldToView;
    self.TransformData.ViewToPerspective = TransformData_Instance.ViewToPerspective;
    self.TransformData.LocalToPerspective = TransformData_Instance.LocalToPerspective;
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.FrameTime = 0.0;
    self.LogicTime = 0.0;
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
    self.Time = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Main(AppBuiltInInputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    vertex0.FrameTime = self.FrameData.FrameTime;
    vertex0.LogicTime = self.FrameData.LogicTime;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.Time = self.FrameData.FrameTime;
    vertex1.ViewportSize = self.CameraData.ViewportSize;
    vertex1.LocalToWorld = self.TransformData.LocalToWorld;
    Main(vertex1);
}

void main()
{
    AppBuiltInInputTest_Vertex self;
    AppBuiltInInputTest_Vertex_DefaultConstructor(self);
    AppBuiltInInputTest_Vertex_CopyInputs(self);
    Main(self);
}

