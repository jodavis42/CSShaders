#version 450

struct FloatUnaryOps
{
    int empty_struct_member;
};

void FloatUnaryOps_InitGlobals()
{
}

void FloatUnaryOps_PreConstructor(FloatUnaryOps self)
{
}

void FloatUnaryOps_DefaultConstructor(FloatUnaryOps self)
{
    FloatUnaryOps_PreConstructor(self);
}

void FloatUnaryOps_CopyInputs(FloatUnaryOps self)
{
}

void Main(FloatUnaryOps self)
{
    float f = 0.0;
    float r = -f;
}

void FloatUnaryOps_CopyOutputs(FloatUnaryOps self)
{
}

void main()
{
    FloatUnaryOps_InitGlobals();
    FloatUnaryOps self;
    FloatUnaryOps_DefaultConstructor(self);
    FloatUnaryOps_CopyInputs(self);
    Main(self);
    FloatUnaryOps_CopyOutputs(self);
}

