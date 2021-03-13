#version 450

struct ShortCircuitIf
{
    int empty_struct_member;
};

void ShortCircuitIf_PreConstructor(ShortCircuitIf self)
{
}

void ShortCircuitIf_DefaultConstructor(ShortCircuitIf self)
{
    ShortCircuitIf_PreConstructor(self);
}

int ShortCircuitIf0(ShortCircuitIf self)
{
    bool lhs = true;
    bool rhs = false;
    bool tempAnd;
    if (lhs)
    {
        tempAnd = rhs;
    }
    else
    {
        tempAnd = lhs;
    }
    if (tempAnd)
    {
        return 0;
    }
    return 3;
}

int ShortCircuitIf1(ShortCircuitIf self)
{
    bool lhs = true;
    bool rhs = false;
    bool tempAnd;
    if (lhs)
    {
        tempAnd = rhs;
    }
    else
    {
        tempAnd = lhs;
    }
    if (tempAnd)
    {
        return 0;
    }
    else
    {
        bool tempOr;
        if (lhs)
        {
            tempOr = lhs;
        }
        else
        {
            tempOr = rhs;
        }
        if (tempOr)
        {
            return 1;
        }
    }
    return 3;
}

int ShortCircuitIf2(ShortCircuitIf self)
{
    bool lhs = true;
    bool rhs = false;
    bool tempAnd;
    if (lhs)
    {
        tempAnd = rhs;
    }
    else
    {
        tempAnd = lhs;
    }
    if (tempAnd)
    {
        return 0;
    }
    else
    {
        bool tempOr;
        if (lhs)
        {
            tempOr = lhs;
        }
        else
        {
            tempOr = rhs;
        }
        if (tempOr)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}

void Main(ShortCircuitIf self)
{
    int ret = 0;
    ret = ShortCircuitIf0(self);
    ret = ShortCircuitIf1(self);
    ret = ShortCircuitIf2(self);
}

void main()
{
    ShortCircuitIf self;
    ShortCircuitIf_DefaultConstructor(self);
    Main(self);
}

