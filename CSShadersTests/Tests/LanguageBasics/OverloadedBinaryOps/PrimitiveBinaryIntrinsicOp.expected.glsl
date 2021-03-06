#version 450

struct PrimitiveBinaryIntrinsicOp
{
    int empty_struct_member;
};

void PrimitiveBinaryIntrinsicOp_PreConstructor(PrimitiveBinaryIntrinsicOp self)
{
}

void PrimitiveBinaryIntrinsicOp_DefaultConstructor(PrimitiveBinaryIntrinsicOp self)
{
    PrimitiveBinaryIntrinsicOp_PreConstructor(self);
}

void Main(PrimitiveBinaryIntrinsicOp self)
{
    vec2 value = vec2(0.0);
    vec2 result = value + value;
}

void main()
{
    PrimitiveBinaryIntrinsicOp self;
    PrimitiveBinaryIntrinsicOp_DefaultConstructor(self);
    Main(self);
}

