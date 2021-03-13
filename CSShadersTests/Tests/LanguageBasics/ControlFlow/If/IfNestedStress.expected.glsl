#version 450

struct IfNestedStress
{
    int empty_struct_member;
};

void IfNestedStress_PreConstructor(IfNestedStress self)
{
}

void IfNestedStress_DefaultConstructor(IfNestedStress self)
{
    IfNestedStress_PreConstructor(self);
}

int IfNestedStress0(IfNestedStress self)
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

int IfNestedStress1(IfNestedStress self)
{
    int one = 1;
    int nOne = -1;
    int zero = 0;
    int i0 = 0;
    int i1 = 0;
    int r0 = 0;
    int r1 = 0;
    if (i0 < 0)
    {
        r0 = nOne;
        if (i1 < 0)
        {
            r1 = nOne + nOne;
        }
        else
        {
            if (i1 > 0)
            {
                r1 = one + nOne;
            }
            else
            {
                r1 = zero + nOne;
            }
        }
        r0 = nOne;
    }
    else
    {
        if (i0 > 0)
        {
            r0 = one;
            if (i1 < 0)
            {
                r1 = nOne + one;
            }
            else
            {
                if (i1 > 0)
                {
                    r1 = one + one;
                }
                else
                {
                    r1 = zero + one;
                }
            }
            r0 = one;
        }
        else
        {
            r0 = zero;
            if (i1 < 0)
            {
                r1 = nOne + zero;
            }
            else
            {
                if (i1 > 0)
                {
                    r1 = one + zero;
                }
                else
                {
                    r1 = zero + zero;
                }
            }
            r0 = zero;
        }
    }
    int result = r0 + r1;
    return result;
}

int IfNestedStress2(IfNestedStress self)
{
    int one = 1;
    int nOne = -1;
    int zero = 0;
    int i0 = 0;
    int i1 = 0;
    int i2 = 0;
    int r0 = 0;
    int r1 = 0;
    int r2 = 0;
    if (i0 < 0)
    {
        r0 = nOne;
        if (i1 < 0)
        {
            r1 = nOne + nOne;
            if (i2 < 0)
            {
                r2 = (nOne + nOne) + nOne;
            }
            else
            {
                if (i2 > 0)
                {
                    r2 = (one + nOne) + nOne;
                }
                else
                {
                    r2 = (zero + nOne) + nOne;
                }
            }
            r1 = nOne + nOne;
        }
        else
        {
            if (i1 > 0)
            {
                r1 = one + nOne;
                if (i2 < 0)
                {
                    r2 = (nOne + one) + nOne;
                }
                else
                {
                    if (i2 > 0)
                    {
                        r2 = (one + one) + nOne;
                    }
                    else
                    {
                        r2 = (zero + one) + nOne;
                    }
                }
                r1 = one + nOne;
            }
            else
            {
                r1 = zero + nOne;
                if (i2 < 0)
                {
                    r2 = (nOne + zero) + nOne;
                }
                else
                {
                    if (i2 > 0)
                    {
                        r2 = (one + zero) + nOne;
                    }
                    else
                    {
                        r2 = (zero + zero) + nOne;
                    }
                }
                r1 = zero + nOne;
            }
        }
        r0 = nOne;
    }
    else
    {
        if (i0 > 0)
        {
            r0 = one;
            if (i1 < 0)
            {
                r1 = nOne + one;
                if (i2 < 0)
                {
                    r2 = (nOne + nOne) + one;
                }
                else
                {
                    if (i2 > 0)
                    {
                        r2 = (one + nOne) + one;
                    }
                    else
                    {
                        r2 = (zero + nOne) + one;
                    }
                }
                r1 = nOne + one;
            }
            else
            {
                if (i1 > 0)
                {
                    r1 = one + one;
                    if (i2 < 0)
                    {
                        r2 = (nOne + one) + one;
                    }
                    else
                    {
                        if (i2 > 0)
                        {
                            r2 = (one + one) + one;
                        }
                        else
                        {
                            r2 = (zero + one) + one;
                        }
                    }
                    r1 = one + one;
                }
                else
                {
                    r1 = zero + one;
                    if (i2 < 0)
                    {
                        r2 = (nOne + zero) + one;
                    }
                    else
                    {
                        if (i2 > 0)
                        {
                            r2 = (one + zero) + one;
                        }
                        else
                        {
                            r2 = (zero + zero) + one;
                        }
                    }
                    r1 = zero + one;
                }
            }
            r0 = one;
        }
        else
        {
            r0 = zero;
            if (i1 < 0)
            {
                r1 = nOne + zero;
                if (i2 < 0)
                {
                    r2 = (nOne + nOne) + zero;
                }
                else
                {
                    if (i2 > 0)
                    {
                        r2 = (one + nOne) + zero;
                    }
                    else
                    {
                        r2 = (zero + nOne) + zero;
                    }
                }
                r1 = nOne + zero;
            }
            else
            {
                if (i1 > 0)
                {
                    r1 = one + zero;
                    if (i2 < 0)
                    {
                        r2 = (nOne + one) + zero;
                    }
                    else
                    {
                        if (i2 > 0)
                        {
                            r2 = (one + one) + zero;
                        }
                        else
                        {
                            r2 = (zero + one) + zero;
                        }
                    }
                    r1 = one + zero;
                }
                else
                {
                    r1 = zero + zero;
                    if (i2 < 0)
                    {
                        r2 = (nOne + zero) + zero;
                    }
                    else
                    {
                        if (i2 > 0)
                        {
                            r2 = (one + zero) + zero;
                        }
                        else
                        {
                            r2 = (zero + zero) + zero;
                        }
                    }
                    r1 = zero + zero;
                }
            }
            r0 = zero;
        }
    }
    int result = (r0 + r1) + r2;
    return result;
}

void Main(IfNestedStress self)
{
    int ret = 0;
    ret = IfNestedStress0(self);
    ret = IfNestedStress1(self);
    ret = IfNestedStress2(self);
}

void main()
{
    IfNestedStress self;
    IfNestedStress_DefaultConstructor(self);
    Main(self);
}

