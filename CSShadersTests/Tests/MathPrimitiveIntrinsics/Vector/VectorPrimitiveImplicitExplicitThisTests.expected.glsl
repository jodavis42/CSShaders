#version 450

struct VectorPrimitiveImplicitExplicitThisTests
{
    int empty_struct_member;
};

void PreConstructor_VectorPrimitiveImplicitExplicitThisTests(VectorPrimitiveImplicitExplicitThisTests self)
{
}

void DefaultConstructor_VectorPrimitiveImplicitExplicitThisTests(VectorPrimitiveImplicitExplicitThisTests self)
{
    PreConstructor_VectorPrimitiveImplicitExplicitThisTests(self);
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

void main()
{
    VectorPrimitiveImplicitExplicitThisTests self;
    DefaultConstructor_VectorPrimitiveImplicitExplicitThisTests(self);
    Main(self);
}

