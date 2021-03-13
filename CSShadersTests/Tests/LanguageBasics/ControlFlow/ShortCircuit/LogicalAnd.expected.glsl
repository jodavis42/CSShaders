#version 450

struct LogicalAnd
{
    int empty_struct_member;
};

void LogicalAnd_PreConstructor(LogicalAnd self)
{
}

void LogicalAnd_DefaultConstructor(LogicalAnd self)
{
    LogicalAnd_PreConstructor(self);
}

bool LogicalAnd0(LogicalAnd self)
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
    return tempAnd;
}

bool LogicalAnd1(LogicalAnd self)
{
    bool a = true;
    bool b = false;
    bool tempAnd;
    if (a)
    {
        tempAnd = b;
    }
    else
    {
        tempAnd = a;
    }
    return tempAnd;
}

bool GetBool(LogicalAnd self)
{
    return false;
}

bool LogicalAnd2(LogicalAnd self)
{
    bool _76 = GetBool(self);
    bool tempAnd;
    if (_76)
    {
        tempAnd = GetBool(self);
    }
    else
    {
        tempAnd = _76;
    }
    return tempAnd;
}

void Main(LogicalAnd self)
{
    bool ret = false;
    ret = LogicalAnd0(self);
    ret = LogicalAnd1(self);
    ret = LogicalAnd2(self);
}

void main()
{
    LogicalAnd self;
    LogicalAnd_DefaultConstructor(self);
    Main(self);
}

