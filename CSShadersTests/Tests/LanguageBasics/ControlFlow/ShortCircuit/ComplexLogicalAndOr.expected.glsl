#version 450

struct ComplexLogicalAndOr
{
    int empty_struct_member;
};

void ComplexLogicalAndOr_PreConstructor(ComplexLogicalAndOr self)
{
}

void ComplexLogicalAndOr_DefaultConstructor(ComplexLogicalAndOr self)
{
    ComplexLogicalAndOr_PreConstructor(self);
}

bool LogicalOrAnd0(ComplexLogicalAndOr self)
{
    bool tempOr;
    if (true)
    {
        tempOr = true;
    }
    else
    {
        bool tempAnd;
        if (true)
        {
            tempAnd = false;
        }
        else
        {
            tempAnd = true;
        }
        tempOr = tempAnd;
    }
    return tempOr;
}

bool LogicalOrAnd1(ComplexLogicalAndOr self)
{
    bool tempOr;
    if (true)
    {
        tempOr = true;
    }
    else
    {
        tempOr = true;
    }
    bool tempAnd;
    if (tempOr)
    {
        tempAnd = false;
    }
    else
    {
        tempAnd = tempOr;
    }
    return tempAnd;
}

bool GetBool(ComplexLogicalAndOr self)
{
    return false;
}

bool LogicalOrAnd2(ComplexLogicalAndOr self)
{
    bool tempOr;
    if (false)
    {
        tempOr = false;
    }
    else
    {
        tempOr = GetBool(self);
    }
    bool tempAnd;
    if (tempOr)
    {
        bool _111 = GetBool(self);
        bool tempOr_1;
        if (_111)
        {
            tempOr_1 = _111;
        }
        else
        {
            tempOr_1 = true;
        }
        tempAnd = tempOr_1;
    }
    else
    {
        tempAnd = tempOr;
    }
    return tempAnd;
}

void Main(ComplexLogicalAndOr self)
{
    bool ret = false;
    ret = LogicalOrAnd0(self);
    ret = LogicalOrAnd1(self);
    ret = LogicalOrAnd2(self);
}

void main()
{
    ComplexLogicalAndOr self;
    ComplexLogicalAndOr_DefaultConstructor(self);
    Main(self);
}

