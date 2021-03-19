#version 450

struct Vec4Buffer0
{
    vec4 TestVector4_0;
    vec4 TestVector4_1;
};

struct Vec4Buffer1
{
    float Value0;
    vec4 TestVector4;
};

struct Vec4Buffer2
{
    float Value0;
    float Value1;
    vec4 TestVector4;
};

struct Vec4Buffer3
{
    float Value0;
    float Value1;
    float Value2;
    vec4 TestVector4;
};

struct UniformBufferVec4Layouts
{
    Vec4Buffer0 Vec4Buffer0;
    Vec4Buffer1 Vec4Buffer1;
    Vec4Buffer2 Vec4Buffer2;
    Vec4Buffer3 Vec4Buffer3;
};

layout(std140) uniform Vec4Buffer0_0
{
    vec4 TestVector4_0;
    vec4 TestVector4_1;
} Vec4Buffer0_Instance;

layout(std140) uniform Vec4Buffer1_0
{
    float Value0;
    vec4 TestVector4;
} Vec4Buffer1_Instance;

layout(std140) uniform Vec4Buffer2_0
{
    float Value0;
    float Value1;
    vec4 TestVector4;
} Vec4Buffer2_Instance;

layout(std140) uniform Vec4Buffer3_0
{
    float Value0;
    float Value1;
    float Value2;
    vec4 TestVector4;
} Vec4Buffer3_Instance;

void Vec4Buffer0_PreConstructor(Vec4Buffer0 self)
{
}

void Vec4Buffer0_DefaultConstructor(Vec4Buffer0 self)
{
    Vec4Buffer0_PreConstructor(self);
}

void Vec4Buffer1_PreConstructor(inout Vec4Buffer1 self)
{
    self.Value0 = 0.0;
}

void Vec4Buffer1_DefaultConstructor(inout Vec4Buffer1 self)
{
    Vec4Buffer1_PreConstructor(self);
}

void Vec4Buffer2_PreConstructor(inout Vec4Buffer2 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
}

void Vec4Buffer2_DefaultConstructor(inout Vec4Buffer2 self)
{
    Vec4Buffer2_PreConstructor(self);
}

void Vec4Buffer3_PreConstructor(inout Vec4Buffer3 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
    self.Value2 = 0.0;
}

void Vec4Buffer3_DefaultConstructor(inout Vec4Buffer3 self)
{
    Vec4Buffer3_PreConstructor(self);
}

void UniformBufferVec4Layouts_PreConstructor(inout UniformBufferVec4Layouts self)
{
    Vec4Buffer0 tempVec4Buffer0;
    Vec4Buffer0_DefaultConstructor(tempVec4Buffer0);
    self.Vec4Buffer0 = tempVec4Buffer0;
    Vec4Buffer1 tempVec4Buffer1;
    Vec4Buffer1_DefaultConstructor(tempVec4Buffer1);
    self.Vec4Buffer1 = tempVec4Buffer1;
    Vec4Buffer2 tempVec4Buffer2;
    Vec4Buffer2_DefaultConstructor(tempVec4Buffer2);
    self.Vec4Buffer2 = tempVec4Buffer2;
    Vec4Buffer3 tempVec4Buffer3;
    Vec4Buffer3_DefaultConstructor(tempVec4Buffer3);
    self.Vec4Buffer3 = tempVec4Buffer3;
}

void UniformBufferVec4Layouts_DefaultConstructor(inout UniformBufferVec4Layouts self)
{
    UniformBufferVec4Layouts_PreConstructor(self);
}

void UniformBufferVec4Layouts_CopyInputs(inout UniformBufferVec4Layouts self)
{
    self.Vec4Buffer0.TestVector4_0 = Vec4Buffer0_Instance.TestVector4_0;
    self.Vec4Buffer0.TestVector4_1 = Vec4Buffer0_Instance.TestVector4_1;
    self.Vec4Buffer1.Value0 = Vec4Buffer1_Instance.Value0;
    self.Vec4Buffer1.TestVector4 = Vec4Buffer1_Instance.TestVector4;
    self.Vec4Buffer2.Value0 = Vec4Buffer2_Instance.Value0;
    self.Vec4Buffer2.Value1 = Vec4Buffer2_Instance.Value1;
    self.Vec4Buffer2.TestVector4 = Vec4Buffer2_Instance.TestVector4;
    self.Vec4Buffer3.Value0 = Vec4Buffer3_Instance.Value0;
    self.Vec4Buffer3.Value1 = Vec4Buffer3_Instance.Value1;
    self.Vec4Buffer3.Value2 = Vec4Buffer3_Instance.Value2;
    self.Vec4Buffer3.TestVector4 = Vec4Buffer3_Instance.TestVector4;
}

void Main(UniformBufferVec4Layouts self)
{
}

void main()
{
    UniformBufferVec4Layouts self;
    UniformBufferVec4Layouts_DefaultConstructor(self);
    UniformBufferVec4Layouts_CopyInputs(self);
    Main(self);
}

