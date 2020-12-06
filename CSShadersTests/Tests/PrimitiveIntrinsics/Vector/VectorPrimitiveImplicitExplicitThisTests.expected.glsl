#version 450

struct VectorPrimitiveImplicitExplicitThisTests
{
    int empty_struct_member;
};

void VectorPrimitiveImplicitExplicitThisTests_InitGlobals()
{
}

void VectorPrimitiveImplicitExplicitThisTests_PreConstructor(VectorPrimitiveImplicitExplicitThisTests self)
{
}

void VectorPrimitiveImplicitExplicitThisTests_DefaultConstructor(VectorPrimitiveImplicitExplicitThisTests self)
{
    VectorPrimitiveImplicitExplicitThisTests_PreConstructor(self);
}

void VectorPrimitiveImplicitExplicitThisTests_CopyInputs(VectorPrimitiveImplicitExplicitThisTests self)
{
}

float FieldThisExplicit(vec2 self)
{
    return self.x;
}

float FieldThisImplicit(vec2 self)
{
    return self.x;
}

vec2 PropertyThisExplicit(vec2 self)
{
    return self.xy;
}

vec2 PropertyThisImplicit(vec2 self)
{
    return self.xy;
}

void Main(VectorPrimitiveImplicitExplicitThisTests self)
{
    vec2 vector0 = vec2(0.0);
    float fieldThisExplicit = FieldThisExplicit(vector0);
    float fieldThisImplicit = FieldThisImplicit(vector0);
    vec2 propertyThisExplicit = PropertyThisExplicit(vector0);
    vec2 propertyThisImplicit = PropertyThisImplicit(vector0);
}

void VectorPrimitiveImplicitExplicitThisTests_CopyOutputs(VectorPrimitiveImplicitExplicitThisTests self)
{
}

void main()
{
    VectorPrimitiveImplicitExplicitThisTests_InitGlobals();
    VectorPrimitiveImplicitExplicitThisTests self;
    VectorPrimitiveImplicitExplicitThisTests_DefaultConstructor(self);
    VectorPrimitiveImplicitExplicitThisTests_CopyInputs(self);
    Main(self);
    VectorPrimitiveImplicitExplicitThisTests_CopyOutputs(self);
}

