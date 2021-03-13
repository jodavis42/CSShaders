#version 450

struct LogicalOr
{
    int empty_struct_member;
};

void LogicalOr_PreConstructor(LogicalOr self)
{
}

void LogicalOr_DefaultConstructor(LogicalOr self)
{
    LogicalOr_PreConstructor(self);
}

bool LogicalOr0(LogicalOr self)
{
    bool tempOr;
    if (true)
    {
        tempOr = true;
    }
    else
    {
        tempOr = false;
    }
    return tempOr;
}

bool LogicalOr1(LogicalOr self)
{
    bool a = true;
    bool b = false;
    bool tempOr;
    if (a)
    {
        tempOr = a;
    }
    else
    {
        tempOr = b;
    }
    return tempOr;
}

bool GetBool(LogicalOr self)
{
    return false;
}

bool LogicalOr2(LogicalOr self)
{
    bool _76 = GetBool(self);
    bool tempOr;
    if (_76)
    {
        tempOr = _76;
    }
    else
    {
        tempOr = GetBool(self);
    }
    return tempOr;
}

void Main(LogicalOr self)
{
    bool ret = false;
    ret = LogicalOr0(self);
    ret = LogicalOr1(self);
    ret = LogicalOr2(self);
}

void main()
{
    LogicalOr self;
    LogicalOr_DefaultConstructor(self);
    Main(self);
}

