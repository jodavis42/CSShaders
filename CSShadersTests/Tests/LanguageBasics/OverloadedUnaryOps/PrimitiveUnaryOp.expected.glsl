#version 450

struct PrimitiveUnaryOp
{
    int empty_struct_member;
};

void PrimitiveUnaryOp_InitGlobals()
{
}

void PrimitiveUnaryOp_PreConstructor(PrimitiveUnaryOp self)
{
}

void PrimitiveUnaryOp_DefaultConstructor(PrimitiveUnaryOp self)
{
    PrimitiveUnaryOp_PreConstructor(self);
}

void PrimitiveUnaryOp_CopyInputs(PrimitiveUnaryOp self)
{
}

vec2 op_UnaryNegation(vec2 value)
{
    return -value;
}

void Main(PrimitiveUnaryOp self)
{
    vec2 result = op_UnaryNegation(vec2(0.0));
}

void PrimitiveUnaryOp_CopyOutputs(PrimitiveUnaryOp self)
{
}

void main()
{
    PrimitiveUnaryOp_InitGlobals();
    PrimitiveUnaryOp self;
    PrimitiveUnaryOp_DefaultConstructor(self);
    PrimitiveUnaryOp_CopyInputs(self);
    Main(self);
    PrimitiveUnaryOp_CopyOutputs(self);
}

