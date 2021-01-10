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

void MyClass_Constructor(inout MyClass self, int intVal, float floatVal)
{
    self.IntVal = intVal;
    self.FloatVal = floatVal;
}

void Main(DefaultConstructorIntrinsic self)
{
    MyClass tempMyClass;
    MyClass_Constructor(tempMyClass, 1, 2.0);
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

