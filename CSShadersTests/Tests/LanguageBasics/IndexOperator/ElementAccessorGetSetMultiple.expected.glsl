#version 450

struct MyStruct
{
    int value;
};

struct ElementAccessorGetSetMultipleTest
{
    int empty_struct_member;
};

void ElementAccessorGetSetMultipleTest_PreConstructor(ElementAccessorGetSetMultipleTest self)
{
}

void ElementAccessorGetSetMultipleTest_DefaultConstructor(ElementAccessorGetSetMultipleTest self)
{
    ElementAccessorGetSetMultipleTest_PreConstructor(self);
}

void MyStruct_PreConstructor(inout MyStruct self)
{
    self.value = 0;
}

void MyStruct_DefaultConstructor(inout MyStruct self)
{
    MyStruct_PreConstructor(self);
}

int get_Item(MyStruct self, int y, int x)
{
    return (self.value + y) + x;
}

void set_Item(inout MyStruct self, int y, int x, int value)
{
    self.value = (value + y) + x;
}

void Main(ElementAccessorGetSetMultipleTest self)
{
    MyStruct tempMyStruct;
    MyStruct_DefaultConstructor(tempMyStruct);
    MyStruct data = tempMyStruct;
    int item00 = get_Item(data, 0, 0);
    set_Item(data, 1, 1, item00);
    int _85 = get_Item(data, 0, 1);
    set_Item(data, 1, 0, _85);
}

void main()
{
    ElementAccessorGetSetMultipleTest self;
    ElementAccessorGetSetMultipleTest_DefaultConstructor(self);
    Main(self);
}

