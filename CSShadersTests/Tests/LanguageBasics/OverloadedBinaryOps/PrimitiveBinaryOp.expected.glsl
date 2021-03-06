#version 450

struct PrimitiveBinaryOp
{
    int empty_struct_member;
};

void PrimitiveBinaryOp_PreConstructor(PrimitiveBinaryOp self)
{
}

void PrimitiveBinaryOp_DefaultConstructor(PrimitiveBinaryOp self)
{
    PrimitiveBinaryOp_PreConstructor(self);
}

vec2 op_Addition(vec2 lhs, vec2 rhs)
{
    return lhs + rhs;
}

void Main(PrimitiveBinaryOp self)
{
    vec2 value = vec2(0.0);
    vec2 result = op_Addition(value, value);
}

void main()
{
    PrimitiveBinaryOp self;
    PrimitiveBinaryOp_DefaultConstructor(self);
    Main(self);
}

