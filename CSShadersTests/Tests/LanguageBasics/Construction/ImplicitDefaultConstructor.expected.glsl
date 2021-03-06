#version 450

struct MyClass
{
    int IntVal;
    float FloatVal;
};

struct ImplicitDefaultConstructor
{
    int empty_struct_member;
};

void ImplicitDefaultConstructor_PreConstructor(ImplicitDefaultConstructor self)
{
}

void ImplicitDefaultConstructor_DefaultConstructor(ImplicitDefaultConstructor self)
{
    ImplicitDefaultConstructor_PreConstructor(self);
}

void MyClass_PreConstructor(inout MyClass self)
{
    self.IntVal = 0;
    self.FloatVal = 0.0;
}

void MyClass_DefaultConstructor(inout MyClass self)
{
    MyClass_PreConstructor(self);
}

void Main(ImplicitDefaultConstructor self)
{
    MyClass myClass;
    MyClass_DefaultConstructor(myClass);
}

void main()
{
    ImplicitDefaultConstructor self;
    ImplicitDefaultConstructor_DefaultConstructor(self);
    Main(self);
}

