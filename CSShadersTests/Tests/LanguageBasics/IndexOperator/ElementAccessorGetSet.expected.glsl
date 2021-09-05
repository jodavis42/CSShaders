#version 450

struct MyStruct
{
    int value;
};

struct ElementAccessorGetSetTest
{
    int empty_struct_member;
};

void ElementAccessorGetSetTest_PreConstructor(ElementAccessorGetSetTest self)
{
}

void ElementAccessorGetSetTest_DefaultConstructor(ElementAccessorGetSetTest self)
{
    ElementAccessorGetSetTest_PreConstructor(self);
}

void MyStruct_PreConstructor(inout MyStruct self)
{
    self.value = 0;
}

void MyStruct_DefaultConstructor(inout MyStruct self)
{
    MyStruct_PreConstructor(self);
}

int get_Item(MyStruct self, int key)
{
    return self.value + key;
}

void set_Item(inout MyStruct self, int key, int value)
{
    self.value = key + value;
}

void Main(ElementAccessorGetSetTest self)
{
    MyStruct tempMyStruct;
    MyStruct_DefaultConstructor(tempMyStruct);
    MyStruct data = tempMyStruct;
    int item0 = get_Item(data, 0);
    set_Item(data, 1, item0);
    int _81 = get_Item(data, 1);
    set_Item(data, 0, _81);
}

void main()
{
    ElementAccessorGetSetTest self;
    ElementAccessorGetSetTest_DefaultConstructor(self);
    Main(self);
}

