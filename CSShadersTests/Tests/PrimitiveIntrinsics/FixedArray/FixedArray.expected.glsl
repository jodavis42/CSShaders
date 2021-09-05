#version 450

struct FixedArrayPrimitiveDeclarationTest
{
    int empty_struct_member;
};

void FixedArrayPrimitiveDeclarationTest_PreConstructor(FixedArrayPrimitiveDeclarationTest self)
{
}

void FixedArrayPrimitiveDeclarationTest_DefaultConstructor(FixedArrayPrimitiveDeclarationTest self)
{
    FixedArrayPrimitiveDeclarationTest_PreConstructor(self);
}

float get_Item(float self[20], int key)
{
    return self[key];
}

void set_Item(inout float self[20], int key, float value)
{
    self[key] = value;
}

void Main(FixedArrayPrimitiveDeclarationTest self)
{
    float data[20] = float[](0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
    float s = get_Item(data, 2);
    s = get_Item(data, 1);
    float _86 = get_Item(data, 0);
    set_Item(data, 0, _86 + s);
}

void main()
{
    FixedArrayPrimitiveDeclarationTest self;
    FixedArrayPrimitiveDeclarationTest_DefaultConstructor(self);
    Main(self);
}

