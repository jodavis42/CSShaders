#version 450

struct ArrayGetIntrinsicTest
{
    int empty_struct_member;
};

void ArrayGetIntrinsicTest_PreConstructor(ArrayGetIntrinsicTest self)
{
}

void ArrayGetIntrinsicTest_DefaultConstructor(ArrayGetIntrinsicTest self)
{
    ArrayGetIntrinsicTest_PreConstructor(self);
}

float get_Item(float self[20], int key)
{
    return self[key];
}

void Main(ArrayGetIntrinsicTest self)
{
    float data[20] = float[](0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
    int index = 0;
    float value = data[index];
    value = data[0];
    value = get_Item(data, index);
}

void main()
{
    ArrayGetIntrinsicTest self;
    ArrayGetIntrinsicTest_DefaultConstructor(self);
    Main(self);
}

