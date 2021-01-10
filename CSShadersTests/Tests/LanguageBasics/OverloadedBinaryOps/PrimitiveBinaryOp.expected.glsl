#version 450

struct PrimitiveBinaryOp
{
    int empty_struct_member;
};

void PrimitiveBinaryOp_InitGlobals()
{
}

void PrimitiveBinaryOp_PreConstructor(PrimitiveBinaryOp self)
{
}

void PrimitiveBinaryOp_DefaultConstructor(PrimitiveBinaryOp self)
{
    PrimitiveBinaryOp_PreConstructor(self);
}

void PrimitiveBinaryOp_CopyInputs(PrimitiveBinaryOp self)
{
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

void PrimitiveBinaryOp_CopyOutputs(PrimitiveBinaryOp self)
{
}

void main()
{
    PrimitiveBinaryOp_InitGlobals();
    PrimitiveBinaryOp self;
    PrimitiveBinaryOp_DefaultConstructor(self);
    PrimitiveBinaryOp_CopyInputs(self);
    Main(self);
    PrimitiveBinaryOp_CopyOutputs(self);
}

