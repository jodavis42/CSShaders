#version 450

struct PrimitiveUnaryOp
{
    int empty_struct_member;
};

void PrimitiveUnaryOp_PreConstructor(PrimitiveUnaryOp self)
{
}

void PrimitiveUnaryOp_DefaultConstructor(PrimitiveUnaryOp self)
{
    PrimitiveUnaryOp_PreConstructor(self);
}

vec2 op_UnaryNegation(vec2 value)
{
    return -value;
}

void Main(PrimitiveUnaryOp self)
{
    vec2 result = op_UnaryNegation(vec2(0.0));
}

void main()
{
    PrimitiveUnaryOp self;
    PrimitiveUnaryOp_DefaultConstructor(self);
    Main(self);
}

