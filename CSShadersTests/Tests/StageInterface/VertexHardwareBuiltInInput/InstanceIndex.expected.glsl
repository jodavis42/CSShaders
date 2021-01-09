#version 450

struct InstanceIndexTest
{
    int InstanceIndex;
};

uniform int SPIRV_Cross_BaseInstance;

void InstanceIndexTest_InitGlobals()
{
}

void InstanceIndexTest_PreConstructor(inout InstanceIndexTest self)
{
    self.InstanceIndex = 0;
}

void InstanceIndexTest_DefaultConstructor(inout InstanceIndexTest self)
{
    InstanceIndexTest_PreConstructor(self);
}

void InstanceIndexTest_CopyInputs(inout InstanceIndexTest self)
{
    self.InstanceIndex = (gl_InstanceID + SPIRV_Cross_BaseInstance);
}

void Main(InstanceIndexTest self)
{
    int index = self.InstanceIndex;
}

void InstanceIndexTest_CopyOutputs(InstanceIndexTest self)
{
}

void main()
{
    InstanceIndexTest_InitGlobals();
    InstanceIndexTest self;
    InstanceIndexTest_DefaultConstructor(self);
    InstanceIndexTest_CopyInputs(self);
    Main(self);
    InstanceIndexTest_CopyOutputs(self);
}

