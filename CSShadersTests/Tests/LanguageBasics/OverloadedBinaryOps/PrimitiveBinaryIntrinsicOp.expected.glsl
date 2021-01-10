#version 450

struct PrimitiveBinaryIntrinsicOp
{
    int empty_struct_member;
};

void PrimitiveBinaryIntrinsicOp_InitGlobals()
{
}

void PrimitiveBinaryIntrinsicOp_PreConstructor(PrimitiveBinaryIntrinsicOp self)
{
}

void PrimitiveBinaryIntrinsicOp_DefaultConstructor(PrimitiveBinaryIntrinsicOp self)
{
    PrimitiveBinaryIntrinsicOp_PreConstructor(self);
}

void PrimitiveBinaryIntrinsicOp_CopyInputs(PrimitiveBinaryIntrinsicOp self)
{
}

void Main(PrimitiveBinaryIntrinsicOp self)
{
    vec2 value = vec2(0.0);
    vec2 result = value + value;
}

void PrimitiveBinaryIntrinsicOp_CopyOutputs(PrimitiveBinaryIntrinsicOp self)
{
}

void main()
{
    PrimitiveBinaryIntrinsicOp_InitGlobals();
    PrimitiveBinaryIntrinsicOp self;
    PrimitiveBinaryIntrinsicOp_DefaultConstructor(self);
    PrimitiveBinaryIntrinsicOp_CopyInputs(self);
    Main(self);
    PrimitiveBinaryIntrinsicOp_CopyOutputs(self);
}

