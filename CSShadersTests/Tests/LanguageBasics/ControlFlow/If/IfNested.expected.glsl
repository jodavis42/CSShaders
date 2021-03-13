#version 450

struct IfNested
{
    int empty_struct_member;
};

void IfNested_PreConstructor(IfNested self)
{
}

void IfNested_DefaultConstructor(IfNested self)
{
    IfNested_PreConstructor(self);
}

int IfNested0(IfNested self)
{
    int one = 1;
    int zero = 0;
    int i0 = 0;
    int i1 = 1;
    int r0 = 0;
    int r1 = 1;
    if (i0 == 0)
    {
        r0 = zero;
        if (i1 == 0)
        {
            r1 = one;
        }
        r0 = zero;
    }
    int result = r0 + r1;
    return result;
}

int IfNested1(IfNested self)
{
    int one = 1;
    int zero = 0;
    int i0 = 0;
    int i1 = 1;
    int r0 = 0;
    int r1 = 1;
    if (i0 == zero)
    {
        r0 = zero;
        if (i1 > 0)
        {
            r1 = one;
        }
        else
        {
            r1 = zero;
        }
        r0 = zero;
    }
    int result = r0 + r1;
    return result;
}

int IfNested2(IfNested self)
{
    int one = 1;
    int zero = 0;
    int nOne = -1;
    int i0 = 0;
    int i1 = 1;
    int r0 = 0;
    int r1 = 1;
    if (i0 == zero)
    {
        r0 = zero;
        if (i1 > 0)
        {
            r1 = one;
        }
        else
        {
            if (i1 < 0)
            {
                r1 = nOne;
            }
            else
            {
                r1 = zero;
            }
        }
        r0 = zero;
    }
    int result = r0 + r1;
    return result;
}

int IfNested3(IfNested self)
{
    int one = 1;
    int zero = 0;
    int nOne = -1;
    int i0 = 0;
    int i1 = 1;
    int r0 = 0;
    int r1 = 1;
    if (i0 > 0)
    {
        r0 = one;
        if (i1 > 0)
        {
            r1 = one;
        }
        r0 = one;
    }
    else
    {
        r0 = zero;
        if (i1 > 0)
        {
            r1 = one;
        }
        r0 = zero;
    }
    int result = r0 + r1;
    return result;
}

int SuperNestedIf0(IfNested self)
{
    int a = 0;
    if (true)
    {
        a = 1;
        if (false)
        {
            a = 2;
            if (a > 3)
            {
                a = 3;
            }
            a = 4;
        }
        a = 3;
    }
    a++;
    return a;
}

void Main(IfNested self)
{
    int ret = 0;
    ret = IfNested0(self);
    ret = IfNested1(self);
    ret = IfNested2(self);
    ret = IfNested3(self);
    ret = SuperNestedIf0(self);
}

void main()
{
    IfNested self;
    IfNested_DefaultConstructor(self);
    Main(self);
}

