#version 450

struct PrimitiveUnaryIntrinsicOp
{
    int empty_struct_member;
};

void PrimitiveUnaryIntrinsicOp_InitGlobals()
{
}

void PrimitiveUnaryIntrinsicOp_PreConstructor(PrimitiveUnaryIntrinsicOp self)
{
}

void PrimitiveUnaryIntrinsicOp_DefaultConstructor(PrimitiveUnaryIntrinsicOp self)
{
    PrimitiveUnaryIntrinsicOp_PreConstructor(self);
}

void PrimitiveUnaryIntrinsicOp_CopyInputs(PrimitiveUnaryIntrinsicOp self)
{
}

void Main(PrimitiveUnaryIntrinsicOp self)
{
    vec2 result = -vec2(0.0);
}

void PrimitiveUnaryIntrinsicOp_CopyOutputs(PrimitiveUnaryIntrinsicOp self)
{
}

void main()
{
    PrimitiveUnaryIntrinsicOp_InitGlobals();
    PrimitiveUnaryIntrinsicOp self;
    PrimitiveUnaryIntrinsicOp_DefaultConstructor(self);
    PrimitiveUnaryIntrinsicOp_CopyInputs(self);
    Main(self);
    PrimitiveUnaryIntrinsicOp_CopyOutputs(self);
}

