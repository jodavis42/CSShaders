#version 450

struct PrimitiveUnaryIntrinsicOp
{
    int empty_struct_member;
};

void PrimitiveUnaryIntrinsicOp_PreConstructor(PrimitiveUnaryIntrinsicOp self)
{
}

void PrimitiveUnaryIntrinsicOp_DefaultConstructor(PrimitiveUnaryIntrinsicOp self)
{
    PrimitiveUnaryIntrinsicOp_PreConstructor(self);
}

void Main(PrimitiveUnaryIntrinsicOp self)
{
    vec2 result = -vec2(0.0);
}

void main()
{
    PrimitiveUnaryIntrinsicOp self;
    PrimitiveUnaryIntrinsicOp_DefaultConstructor(self);
    Main(self);
}

