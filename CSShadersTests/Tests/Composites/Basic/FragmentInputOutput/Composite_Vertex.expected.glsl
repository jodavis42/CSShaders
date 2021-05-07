#version 450

struct FragmentInputOutputTest_Vertex
{
    int empty_struct_member;
};

struct Vertex0
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct Vertex1
{
    float Scale;
    vec2 Uv;
};

struct Vertex2
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct FragmentInputOutputTest_Pixel
{
    int empty_struct_member;
};

struct Pixel0
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

struct Pixel1
{
    float Scale;
    vec2 Uv;
};

struct Pixel2
{
    float Scale;
    vec2 Uv;
    vec4 Color;
};

void FragmentInputOutputTest_Vertex_PreConstructor(FragmentInputOutputTest_Vertex self)
{
}

void FragmentInputOutputTest_Vertex_DefaultConstructor(FragmentInputOutputTest_Vertex self)
{
    FragmentInputOutputTest_Vertex_PreConstructor(self);
}

void Vertex0_PreConstructor(inout Vertex0 self)
{
    self.Scale = 0.0;
}

void Vertex0_DefaultConstructor(inout Vertex0 self)
{
    Vertex0_PreConstructor(self);
}

void Main(Vertex0 self)
{
}

void Vertex1_PreConstructor(inout Vertex1 self)
{
    self.Scale = 0.0;
}

void Vertex1_DefaultConstructor(inout Vertex1 self)
{
    Vertex1_PreConstructor(self);
}

void Main(Vertex1 self)
{
}

void Vertex2_PreConstructor(inout Vertex2 self)
{
    self.Scale = 0.0;
}

void Vertex2_DefaultConstructor(inout Vertex2 self)
{
    Vertex2_PreConstructor(self);
}

void Main(Vertex2 self)
{
}

void Main(FragmentInputOutputTest_Vertex self)
{
    Vertex0 tempVertex0;
    Vertex0_DefaultConstructor(tempVertex0);
    Vertex0 vertex0 = tempVertex0;
    Main(vertex0);
    Vertex1 tempVertex1;
    Vertex1_DefaultConstructor(tempVertex1);
    Vertex1 vertex1 = tempVertex1;
    vertex1.Scale = vertex0.Scale;
    Main(vertex1);
    Vertex2 tempVertex2;
    Vertex2_DefaultConstructor(tempVertex2);
    Vertex2 vertex2 = tempVertex2;
    vertex2.Scale = vertex1.Scale;
    vertex2.Uv = vertex1.Uv;
    vertex2.Color = vertex0.Color;
    Main(vertex2);
}

void main()
{
    FragmentInputOutputTest_Vertex self;
    FragmentInputOutputTest_Vertex_DefaultConstructor(self);
    Main(self);
}

