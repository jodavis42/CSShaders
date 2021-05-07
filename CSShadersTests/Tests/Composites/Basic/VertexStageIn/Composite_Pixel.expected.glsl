#version 450

struct StageInputOutputTest_Vertex
{
    vec2 Uv;
    vec3 LocalPosition;
};

struct Vertex
{
    vec2 Uv;
    vec3 LocalPosition;
};

struct StageInputOutputTest_Pixel
{
    int empty_struct_member;
};

void StageInputOutputTest_Pixel_PreConstructor(StageInputOutputTest_Pixel self)
{
}

void StageInputOutputTest_Pixel_DefaultConstructor(StageInputOutputTest_Pixel self)
{
    StageInputOutputTest_Pixel_PreConstructor(self);
}

void Main(StageInputOutputTest_Pixel self)
{
}

void main()
{
    StageInputOutputTest_Pixel self;
    StageInputOutputTest_Pixel_DefaultConstructor(self);
    Main(self);
}

