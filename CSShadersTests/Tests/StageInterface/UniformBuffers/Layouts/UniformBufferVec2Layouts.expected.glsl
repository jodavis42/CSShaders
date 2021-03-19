#version 450

struct Vec2Buffer0
{
    vec2 Vec2_0;
    vec2 Vec2_1;
    vec2 Vec2_2;
};

struct Vec2Buffer1
{
    float Value0;
    vec2 Vec2_0;
    vec2 Vec2_1;
};

struct Vec2Buffer2
{
    float Value0;
    float Value1;
    vec2 Vec2_0;
    vec2 Vec2_1;
};

struct Vec2Buffer3
{
    float Value0;
    float Value1;
    float Value2;
    vec2 Vec2;
};

struct UniformBufferVec2Layouts
{
    Vec2Buffer0 Vec2Buffer0;
    Vec2Buffer1 Vec2Buffer1;
    Vec2Buffer2 Vec2Buffer2;
    Vec2Buffer3 Vec2Buffer3;
};

layout(std140) uniform Vec2Buffer0_0
{
    vec2 Vec2_0;
    vec2 Vec2_1;
    vec2 Vec2_2;
} Vec2Buffer0_Instance;

layout(std140) uniform Vec2Buffer1_0
{
    float Value0;
    vec2 Vec2_0;
    vec2 Vec2_1;
} Vec2Buffer1_Instance;

layout(std140) uniform Vec2Buffer2_0
{
    float Value0;
    float Value1;
    vec2 Vec2_0;
    vec2 Vec2_1;
} Vec2Buffer2_Instance;

layout(std140) uniform Vec2Buffer3_0
{
    float Value0;
    float Value1;
    float Value2;
    vec2 Vec2;
} Vec2Buffer3_Instance;

void Vec2Buffer0_PreConstructor(Vec2Buffer0 self)
{
}

void Vec2Buffer0_DefaultConstructor(Vec2Buffer0 self)
{
    Vec2Buffer0_PreConstructor(self);
}

void Vec2Buffer1_PreConstructor(inout Vec2Buffer1 self)
{
    self.Value0 = 0.0;
}

void Vec2Buffer1_DefaultConstructor(inout Vec2Buffer1 self)
{
    Vec2Buffer1_PreConstructor(self);
}

void Vec2Buffer2_PreConstructor(inout Vec2Buffer2 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
}

void Vec2Buffer2_DefaultConstructor(inout Vec2Buffer2 self)
{
    Vec2Buffer2_PreConstructor(self);
}

void Vec2Buffer3_PreConstructor(inout Vec2Buffer3 self)
{
    self.Value0 = 0.0;
    self.Value1 = 0.0;
    self.Value2 = 0.0;
}

void Vec2Buffer3_DefaultConstructor(inout Vec2Buffer3 self)
{
    Vec2Buffer3_PreConstructor(self);
}

void UniformBufferVec2Layouts_PreConstructor(inout UniformBufferVec2Layouts self)
{
    Vec2Buffer0 tempVec2Buffer0;
    Vec2Buffer0_DefaultConstructor(tempVec2Buffer0);
    self.Vec2Buffer0 = tempVec2Buffer0;
    Vec2Buffer1 tempVec2Buffer1;
    Vec2Buffer1_DefaultConstructor(tempVec2Buffer1);
    self.Vec2Buffer1 = tempVec2Buffer1;
    Vec2Buffer2 tempVec2Buffer2;
    Vec2Buffer2_DefaultConstructor(tempVec2Buffer2);
    self.Vec2Buffer2 = tempVec2Buffer2;
    Vec2Buffer3 tempVec2Buffer3;
    Vec2Buffer3_DefaultConstructor(tempVec2Buffer3);
    self.Vec2Buffer3 = tempVec2Buffer3;
}

void UniformBufferVec2Layouts_DefaultConstructor(inout UniformBufferVec2Layouts self)
{
    UniformBufferVec2Layouts_PreConstructor(self);
}

void UniformBufferVec2Layouts_CopyInputs(inout UniformBufferVec2Layouts self)
{
    self.Vec2Buffer0.Vec2_0 = Vec2Buffer0_Instance.Vec2_0;
    self.Vec2Buffer0.Vec2_1 = Vec2Buffer0_Instance.Vec2_1;
    self.Vec2Buffer0.Vec2_2 = Vec2Buffer0_Instance.Vec2_2;
    self.Vec2Buffer1.Value0 = Vec2Buffer1_Instance.Value0;
    self.Vec2Buffer1.Vec2_0 = Vec2Buffer1_Instance.Vec2_0;
    self.Vec2Buffer1.Vec2_1 = Vec2Buffer1_Instance.Vec2_1;
    self.Vec2Buffer2.Value0 = Vec2Buffer2_Instance.Value0;
    self.Vec2Buffer2.Value1 = Vec2Buffer2_Instance.Value1;
    self.Vec2Buffer2.Vec2_0 = Vec2Buffer2_Instance.Vec2_0;
    self.Vec2Buffer2.Vec2_1 = Vec2Buffer2_Instance.Vec2_1;
    self.Vec2Buffer3.Value0 = Vec2Buffer3_Instance.Value0;
    self.Vec2Buffer3.Value1 = Vec2Buffer3_Instance.Value1;
    self.Vec2Buffer3.Value2 = Vec2Buffer3_Instance.Value2;
    self.Vec2Buffer3.Vec2 = Vec2Buffer3_Instance.Vec2;
}

void Main(UniformBufferVec2Layouts self)
{
}

void main()
{
    UniformBufferVec2Layouts self;
    UniformBufferVec2Layouts_DefaultConstructor(self);
    UniformBufferVec2Layouts_CopyInputs(self);
    Main(self);
}

