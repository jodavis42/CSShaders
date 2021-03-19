#version 450

struct Vec3Buffer0
{
    vec3 TestVector3_0;
    vec3 TestVector3_1;
};

struct Vec3Buffer1
{
    vec3 TestVector3_0;
    float Value0;
    vec3 TestVector3_1;
};

struct Vec3Buffer2
{
    float Value0;
    vec3 TestVector3;
};

struct Vec3Buffer3
{
    float Value0;
    float Value1;
    vec3 TestVector3;
};

struct Vec3Buffer4
{
    float Value0;
    float Value1;
    float Value2;
    vec3 TestVector3;
};

struct UniformBufferVec3Layouts
{
    Vec3Buffer0 Vec3Buffer0;
    Vec3Buffer1 Vec3Buffer1;
    Vec3Buffer2 Vec3Buffer2;
    Vec3Buffer3 Vec3Buffer3;
    Vec3Buffer4 Vec3Buffer4;
};

layout(std140) uniform Vec3Buffer0_0
{
    vec3 TestVector3_0;
    vec3 TestVector3_1;
} Vec3Buffer0_Instance;

layout(std140) uniform Vec3Buffer1_0
{
    vec3 TestVector3_0;
    float Value0;
    vec3 TestVector3_1;
} Vec3Buffer1_Instance;

layout(std140) uniform Vec3Buffer2_0
{
    float Value0;
    vec3 TestVector3;
} Vec3Buffer2_Instance;

layout(std140) uniform Vec3Buffer3_0
{
    float Value0;
    float Value1;
    vec3 TestVector3;
} Vec3Buffer3_Instance;

layout(std140) uniform Vec3Buffer4_0
{
    float Value0;
    float Value1;
    float Value2;
    vec3 TestVector3;
} Vec3Buffer4_Instance;

void Vec3Buffer0_PreConstructor(Vec3Buffer0 self)
{
}

void Vec3Buffer0_DefaultConstructor(Vec3Buffer0 self)
{
    Vec3Buffer0_PreConstructor(self);
}

void Vec3Buffer1_PreConstructor(inout Vec3Buffer1 self)
{
    self.Value0 = 0.0;
}

void Vec3Buffer1_DefaultConstructor(inout Vec3Buffer1 self)
{
    Vec3Buffer1_PreConstructor(self);
}

void Vec3Buffer2_PreConstructor(inout Vec3Buffer2 self)
{
    self.Value0 = 0.0;
}

void Vec3Buffer2_DefaultConstructor(inout Vec3Buffer2 self)
{
    Vec3Buffer2_PreConstructor(self);
}

void Vec3Buffer3_PreConstructor(inout Vec3Buffer3 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
}

void Vec3Buffer3_DefaultConstructor(inout Vec3Buffer3 self)
{
    Vec3Buffer3_PreConstructor(self);
}

void Vec3Buffer4_PreConstructor(inout Vec3Buffer4 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
    self.Value2 = 0.0;
}

void Vec3Buffer4_DefaultConstructor(inout Vec3Buffer4 self)
{
    Vec3Buffer4_PreConstructor(self);
}

void UniformBufferVec3Layouts_PreConstructor(inout UniformBufferVec3Layouts self)
{
    Vec3Buffer0 tempVec3Buffer0;
    Vec3Buffer0_DefaultConstructor(tempVec3Buffer0);
    self.Vec3Buffer0 = tempVec3Buffer0;
    Vec3Buffer1 tempVec3Buffer1;
    Vec3Buffer1_DefaultConstructor(tempVec3Buffer1);
    self.Vec3Buffer1 = tempVec3Buffer1;
    Vec3Buffer2 tempVec3Buffer2;
    Vec3Buffer2_DefaultConstructor(tempVec3Buffer2);
    self.Vec3Buffer2 = tempVec3Buffer2;
    Vec3Buffer3 tempVec3Buffer3;
    Vec3Buffer3_DefaultConstructor(tempVec3Buffer3);
    self.Vec3Buffer3 = tempVec3Buffer3;
    Vec3Buffer4 tempVec3Buffer4;
    Vec3Buffer4_DefaultConstructor(tempVec3Buffer4);
    self.Vec3Buffer4 = tempVec3Buffer4;
}

void UniformBufferVec3Layouts_DefaultConstructor(inout UniformBufferVec3Layouts self)
{
    UniformBufferVec3Layouts_PreConstructor(self);
}

void UniformBufferVec3Layouts_CopyInputs(inout UniformBufferVec3Layouts self)
{
    self.Vec3Buffer0.TestVector3_0 = Vec3Buffer0_Instance.TestVector3_0;
    self.Vec3Buffer0.TestVector3_1 = Vec3Buffer0_Instance.TestVector3_1;
    self.Vec3Buffer1.TestVector3_0 = Vec3Buffer1_Instance.TestVector3_0;
    self.Vec3Buffer1.Value0 = Vec3Buffer1_Instance.Value0;
    self.Vec3Buffer1.TestVector3_1 = Vec3Buffer1_Instance.TestVector3_1;
    self.Vec3Buffer2.Value0 = Vec3Buffer2_Instance.Value0;
    self.Vec3Buffer2.TestVector3 = Vec3Buffer2_Instance.TestVector3;
    self.Vec3Buffer3.Value0 = Vec3Buffer3_Instance.Value0;
    self.Vec3Buffer3.Value1 = Vec3Buffer3_Instance.Value1;
    self.Vec3Buffer3.TestVector3 = Vec3Buffer3_Instance.TestVector3;
    self.Vec3Buffer4.Value0 = Vec3Buffer4_Instance.Value0;
    self.Vec3Buffer4.Value1 = Vec3Buffer4_Instance.Value1;
    self.Vec3Buffer4.Value2 = Vec3Buffer4_Instance.Value2;
    self.Vec3Buffer4.TestVector3 = Vec3Buffer4_Instance.TestVector3;
}

void Main(UniformBufferVec3Layouts self)
{
}

void main()
{
    UniformBufferVec3Layouts self;
    UniformBufferVec3Layouts_DefaultConstructor(self);
    UniformBufferVec3Layouts_CopyInputs(self);
    Main(self);
}

