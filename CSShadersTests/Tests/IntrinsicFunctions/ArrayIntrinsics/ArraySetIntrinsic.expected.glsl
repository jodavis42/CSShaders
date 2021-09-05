#version 450

struct ArraySetIntrinsicTest
{
    int empty_struct_member;
};

void ArraySetIntrinsicTest_PreConstructor(ArraySetIntrinsicTest self)
{
}

void ArraySetIntrinsicTest_DefaultConstructor(ArraySetIntrinsicTest self)
{
    ArraySetIntrinsicTest_PreConstructor(self);
}

void set_Item(inout float self[20], int key, float value)
{
    self[key] = value;
}

void Main(ArraySetIntrinsicTest self)
{
    float data[20] = float[](0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
    int index = 0;
    data[index] = 1.0;
    data[0] = 2.0;
    set_Item(data, 0, 2.0);
}

void main()
{
    ArraySetIntrinsicTest self;
    ArraySetIntrinsicTest_DefaultConstructor(self);
    Main(self);
}

