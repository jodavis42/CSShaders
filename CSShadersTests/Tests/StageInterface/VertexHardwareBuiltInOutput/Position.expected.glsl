#version 450

struct Position
{
    vec4 PerspectivePosition;
};

void Position_PreConstructor(Position self)
{
}

void Position_DefaultConstructor(Position self)
{
    Position_PreConstructor(self);
}

void Main(inout Position self)
{
    self.PerspectivePosition = vec4(1.0, 2.0, 3.0, 4.0);
}

void Position_CopyOutputs(Position self)
{
    gl_Position = self.PerspectivePosition;
}

void main()
{
    Position self;
    Position_DefaultConstructor(self);
    Main(self);
    Position_CopyOutputs(self);
}

