#version 450

struct MyClass
{
    int IntVal;
    float FloatVal;
};

struct DefaultConstructorIntrinsic
{
    int empty_struct_member;
};

void DefaultConstructorIntrinsic_InitGlobals()
{
}

void DefaultConstructorIntrinsic_PreConstructor(DefaultConstructorIntrinsic self)
{
}

void DefaultConstructorIntrinsic_DefaultConstructor(DefaultConstructorIntrinsic self)
{
    DefaultConstructorIntrinsic_PreConstructor(self);
}

void DefaultConstructorIntrinsic_CopyInputs(DefaultConstructorIntrinsic self)
{
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

void Main(DefaultConstructorIntrinsic self)
{
    MyClass tempMyClass;
    MyClass_DefaultConstructor(tempMyClass);
    MyClass myClass = tempMyClass;
}

void DefaultConstructorIntrinsic_CopyOutputs(DefaultConstructorIntrinsic self)
{
}

void main()
{
    DefaultConstructorIntrinsic_InitGlobals();
    DefaultConstructorIntrinsic self;
    DefaultConstructorIntrinsic_DefaultConstructor(self);
    DefaultConstructorIntrinsic_CopyInputs(self);
    Main(self);
    DefaultConstructorIntrinsic_CopyOutputs(self);
}

